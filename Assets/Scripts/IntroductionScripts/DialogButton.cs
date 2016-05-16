using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogButton : MonoBehaviour {

	public Canvas structureARCanvas;
	public Text dialogText;
	public GameObject unitychan;

	private string[] dialogMessages;
	private int dialogMessageCounter;

	// Use this for initialization
	void Start () 
	{
		structureARCanvas = structureARCanvas.GetComponent<Canvas> ();
		dialogText = dialogText.GetComponent<Text> ();
		unitychan.GetComponent<GameObject> ();
			
		dialogMessageCounter = -1;
		dialogMessages = new string[9];
		loadDialogMessages ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick ()
	{
		if (dialogMessageCounter < (dialogMessages.Length - 1)) 
		{
			dialogMessageCounter++;
			dialogText.text = dialogMessages [dialogMessageCounter];
			ResetText ();
			unitychan.SendMessage ("Animation" + dialogMessageCounter, SendMessageOptions.DontRequireReceiver);
			//UtilityScript.printMessage ("dialogMessageCounter = " + dialogMessageCounter); 
		}
		else 
		{
			//UtilityScript.printMessage ("Next Scene: Scan For Animal");
			//SceneManager.LoadScene("ScanForAnimal");
			//SceneManager.LoadScene("GPSTest");
			//SceneManager.LoadScene("AnimalScan");
			structureARCanvas.SendMessage("Scene2", SendMessageOptions.DontRequireReceiver);			
		}
	}

	public void ResetText(bool deleteFontCharacterInfo = false)
	{
		if (dialogText)
		{
			if (deleteFontCharacterInfo)
			{
				dialogText.font.characterInfo = null;
			}

			dialogText.font.RequestCharactersInTexture(dialogText.text, dialogText.fontSize, dialogText.fontStyle);
			dialogText.FontTextureChanged();
		}
	}

	void loadDialogMessages ()
	{
		dialogMessages [0] = "Oh good! I'm so glad I found you!";
		dialogMessages [1] = "The Santa Barbara Zoo animals have escaped and I need your help to find them.";
		dialogMessages [2] = "Oh! I didn't even introduce myself. Sorry! My name is Katalie. Nice to meet you!";
		dialogMessages [3] = "Anyway, this is urgent. We need to find them before the zoo keepers do so we can " +
			"return them to their home in the wild, otherwise they will be put back in their cages. \n";
		dialogMessages [4] = "I don’t have many clues to their whereabouts but maybe if you find one of them, it can lead you to the rest.";
		dialogMessages [5] = "I heard people have been avoiding a dining area near engineering because they have " +
			"been finding trash scattered all over the floor in the mornings.";
		dialogMessages [6] = "You should start there. Go take a look, maybe an animal has been looking around for food at night " +
			"and is unintentionally scaring people away.\n";
		dialogMessages [7] = "Here, take this radar, it will help you sense and see animals that are close to you more easily.";
		dialogMessages [8] = "Just touch the radar when you think you are in the area where an animal may be. The radar will let " +
			"you know if it senses an animal nearby and will offer a magic lens to look through so you can spot it.\n";
	}
}
