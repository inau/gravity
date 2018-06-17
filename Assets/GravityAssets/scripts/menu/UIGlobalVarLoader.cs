using UnityEngine;
using System.Collections;

public abstract class UIGlobalVarLoader : MonoBehaviour {

	void OnEnable() {
		LoadVariables ();
	}

	abstract protected void LoadVariables ();
}
