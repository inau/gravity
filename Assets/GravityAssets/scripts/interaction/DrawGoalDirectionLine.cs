using UnityEngine;
using System;
using System.Collections.Generic;
using UniRx;

public class DrawGoalDirectionLine : MonoBehaviour {

	public GameObject Player, Goal = null;
	public RectTransform Visual;
	public Queue<GameObject> goals = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
		MessageBroker.Default.Receive<TriggerConditionRx.LevelGenerationEventArgs> ()
			.Where( x => x.block_type == GravityLevels.MapBlock.GOAL )
			.Subscribe (x => this.PushGoal (x.block) )
			.AddTo (this);

		MessageBroker.Default.Receive<TriggerConditionRx.CollisionEventArgs> ()
			.Where( x => x.collision_event_type == TriggerConditionRx.CollisionEventType.WIN )
			.Subscribe (x => this.NextGoal() )
			.AddTo (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (Goal != null) {
			Visual.transform.rotation = Quaternion.Euler(Vector3.zero) * GetRotation ();
		} else
			Visual.transform.rotation = GetDownwar ();
	}

	public void PushGoal(GameObject goal) {
		goals.Enqueue (goal);
	}

	public void NextGoal() {
		this.Goal = goals.Count > 0 ? goals.Dequeue() : null;
	}

	public Quaternion GetRotation() {
		var r = Quaternion.FromToRotation (Player.transform.position, (Player.transform.position - Goal.transform.position) );
		return r;
	}

	public Quaternion GetDownwar() {
		var inv = Quaternion.Inverse( Player.transform.rotation );
		return inv;
	}
}
