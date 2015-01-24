using UnityEngine;
using System.Collections;

public class SkyboxCamera : MonoBehaviour {

	public GameObject mainCamera;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = mainCamera.transform.rotation;
	}
}
