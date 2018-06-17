using UnityEngine;
using System.Collections;

public class DrawGoalDirectionLine : MonoBehaviour {

	public GameObject Player, Goal = null;
	LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer>();
		if (lineRenderer == null)
			lineRenderer = this.gameObject.AddComponent<LineRenderer> ();
		lineRenderer.useWorldSpace = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Goal != null) {
			//Debug.Log ("" + Player.transform.position + " to " + Goal.transform.position);
			var vs = new Vector3[]{Player.transform.position, Goal.transform.position};
			lineRenderer.SetPositions(vs);
		}
	}

	public void SetGoal(GameObject goal) {
		this.Goal = goal;
	}
}
