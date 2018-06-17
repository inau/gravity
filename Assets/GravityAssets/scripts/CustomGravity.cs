using UnityEngine;
using System.Collections;

public class CustomGravity : MonoBehaviour {
	float gravity_magnitude = 0.5f;
	Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		gravity_magnitude = GlobalVariables.GravityMomentum;
		Debug.Log ("Player Grav = " + gravity_magnitude);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 g = ( gravity_magnitude * -this.transform.up);
		rb2d.AddForce ( g , ForceMode2D.Impulse );
	}
}
