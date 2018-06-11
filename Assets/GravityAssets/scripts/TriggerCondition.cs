using UnityEngine;
using System.Collections;

public class TriggerCondition : MonoBehaviour {

    public enum ConditionType
    {
        WIN,
        LOSE,
        POWERUP
    }

    public bool terminating_condition = true;
    public ConditionType ctype = ConditionType.WIN;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player")
        {
            SendMessageUpwards("TriggerEvent", ctype);
        }
    }

}
