using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    public GameObject follow_target;
    public float normal_dist, overview_distance;
    public bool overview = false;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = follow_target.transform.position;
        pos.z = overview ? overview_distance : normal_dist;
        transform.position = pos;
    }
}
