using UnityEngine;
using System.Collections;

public class UIGroupToggle : MonoBehaviour {

	public void Toggle() {
		this.gameObject.SetActive (!this.gameObject.activeSelf);
	}

	public void SetVisibility(bool show) {
		this.gameObject.SetActive (show);
	}
}
