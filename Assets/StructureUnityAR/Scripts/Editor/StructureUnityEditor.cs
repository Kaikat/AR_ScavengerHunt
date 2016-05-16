/**********************************************************************
  This file is part of the Structure SDK.
  Copyright © 2015 Occipital, Inc. All rights reserved.
  http://structure.io
**********************************************************************/

using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading;
using Debug = UnityEngine.Debug;

//NOTE: To parse the Xcode project and make changes to it post-generation by Unity,
// we use a copy of Unity's XcodeAPI. This is under development, and there will be
// duplicate symbol warnings between our inclusion of the XcodeAPI scripts here
// as well as built-in to Unity. These warnings appear to be harmless.

#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

using PlistEntry = System.Collections.Generic.KeyValuePair<string, UnityEditor.iOS.Xcode.PlistElement>; 

// IOSArchitectures taken from: http://forum.unity3d.com/threads/4-6-ios-64-bit-beta.290551/page-11
enum IOSArchitectures : int
{
	Armv7 = 0,
	Arm64 = 1,
	Universal = 2,
};

[InitializeOnLoad]
public class StructurePlugin : MonoBehaviour
{
	static StructurePlugin ()
	{
		// Set the iOS target version to iOS 8.
		PlayerSettings.iOS.targetOSVersion = iOSTargetOSVersion.iOS_8_0;
		
		// Restrict deployment to iPad only.
		//PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPadOnly;
		PlayerSettings.iOS.targetDevice = iOSTargetDevice.iPhoneAndiPad;

		// Use IL2CPP (the only arm64-capable one) as a scripting backend.
		PlayerSettings.SetPropertyInt("ScriptingBackend", (int)ScriptingImplementation.IL2CPP, BuildTargetGroup.iOS);
		
		// Assuming you want to build Arm64 only.
		PlayerSettings.SetPropertyInt("Architecture", (int)IOSArchitectures.Arm64, BuildTargetGroup.iOS);
		
		EditorApplication.update += Update;
	}
	
	static void Update ()
	{
		// Called once per frame, by the editor.
	}
	
	[PostProcessBuildAttribute]
	public static void OnPostprocessBuild (BuildTarget target, string pathToBuiltProject)
	{
		if (target != BuildTarget.iOS)
			return;
		
		//NOTE: these patches are not robust to Xcode generation by Append, only Rebuild
		PatchXcodeProject(pathToBuiltProject);
		
		PatchInfoPlist(pathToBuiltProject);
		
		// This needs to done after we have rewritten everything, because it may trigger an Xcode launch.
		SelectXcodeBuildConfiguration(pathToBuiltProject, "Release");
	}

	public static string checkPBXProjectPath (string projectPath)
	{
		//In versions of Unity < 5.1.3p2,
		// the xcode project path returned by PBXProject.GetPBXProjectPath
		// is incorrect. We fix it here.

		string projectBundlePath = Path.GetDirectoryName(projectPath);

		if (projectBundlePath.EndsWith(".xcodeproj"))
			return projectPath;
		else
			return projectBundlePath + ".xcodeproj/project.pbxproj";
	}
	
	public static void PatchXcodeProject (string pathToBuiltProject)
	{
		PBXProject project = new PBXProject();
		
		string projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);

		projectPath = checkPBXProjectPath(projectPath);

		project.ReadFromFile(projectPath);
		
		string guid = project.TargetGuidByName("Unity-iPhone");
		project.AddFrameworkToProject(guid, "ExternalAccessory.framework", false);
		
		// The following settings lead to a quicker build
		string releaseConfig = project.BuildConfigByName(guid, "Release");
		project.SetBuildPropertyForConfig(releaseConfig, "DEBUG_INFORMATION_FORMAT", "dwarf");
		project.SetBuildPropertyForConfig(releaseConfig, "ONLY_ACTIVE_ARCH", "YES");
		// XCode7 enables BitCode for all projects by default.  Neither the Structure SDK nor Unity support BitCode at this time
		project.SetBuildPropertyForConfig(releaseConfig, "ENABLE_BITCODE", "NO");
		string debugConfig = project.BuildConfigByName(guid, "Debug");
		project.SetBuildPropertyForConfig(debugConfig, "ENABLE_BITCODE", "NO"); 
		project.WriteToFile(projectPath);
	}
	
	public static void PatchInfoPlist(string pathToBuiltProject)
	{
		string plistPath = Path.Combine(pathToBuiltProject, "Info.plist");
		
		PlistDocument plist = new PlistDocument();
		plist.ReadFromFile(plistPath);
		
		
		// =================================
		// We must do this here instead of passing the plist to
		//  a useful helper function because Unity refuses to build functions
		//  where a variable of type PlistDocument is passed.
		
		string key = "UISupportedExternalAccessoryProtocols";
		string[] values = new string[3]
		{
			"io.structure.control",
			"io.structure.depth",
			"io.structure.infrared"
		};
		
		if (plist.root.values.ContainsKey(key))
			return;
		
		PlistElementArray array = new PlistElementArray();
		foreach (string value in values)
			array.AddString(value);
		
		plist.root.values.Add (new PlistEntry(key, array));
		// =================================
		
		
		plist.root.values.Add( new PlistEntry("UIFileSharingEnabled", new PlistElementBoolean(true) ) );
		
		
		plist.WriteToFile(plistPath);
	}
	
	public static void TriggerXcodeDefaultSharedSchemeGeneration (string pathToBuiltProject)
	{
		// Launch Xcode to trigger the scheme generation.
		ProcessStartInfo proc = new ProcessStartInfo();
		
		proc.FileName = "open";
		proc.WorkingDirectory = pathToBuiltProject;
		proc.Arguments = "Unity-iPhone.xcodeproj";
		proc.WindowStyle = ProcessWindowStyle.Hidden;
		proc.UseShellExecute = true;
		Process.Start(proc);
		
		Thread.Sleep(3000);
	}
	
	public static string GetDefaultSharedSchemePath (string pathToBuiltProject)
	{
		return Path.Combine(pathToBuiltProject, "Unity-iPhone.xcodeproj/xcshareddata/xcschemes/Unity-iPhone.xcscheme");
	}
	
	public static void SelectXcodeBuildConfiguration (string pathToBuiltProject, string configuration)
	{
		string schemePath = GetDefaultSharedSchemePath(pathToBuiltProject);
		
		if (!File.Exists(schemePath))
			TriggerXcodeDefaultSharedSchemeGeneration(pathToBuiltProject);
		
		if (!File.Exists(schemePath))
		{
			//Debug.Log("Xcode scheme project generation failed. You will need to manually select the 'Release' configuration. The deployed iOS application performance will be disastrous, otherwise.");
			return;
		}
		
		XmlDocument xml = new XmlDocument();
		xml.Load(schemePath);
		XmlNode node = xml.SelectSingleNode("Scheme/LaunchAction");
		node.Attributes["buildConfiguration"].Value = configuration;
		xml.Save(schemePath);
	}	
}

#endif
