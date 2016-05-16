using UnityEngine;
using System.Collections;

//only x and z
namespace StructureAR
{
	public class moveAcross : MonoBehaviour {

		public static bool startARGame = false;

		public GameObject Cube;
		private Vector3 initialPos;
		public Camera camera;

		// Use this for initialization
		void Start () 
		{
			Cube = Cube.GetComponent<GameObject> ();
			initialPos = this.transform.position;

			camera = camera.GetComponent<Camera> ();
		}
	
		// Update is called once per frame
		void Update () 
		{
			if (!startARGame)
				return;

			Vector3 movePos = new Vector3 (this.transform.position.x + 0.005f, this.transform.position.y, this.transform.position.z);
			//Vector3 screenPos = camera.WorldToScreenPoint (movePos);
			this.transform.position = movePos;


			/*if (movePos.x > Screen.currentResolution.width)
				movePos.x -= 0.01f;
			if (movePos.z > Screen.currentResolution.height)
				movePos.z -= 0.01f;
			if (movePos.x < Screen.currentResolution.width)
				movePos.x += 0.01f;
			if (movePos.y < Screen.currentResolution.height)
				movePos.y += 0.01f;

*/
			//print ("ScreenPos: " + screenPos);

		}

		public void onClickResetCubePosition ()
		{
			this.transform.position = initialPos;
		}
	}
}