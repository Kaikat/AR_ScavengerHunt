using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	//"Oh good! I'm so glad I found you!"
	void Animation0()
	{
		anim.Play ("HAPPY_SHY", -1, 0f);
	}

	//"The Santa Barbara Zoo animals have escaped and I need your help to find them."
	void Animation1()
	{
		anim.CrossFade ("WAIT00", 0.3f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"Oh! I didn't even introduce myself. Sorry! My name is Katalie. Nice to meet you!"
	void Animation2()
	{
		anim.CrossFade ("POSE01", 0.3f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"Anyway, this is urgent. We need to find them before the zoo keepers do so we can 
	//return them to their home in the wild, otherwise they will be put back in their cages."
	void Animation3()
	{
		anim.CrossFade("WAIT00", 0.3f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"I don’t have many clues to their whereabouts but maybe if you find one of them, it can lead you to the rest."
	void Animation4()
	{
		anim.CrossFade("LOSE00", 0.3f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"I heard people have been avoiding a dining area near engineering because they have
	//been finding trash scattered all over the floor in the mornings."
	void Animation5()
	{
		anim.CrossFade ("WIN00", 0.3f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"You should start there. Go take a look, maybe an animal has been looking around for food at night 
	//and is unintentionally scaring people away."
	void Animation6()
	{
		anim.CrossFade ("POSE04", 0.1f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"Here, take this radar, it will help you sense and see animals that are close to you more easily."
	void Animation7()
	{
		anim.CrossFade ("WAIT00", 2.0f);
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}

	//"Just touch the radar when you think you are in the area where an animal may be. The radar will let 
	//you know if it senses an animal nearby and will offer a magic lens to look through so you can spot it."
	void Animation8()
	{
		SendMessage ("AnimateFace_Talk1", SendMessageOptions.DontRequireReceiver);
	}
}
