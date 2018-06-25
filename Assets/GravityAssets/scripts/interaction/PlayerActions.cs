using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

public class PlayerActions : MonoBehaviour {

	Animator anim;
	Vector3 defaultPos;
	Rigidbody2D rb;
	Vector3 nrm_sc;
	Quaternion base_rot;

	// Use this for initialization
	void Start () {
		base_rot = transform.rotation;
		nrm_sc = transform.localScale;
		defaultPos = transform.position;

		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

		MessageBroker.Default.Receive<TriggerConditionRx.GameStateEventArgs> ()
			.Subscribe (x => this.OnGameEventArgs (x))
			.AddTo (this);
	}

	public void OnGameEventArgs(TriggerConditionRx.GameStateEventArgs args) {
		switch (args.game_event_type) {
		case TriggerConditionRx.GameStateEventType.MENU:
			rb.isKinematic = true;
			break;
		case TriggerConditionRx.GameStateEventType.START_GAME:
			rb.isKinematic = false;
			break;
		default:
			break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void reset() {
		anim.Play("player_default");
		this.transform.rotation = base_rot;
		this.transform.position = defaultPos;
		this.transform.localScale = nrm_sc;
	}

	public void setGravity(bool active) {
		
	}

	public void HandleCollisionEvent( TriggerConditionRx.CollisionEventType cet ) {
		switch (cet) {
		case TriggerConditionRx.CollisionEventType.LOSE:
			OnDeath ();
			break;
		case TriggerConditionRx.CollisionEventType.WIN:
			OnWin ();
			break;
		default:
			break;
		}
	}

	void OnDeath() {
		anim.Play("player_death");
	}

	//death animation hook
	void onDeath() {
		var gea = new TriggerConditionRx.GameStateEventArgs{ 
			game_event_type = TriggerConditionRx.GameStateEventType.START_GAME,
			label = (int)TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME.END
		};

		MessageBroker.Default.Publish ( gea );
	}

	public void OnWin() {
		var gea = new TriggerConditionRx.GameStateEventArgs{ 
			game_event_type = TriggerConditionRx.GameStateEventType.START_GAME,
			label = (int)TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME.NEW
		};

		MessageBroker.Default.Publish ( gea );
	}
}
