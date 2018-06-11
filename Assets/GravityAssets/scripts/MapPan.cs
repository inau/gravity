using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MapPan : MonoBehaviour {

    public float min, max;
    public GameObject player;

    void Start()
    {
        //Fetch the Event Trigger component from your GameObject
        EventTrigger trigger = GetComponent<EventTrigger>();
        //Create a new entry for the Event Trigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        //Add a Drag type event to the Event Trigger
        entry.eventID = EventTriggerType.Drag;
        //call the OnDragDelegate function when the Event System detects dragging
        entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
        //Add the trigger entry
        trigger.triggers.Add(entry);
        Debug.Log("Setup DELEGATE"); 
    }

    public void OnDragDelegate(PointerEventData data)
    {
        Debug.Log(Input.GetAxis("Horizontal") );
        //Create a ray going from the camera through the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Calculate the distance between the Camera and the GameObject, and go this distance along the ray
        Vector3 rayPoint = ray.GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));

        rayPoint.x = rayPoint.x < min ? min : rayPoint.x;
        rayPoint.x = rayPoint.x > max ? max : rayPoint.x;
        rayPoint.y = this.transform.localPosition.y;

        rayPoint.z = 1;

        //Move the GameObject when you drag it
        transform.position = rayPoint;
    }

}
