using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

public class TriggerConditionRx : MonoBehaviour {

	public enum GameStateEventType {
		MENU,
		START_GAME,
		END_GAME
	}

	public enum CollisionEventType
	{
		WIN,
		CHECKPOINT,
		LOSE,
		POWERUP
	}

	public class LevelGenerationEventArgs {
		public GravityLevels.MapBlock block_type { get; set; }
		public GameObject block { get; set; }
	}

	public class GameStateEventArgs : EventArgs {
		public GameStateEventType game_event_type { get; set; }
		public int label { get; set; }

		public static class LABELS {
			public enum MENU {
				MAIN, OPTIONS
			};
			public enum START_GAME {
				NEW, CONTINUE, END
			};
		}
	}

	public class CollisionEventArgs : EventArgs {
		public CollisionEventType collision_event_type { get; set; }
		public Collider2D player { get; set; }
		public Collider2D trigger { get; set; }
	}

	public CollisionEventType event_type = CollisionEventType.WIN;
	Collider2D collider_object;

	// Use this for initialization
	void Start () {
		collider_object = this.GetComponent<Collider2D>();
		gameObject.OnTriggerEnter2DAsObservable ()
			.TakeUntilDisable(this)
			.Where( x => x.name == "player" )
			.Subscribe( x => this.OnPlayerCollision(x) )
			.AddTo(gameObject);
	}

	void OnPlayerCollision(Collider2D player) {
		var gea = new CollisionEventArgs{ 
			collision_event_type = event_type,
			player = player,
			trigger = collider_object
		};
		MessageBroker.Default.Publish ( gea );
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
