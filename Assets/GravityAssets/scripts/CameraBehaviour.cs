using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public GameObject follow_target;
    public float normal_dist, overview_distance, y_offset;
    public bool overview = false;

	void Update() {
		if (overview != GlobalVariables.Overview_Camera) {
			setOverview (GlobalVariables.Overview_Camera);
		}
	}
			
	public void setOverview(bool b) {
		overview = b;
		var v = this.transform.position;
		v.z = overview ? overview_distance : normal_dist;
		this.transform.position = v;
	}
}
