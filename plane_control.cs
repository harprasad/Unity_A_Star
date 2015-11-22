
using UnityEngine;
using System.Collections;      
public class plane_control : MonoBehaviour {
	public float control_quality = 6f;
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
		//rotate as per vertical force
		//gameObject.transform.RotateAround (transform.position, Vector3.forward, horizontal_f*control_quality);
		Vector3 new_eulanges = default_rotation;
		new_eulanges.z -= horizontal_f * control_quality;
		transform.Rotate (new_eulanges);
		//rbody.AddRelativeForce (horizontal_f * control_quality, 0, vertical_f * control_quality);
		if (thruster_on) {
			rbody.AddRelativeForce(Vector3.forward*control_quality); //force on y direction
			//upward lift is relative to forward velocity
			float forward_speed = Mathf.Abs( transform.InverseTransformPoint (rbody.velocity).z);
			print ("forward speed = "+forward_speed.ToString());
			rbody.AddForce(Vector3.up*(forward_speed*0.02f));
		}

	}
	void auto_controls()
	{
		if (Mathf.Abs (horizontal_f) > 0.005)
			return; //if user has pressed a button do not control
		Quaternion current_rot = transform.rotation;
		Quaternion new_rot = current_rot;
		Vector3 cu_eul_angle = new_rot.eulerAngles;
		cu_eul_angle.x = default_rotation.x;
		cu_eul_angle.z = default_rotation.z;
		new_rot.eulerAngles = cu_eul_angle;
		transform.rotation = Quaternion.Lerp (current_rot, new_rot, 0.2f);
	}
}
