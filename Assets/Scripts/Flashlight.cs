using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	private bool lit = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame

	void Update () {
		if (Input.GetKeyDown ("l")) {
			lit = !lit;
			light.enabled = lit;
		}
		if(Input.GetKeyDown (KeyCode.Escape)){
			Application.Quit();
		}
	}
}
