using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class Variables : MonoBehaviour {
	public PlayerActions player;

	public Text MomentumLabel;
	public Toggle overviewToggle;

	public InputField widthInput, heightInput;

	public UIGroupToggle MainMenuToggle;
	public Button Level, Puzzle, Endless, Scavenger, reset;


	// Use this for initialization
	void Start () {
		if (MomentumLabel == null)
			MomentumLabel = this.transform.Find ("MomentumLabel").GetComponent<Text>();

		if (overviewToggle == null)
			overviewToggle = this.transform.Find ("OverViewToggle").GetComponent<Toggle>();

		if (widthInput == null)
			widthInput = this.transform.Find ("MainMenu/SubMenu/Dev/MapW").GetComponent<InputField>();

		if (heightInput == null)
			heightInput = this.transform.Find ("MainMenu/SubMenu/Dev/MapH").GetComponent<InputField>();

		//main menu
		if (Level == null)
			Level = this.transform.Find ("MainMenu/Level").GetComponent<Button>();

		if (Puzzle == null)
			Puzzle = this.transform.Find ("MainMenu/Puzzle").GetComponent<Button>();

		if (Endless == null)
			Endless = this.transform.Find ("MainMenu/Endless").GetComponent<Button>();

		if (Scavenger == null)
			Scavenger = this.transform.Find ("MainMenu/Scavenger").GetComponent<Button>();

		if (reset == null)
			reset = this.transform.Find ("MainMenu/Reset").GetComponent<Button>();

		if(MainMenuToggle == null)
			MainMenuToggle = this.transform.Find ("MainMenu").GetComponent<UIGroupToggle>();

		Level.OnClickAsObservable ().Subscribe (_ => GlobalVariables.variablesRx.mode.Value = Global.Enumerations.GameMode.LEVELS).AddTo (Level);
		Puzzle.OnClickAsObservable ().Subscribe (_ => GlobalVariables.variablesRx.mode.Value = Global.Enumerations.GameMode.PUZZLE).AddTo (Puzzle);
		Endless.OnClickAsObservable ().Subscribe (_ => GlobalVariables.variablesRx.mode.Value = Global.Enumerations.GameMode.ENDLESS).AddTo (Endless);
		Scavenger.OnClickAsObservable ().Subscribe (_ => GlobalVariables.variablesRx.mode.Value = Global.Enumerations.GameMode.SCAVENGER).AddTo (Scavenger);
		reset.OnClickAsObservable ().Subscribe (_ => player.reset() ).AddTo (reset);

		GlobalVariables.variablesRx.mode.Subscribe (x => this.SetColor(x == Global.Enumerations.GameMode.LEVELS, Level) ).AddTo(Level);
		GlobalVariables.variablesRx.mode.Subscribe (x => this.SetColor(x == Global.Enumerations.GameMode.PUZZLE, Puzzle) ).AddTo(Puzzle);
		GlobalVariables.variablesRx.mode.Subscribe (x => this.SetColor(x == Global.Enumerations.GameMode.ENDLESS, Endless) ).AddTo(Endless);
		GlobalVariables.variablesRx.mode.Subscribe (x => this.SetColor(x == Global.Enumerations.GameMode.SCAVENGER, Scavenger) ).AddTo(Scavenger);

		//hook rx components into
		GlobalVariables.variablesRx.player_screen_coordinates.SubscribeToText (MomentumLabel).AddTo(MomentumLabel);

		GlobalVariables.preferencesRx.camera_overview.Subscribe(x => overviewToggle.isOn = x ).AddTo(overviewToggle);

		GlobalVariables.levelPrefsRx.width.SubscribeToText (widthInput.textComponent).AddTo (widthInput.textComponent);
		GlobalVariables.levelPrefsRx.height.SubscribeToText (heightInput.textComponent).AddTo (heightInput.textComponent);
	
		widthInput.OnEndEditAsObservable ().Subscribe (x => GlobalVariables.levelPrefsRx.width.Value = int.Parse(x) ).AddTo (widthInput);
		heightInput.OnEndEditAsObservable ().Subscribe (x => GlobalVariables.levelPrefsRx.height.Value = int.Parse(x) ).AddTo (widthInput);
	}
		
	void SetColor(bool selected, Button v) {
			ColorBlock cb = v.colors;
			cb.normalColor = selected ? Color.cyan : Color.white;
			cb.highlightedColor = selected ? Color.white : Color.cyan;
			v.colors = cb;

			if (selected) {
				MainMenuToggle.Toggle ();
				TriggerConditionRx.GameStateEventArgs gea = new TriggerConditionRx.GameStateEventArgs();
				gea.game_event_type = TriggerConditionRx.GameStateEventType.START_GAME;
				gea.label = (int)TriggerConditionRx.GameStateEventArgs.LABELS.START_GAME.NEW;

				MessageBroker.Default.Publish( gea );
			}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
