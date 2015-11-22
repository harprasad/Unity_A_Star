using UnityEngine;
using System.Collections;

public class rotate_camera : MonoBehaviour {
	public Transform look_at;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (look_at);
		transform.Translate (Vector3.right * Time.deltaTime*4);
	}
}
