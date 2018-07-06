using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class TriggerHandlerRx : MonoBehaviour {
	public PlayerActions play;
	public MapLoader loader;

	// Use this for initialization
	void Start () {
		//loader = GetComponentInChildren<MapLoader>();

		MessageBroker.Default.Receive<TriggerConditionRx.CollisionEventArgs> ()
			.Subscribe (x => this.OnCollisionEventArgs (x))
			.AddTo (this);

		MessageBroker.Default.Receive<TriggerConditionRx.GameStateEventArgs> ()
			.Subscribe (x => this.OnGameEventArgs (x))
			.AddTo (this);
	}
	
	// Update is called once per frame
	TriggerConditionRx.CollisionEventArgs evt = null;

	bool EqualPositions(Vector3 a, Vector3 b) {
		return (a.x.Equals(b.x) && a.y.Equals(b.y));
	}

	// scale factor
	Vector3 sc = new Vector3(.996f, .996f, 1f);

	void Update () {
		// if event is set we have a win/lose conditions
		if (evt != null) {
			float step = 0.5f * Time.deltaTime;

			if(evt.collision_event_type == TriggerConditionRx.CollisionEventType.LOSE){
				Vector3 sc_c = evt.player.transform.localScale;
				sc_c.Scale (sc);
				evt.player.transform.localScale = sc_c;				
			}

			evt.player.attachedRigidbody.velocity.Set(0f,0f);
			evt.player.attachedRigidbody.bodyType = RigidbodyType2D.Static;
			evt.player.transform.position =
				Vector2.MoveTowards(
					evt.player.transform.position,
					evt.trigger.transform.position,
					step
				);
			//terminating condition
			if (EqualPositions (
				    evt.player.transform.position,
				    evt.trigger.transform.position)) {

				//delegate to post collision function
				evt.player.GetComponentInParent<PlayerActions> ()
					.HandleCollisionEvent(evt.collision_event_type);
				
				evt = null;
			}

		}
	}

	public void OnStartGame(TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME lbl) {
		switch (lbl) {
		case TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME.NEW:
			Debug.Log ("NEW");
			var gsgm = GravityLevels.Generation.GenerateSingleGoalMap (GlobalVariables.levelPrefsRx.width.Value, GlobalVariables.levelPrefsRx.height.Value);
			play.reset ();
			loader.LoadMap (gsgm);
			//SceneManager.LoadScene (curr_scene);
			break;
		case TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME.CONTINUE:
			Debug.Log ("Continue");
			//SceneManager.LoadScene (curr_scene);
			break;
		case TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME.END:
			loader.ResetMap ();
			play.reset ();
			break;
		default:
			break;	
		}
	}

	public void OnMenu(TriggerConditionRx.GameStateEventArgs.LABELS.MENU lbl) {
		switch (lbl) {
		case TriggerConditionRx.GameStateEventArgs.LABELS.MENU.MAIN:
			Debug.Log ("MainMenu");
			break;
		case TriggerConditionRx.GameStateEventArgs.LABELS.MENU.OPTIONS:
			Debug.Log ("Options");
			break;
		default:
			break;	
		}
	}

	public void OnGameEventArgs(TriggerConditionRx.GameStateEventArgs args) {
		Debug.Log ("OnGameEvent");

		switch (args.game_event_type) {
		case TriggerConditionRx.GameStateEventType.MENU:
			break;
		case TriggerConditionRx.GameStateEventType.START_GAME:
			OnStartGame ((TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME)args.label);
			break;
		case TriggerConditionRx.GameStateEventType.END_GAME:

			break;
		default:
			break;
		}
	}

	public void OnCollisionEventArgs(TriggerConditionRx.CollisionEventArgs args) {
		switch (args.collision_event_type) {
		case TriggerConditionRx.CollisionEventType.WIN:
			Debug.Log ("Win");
			if (GlobalVariables.variablesRx.mode.Value == Global.Enumerations.GameMode.SCAVENGER && GlobalVariables.variablesRx.coins.Value > 0 )
				return;
			evt = args;
			break;
		case TriggerConditionRx.CollisionEventType.LOSE:
			Debug.Log ("Lose");
			evt = args;
			break;
		case TriggerConditionRx.CollisionEventType.POWERUP:
			Debug.Log ("Coin");
			Destroy (args.trigger.gameObject);
			GlobalVariables.variablesRx.coins.Value--;
			break;
		default:
			Debug.Log ("Unknown Event Caught " + args.collision_event_type);
			break;
		}
	}
}
