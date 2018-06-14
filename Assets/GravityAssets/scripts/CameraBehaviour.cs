﻿using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public GameObject follow_target;
    public float normal_dist, overview_distance, y_offset;
    public bool overview = false;

	void Update() {
		//UpdateWithRot ();
	}

	// Update is called once per frame
	void UpdateNoRot () {
        Vector3 pos = follow_target.transform.position;
		pos.y += y_offset;
        pos.z = overview ? overview_distance : normal_dist;
        transform.position = pos;
    }

	void UpdateWithRot () {
//		transform = follow_target.transform;
	}

	public void setOverview(bool b) {
		overview = b;
		var v = this.transform.position;
			v.z = overview ? overview_distance : normal_dist;
		this.transform.position = v;
	}
}
