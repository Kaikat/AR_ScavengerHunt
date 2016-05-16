using UnityEngine;
using System.Collections;

namespace UnityChan
{
	public class FaceAnimationManager : MonoBehaviour {
	
		Animator anim;
		public AnimationClip[] animations; //don't need
		public float delayWeight;
		public bool isKeepFace = false;

		private int currentClip;
		private int numClips;
		private float minimumTime;
		private string[] clipNames;

		float current = 0;

		void Start ()
		{
			anim = GetComponent<Animator> ();
			currentClip = 0;
			numClips = 0;
			minimumTime = 0.15f;
		}

		public void playFacialAnimations (string[] clippies, float minTime)
		{
			currentClip = 0;
			minimumTime = minTime;
			clipNames = (string[]) clippies.Clone();
			numClips = clipNames.Length;
		}

		void Update ()
		{
			current = Mathf.Lerp (current, 0, delayWeight);
			anim.SetLayerWeight (1, current);

			if (current <= minimumTime && currentClip < numClips) {
				currentClip++;
			}

			TalkAnimation ();

		}
			
		void ChangeFace (string str)
		{
			isKeepFace = true;
			current = 1;
			anim.CrossFade (str, 0);
		}

		void TalkAnimation()
		{
			if (current <= minimumTime && currentClip < numClips) {
				ChangeFace (clipNames[currentClip]);
			}
		}

	} //End FaceAnimationManager
}












/*void OnGUI ()
		{
			GUILayout.Box ("Face Update", GUILayout.Width (170), GUILayout.Height (25 * (animations.Length + 2)));
			Rect screenRect = new Rect (10, 25, 150, 25 * (animations.Length + 1));
			GUILayout.BeginArea (screenRect);
			foreach (var animation in animations) {
				if (GUILayout.RepeatButton (animation.name)) {
					anim.CrossFade (animation.name, 0);
				}
			}
			isKeepFace = GUILayout.Toggle (isKeepFace, " Keep Face");
			GUILayout.EndArea ();
		}*/

/*
			//The Old Update
if (Input.GetMouseButton (0)) {
	current = 1;
} else if (!isKeepFace) {
	current = Mathf.Lerp (current, 0, delayWeight);
}

anim.SetLayerWeight (1, current);
*/

//アニメーションEvents側につける表情切り替え用イベントコール
/*public void OnCallChangeFace (string str)
		{   
			int ichecked = 0;
			foreach (var animation in animations) {
				if (str == animation.name) {
					ChangeFace (str);
					break;
				} else if (ichecked <= animations.Length) {
					ichecked++;
				} else {
					//str指定が間違っている時にはデフォルトで
					str = "default@unitychan";
					ChangeFace (str);
				}
			} 
		}*/


/*
		//"The Santa Barbara Zoo animals have escaped and I need your help to find them."
void Animation1b ()
{
	currentClip = 0;
	string[] temp = { "MTH_E", "MTH_A", "MTH_A", "MTH_A", "MTH_O", 
		"MTH_A", "MTH_I", "MTH_A", "MTH_A", "MTH_E", 
		"MTH_E", "MTH_A", "MTH_E", "MTH_A", "MTH_I", 
		"MTH_E", "MTH_U", "MTH_E", "MTH_O", "MTH_I", 
		"MTH_E"
	};

	clipNames = new string[temp.Length];

	for (int i = 0; i < temp.Length; i++) {
		clipNames [i] = temp [i];
	}

	numClips = clipNames.Length;
}

//"Oh! I didn't even introduce myself. Sorry! My name is Katalie. Nice to meet you!"
void Animation2b ()
{
	currentClip = 0;
	string[] temp = { "MTH_O", "MTH_I", "MTH_I", "MTH_I", "MTH_E", 
		"MTH_E", "MTH_I", "MTH_O", "MTH_U", "MTH_E",
		"MTH_I", "MTH_E", "MTH_O", "MTH_I", "MTH_I", 
		"MTH_A", "MTH_E", "MTH_I", "MTH_A", "MTH_I", 
		"MTH_I", "MTH_I", "MTH_O","MTH_E", "MTH_U"
	};

	clipNames = new string[temp.Length];

	for (int i = 0; i < temp.Length; i++) {
		clipNames [i] = temp [i];
	}

	numClips = clipNames.Length;
}

//"Anyway, this is urgent. We need to find them before the zoo keepers do so we can 
//return them to their home in the wild, otherwise they will be put back in their cages."
void Animation3b ()
{
	currentClip = 0;
	string[] temp = { "angry2@unitychan", "angry2@unitychan", "MTH_I", "MTH_I", "MTH_U", 
		"MTH_E", "MTH_E", "MTH_E", "MTH_O", "MTH_I",
		"MTH_E", "MTH_E", "MTH_O", "MTH_E", "MTH_O", 
		"MTH_E", "MTH_E", "MTH_O", "MTH_O", "MTH_E", 
		"MTH_A", "MTH_E", "MTH_U","MTH_E", "MTH_O", 
		"MTH_I", "MTH_O", "MTH_I", "MTH_E", "MTH_I",
		"angry2@unitychan", "MTH_I", "MTH_E", "MTH_I", "MTH_E",
		"MTH_U", "angry2@unitychan", "MTH_I", "MTH_E", "angry2@unitychan"
	};

	clipNames = new string[temp.Length];

	for (int i = 0; i < temp.Length; i++) {
		clipNames [i] = temp [i];
	}

	numClips = clipNames.Length;
}


*/

//"I don’t have many clues to their whereabouts but maybe if you find one of them, it can lead you to the rest."

//"I heard people have been avoiding a dining area near engineering because they have
//been finding trash scattered all over the floor in the mornings."

//"You should start there. Go take a look, maybe an animal has been looking around for food at night 
//and is unintentionally scaring people away."

//"Here, take this radar, it will help you sense and see animals that are close to you more easily."

//"Just touch the radar when you think you are in the area where an animal may be. The radar will let 
//you know if it senses an animal nearby and will offer a magic lens to look through so you can spot it."