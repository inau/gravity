using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CameraBehaviour : MonoBehaviour {

    [Range(1,25)]
	public float normal_distance = 15;

	[Range(1,50)]
	public float overview_distance = 25;

	[Range(1,25)]
	public float move_scale = 10f;

    bool overview = false, pos_updated = false;

	Vector3 normal_pos = Vector3.zero;
	Vector3 overview_pos = Vector3.zero;

	public Transform player;

	void UpdatePositionVectors() {
		//update cam vectors
		normal_pos = transform.position;
		normal_pos.z = -normal_distance;

		overview_pos = transform.position;
		overview_pos.z = -overview_distance;
	}

	void Start() {
		UpdatePositionVectors ();

		this.transform.position = overview ? overview_pos : normal_pos;	


	}

	void Update() {
		if (overview != GlobalVariables.Overview_Camera) {
			UpdatePositionVectors ();
			overview = GlobalVariables.Overview_Camera;
			return;
		}

		if (overview)
			TransformTo(overview_pos);
		else
			TransformTo(normal_pos);
	}

	void TransformTo(Vector3 target) {
		if (transform.position.z == target.z) {
			if (!pos_updated) {
				GlobalVariables.variablesRx.player_screen_coordinates.Value = Camera.current.WorldToScreenPoint( player.position );
				pos_updated = true;
			}
			return;
		}
		pos_updated = false;
		
		float step = move_scale * Time.deltaTime;
		this.transform.position = Vector3.MoveTowards(this.transform.position, target, step);
	}
		
}
