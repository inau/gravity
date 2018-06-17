using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubMenuVarLoader : UIGlobalVarLoader {

	private InputField w, h;
	private Toggle bht, ovt;

	// Use this for initialization
	void Start () {
		if (gameObject.activeSelf) {
			FindUiComponents ();
		}

	}
	void OnEnable() {
		if (w == null || h == null || bht == null || ovt == null) {
			FindUiComponents ();
		}
		LoadVariables ();
	}

	private void FindUiComponents() {
		w = transform.Find("MapW").GetComponent<InputField>();
		h = transform.Find("MapH").GetComponent<InputField>();
		bht = transform.Find("BlackHoleToggle").GetComponent<Toggle>();
	}
		
	#region implemented abstract members of UIGlobalVarLoader
	protected override void LoadVariables ()
	{
		w.text = ""+GlobalVariables.Map_Width;
		h.text = ""+GlobalVariables.Map_Height;

		bht.isOn = GlobalVariables.Spawn_Black_Holes;
	}
	#endregion
}
