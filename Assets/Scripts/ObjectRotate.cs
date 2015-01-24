using UnityEngine;
using System.Collections;

public class ObjectRotate : MonoBehaviour {

	public Vector3 rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (rotationSpeed * Time.deltaTime);
	}
}
