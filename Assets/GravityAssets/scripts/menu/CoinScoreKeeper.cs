using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UniRx;

public class CoinScoreKeeper : UIGroupToggle {

	public Text ScoreLabel;

	// Use this for initialization
	void Start () {
		if (ScoreLabel == null)
			ScoreLabel = this.transform.Find ("CoinText").GetComponent<Text>();

		GlobalVariables.variablesRx.coins.SubscribeToText (ScoreLabel).AddTo(this);	
	}

}
