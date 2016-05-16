using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class interactCube : MonoBehaviour {

	public Camera MainCamera;
	public GameObject GroundObject;
	public Text CubeDebugText;

	private bool touchedCube;
	private int printCount;
	// Use this for initialization
	void Start () {
		printCount = 0;
		CubeDebugText = CubeDebugText.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (printCount % 3 == 0) {
			CubeDebugText.text = "";
		}
		if ( Input.GetMouseButtonDown (0) ){
			Ray ray = this.MainCamera.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll (this.MainCamera.transform.position, ray.direction);
			foreach (RaycastHit hit in hits) {
				if (hit.collider == this.GetComponent<Collider> ()) {
					this.touchedCube = true;
					CubeDebugText.text += printCount + " MOUSE TOUCH\n";
					printCount++;
				}
			}

		}

		if (Input.touchCount > 0) {
			Ray ray = this.MainCamera.ScreenPointToRay (Input.touches [0].position);
			RaycastHit[] hits = Physics.RaycastAll (this.MainCamera.transform.position, ray.direction);
			switch (Input.touches [0].phase) {
			case TouchPhase.Began:
				foreach (RaycastHit hit in hits) {
					if (hit.collider == this.GetComponent<Collider> ()) {
						this.touchedCube = true;
						CubeDebugText.text += printCount + " TOUCH\n";
						printCount++;
					}
				}
				break;

			case TouchPhase.Stationary:
			case TouchPhase.Moved:
				if (this.touchedCube) {
					foreach (RaycastHit hit in hits) {
						if (hit.collider == this.GroundObject.GetComponent<Collider> ()) {
							this.gameObject.transform.position = hit.point;
						}
					}
				}
				break;

			case TouchPhase.Ended:
			case TouchPhase.Canceled:
				this.touchedCube = false;
				break;
			}
		}
	}
}
