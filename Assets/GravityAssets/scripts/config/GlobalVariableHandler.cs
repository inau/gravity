using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalVariableHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Text t = transform.Find("MomentumLabel").GetComponent<Text>();
		t.text = "Momentum: " + GlobalVariables.GravityMomentum;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnCheckBoxUpdateBHole(bool v) {
		GlobalVariables.Spawn_Black_Holes = v;
		Debug.Log ("spawn changed to " + v);
	}

	public void OnCheckBoxUpdateHelper(bool v) {
		GlobalVariables.Show_Helper_Line = v;
		Debug.Log ("Helper shown " + v);
	}

	public void OnCheckBoxOverviewCamera(bool v) {
		GlobalVariables.Overview_Camera = v;
		Debug.Log ("overview changed to " + v);
	}

	public void OnWidthVal(string v) {
		GlobalVariables.Map_Width = int.Parse(v);
		Debug.Log ("Width to " + v);
	}

	public void OnHeightVal(string v) {
		GlobalVariables.Map_Height = int.Parse(v);
		Debug.Log ("Height changed to " + v);
	}
}
