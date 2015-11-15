using UnityEngine;
using System.Collections;

public class plane_control : MonoBehaviour {
	public float control_quality = 10f;
	public GameObject left_thruster;
	public GameObject right_thruster;
	private Rigidbody rbody;
	private Vector3 default_rotation;
	float horizontal_f;
	float vertical_f;
	bool thruster_on;
	// Use this for initialization
	void Start () {
		rbody = (Rigidbody)gameObject.GetComponent ("Rigidbody");
		default_rotation = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		get_usr_inputs ();
		act_on_body ();
		auto_controls ();
	
	}
	
	void get_usr_inputs(){
		horizontal_f = Input.GetAxis ("Horizontal");
		vertical_f = Input.GetAxis ("Vertical");
		thruster_on = Input.GetKey (KeyCode.Space);
	}
	void act_on_body()
	{
		//print ("hori value is " + horizontal_f.ToString ());
		//print ("verti value is " + vertical_f.ToString ());
		rbody.AddForce (horizontal_f* control_quality, 0, vertical_f * control_quality);
		if (thruster_on) {
			rbody.AddForce(Vector3.up*control_quality);
		}
	}
	void auto_controls()
	{
		Quaternion current_rot = transform.rotation;
		Quaternion new_rot = current_rot;
		Vector3 cu_eul_angle = new_rot.eulerAngles;
		cu_eul_angle.x = default_rotation.x;
		cu_eul_angle.z = default_rotation.z;
		new_rot.eulerAngles = cu_eul_angle;
		transform.rotation = Quaternion.Lerp (current_rot, new_rot, 2f);
	}
}
