using UnityEngine;
using System.Collections;

namespace UnityChan 
{
	public class animate : MonoBehaviour {

		private FaceAnimationManager faceManager;

		// Use this for initialization
		void Start () 
		{
			faceManager = GetComponent<FaceAnimationManager> ();
		}
	
		// Update is called once per frame
		void Update () 
		{
		}

		void AnimateFace_Talk1 ()
		{
			
			string[] temp = { "MTH_E", "MTH_A", "MTH_A", "MTH_A", "MTH_O", 
				"MTH_A", "MTH_I", "MTH_A", "MTH_A", "MTH_E", 
				"MTH_E", "MTH_A", "MTH_E", "MTH_A", "MTH_I", 
				"MTH_E", "MTH_U", "MTH_E", "MTH_O", "MTH_I", 
				"MTH_E"
			};
				
			faceManager.playFacialAnimations(temp, 0.15f);
		}
	}
}
