using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerHandler : MonoBehaviour {

	public float factor = 0.5f;

    void TriggerEvent(TriggerCondition.ConditionType ct)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (ct)
        {
		case TriggerCondition.ConditionType.LOSE:
			GlobalVariables.GravityMomentum = GlobalVariables.GravityMomentum == GlobalVariables.GravityMin ? GlobalVariables.GravityMomentum : GlobalVariables.GravityMomentum -= factor;
                SceneManager.LoadScene(currentSceneName);
                Debug.Log("Black hole");
                break;
            case TriggerCondition.ConditionType.WIN:
			GlobalVariables.GravityMomentum = GlobalVariables.GravityMomentum == GlobalVariables.GravityMax ? GlobalVariables.GravityMomentum : GlobalVariables.GravityMomentum += factor;
                SceneManager.LoadScene(currentSceneName);
                Debug.Log("Win");
                break;
            default:
                Debug.Log("NO event");
                break;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
