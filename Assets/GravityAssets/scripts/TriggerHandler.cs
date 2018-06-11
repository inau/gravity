using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerHandler : MonoBehaviour {

    void TriggerEvent(TriggerCondition.ConditionType ct)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (ct)
        {
            case TriggerCondition.ConditionType.LOSE:
                SceneManager.LoadScene(currentSceneName);
                Debug.Log("Black hole");
                break;
            case TriggerCondition.ConditionType.WIN:
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
