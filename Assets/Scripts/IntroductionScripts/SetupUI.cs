using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace StructureAR {
	public class SetupUI : MonoBehaviour {

		public Button actionButton;
		public RectTransform dialogPanel;

		public Button dialogButton;
		public Text dialogText;
		public GameObject backgroundQuad;
		public GameObject unitychan_dynamic_locomotion;
		public GameObject Cube;
		public Button resetCubeButton;
		//public GameObject borderQuad;

		public Manager ManagerS;

		private Vector2 panelSize;
		private Vector3 actionButtonPos;

		void Start () 
		{
			actionButton = actionButton.GetComponent<Button> ();
			dialogPanel = dialogPanel.GetComponent<RectTransform> ();
			backgroundQuad.GetComponent<GameObject> ();
			unitychan_dynamic_locomotion.GetComponent<GameObject> ();
			Cube.GetComponent<GameObject> ();

			resetCubeButton = resetCubeButton.GetComponent<Button> ();

			ManagerS = ManagerS.GetComponent<Manager> ();
			//borderQuad.GetComponent<GameObject> ();

			//borderQuad.transform.localScale = new Vector3 (Camera.main.orthographicSize * 2.0f * Screen.width/Screen.height, Camera.main.orthographicSize * 2.0f, 1.0f);
			//borderQuad.transform.localScale = new Vector3(8.25f * Screen.width/Screen.height, 1.0f * Screen.height, 1.0f);
			//borderQuad.SetActive (false);

			actionButton.enabled = false;
			actionButton.image.enabled = false;
			actionButton.GetComponentInChildren<Text>().text = "";

			resetCubeButton.enabled = false;
			resetCubeButton.image.enabled = false;
			resetCubeButton.GetComponentInChildren<Text> ().text = "";

			actionButtonPos = actionButton.transform.position;
			Vector3 pos = actionButton.transform.position;
			pos.x = 5000f;
			actionButton.transform.position = pos;

			//actionButton.interactable = false;
			//actionButton.image.raycastTarget = false;

			panelSize = dialogPanel.sizeDelta;
			dialogPanel.sizeDelta = new Vector2 (0.0f, 0.0f);

			dialogText = dialogText.GetComponent<Text> ();
			dialogButton = dialogButton.GetComponent<Button> ();
		}

		public void Scene2 ()
		{
			dialogButton.image.enabled = false;
			dialogButton.enabled = false;
			dialogText.enabled = false;
			backgroundQuad.SetActive (false);
			unitychan_dynamic_locomotion.SetActive (false);

			dialogPanel.sizeDelta = panelSize;
			actionButton.enabled = true;
			actionButton.image.enabled = true;
			actionButton.GetComponentInChildren<Text>().text = "\nScan\nfor\nAnimal";

			resetCubeButton.enabled = true;
			resetCubeButton.image.enabled = true;
			resetCubeButton.GetComponentInChildren<Text> ().text = "Reset\nCube\nPosition";

			actionButton.transform.position = actionButtonPos;

			//actionButton.interactable = true;
			//actionButton.image.raycastTarget = true;


			//borderQuad.SetActive (true);

			ManagerS.SendMessage("structureGame", SendMessageOptions.DontRequireReceiver);
			SendMessage ("structureGame", SendMessageOptions.DontRequireReceiver);
			moveAcross.startARGame = true;
		}
	
		void Update () 
		{
	
		}
	}
}