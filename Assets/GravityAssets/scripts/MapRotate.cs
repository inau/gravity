using UnityEngine;
using System.Collections;

public class MapRotate : MonoBehaviour {

	public Transform map;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setTransform(float angle) {
		if (angle > 360f || -360f > angle)
			return;
		else map.transform.eulerAngles = new Vector3 (map.eulerAngles.x, map.eulerAngles.y, angle);
	}

}
