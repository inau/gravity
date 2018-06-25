using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class DragRotation : MonoBehaviour {

	private ObservableEventTrigger oet;

	// Use this for initialization
	void Start () {
		oet = this.gameObject.AddComponent<ObservableEventTrigger> ();

		oet.OnPointerDownAsObservable()
			.Where(x => x.selectedObject == null)
			.TakeUntil (oet.OnPointerUpAsObservable() )
			.RepeatUntilDestroy(this)
			.Subscribe(x => HandleDrag(x) )
			.AddTo(oet);
	}

	void HandleDrag(UnityEngine.EventSystems.PointerEventData ped) {
		Debug.Log("x: " + ped);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
