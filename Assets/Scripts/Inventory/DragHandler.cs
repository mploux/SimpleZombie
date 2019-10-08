using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject heldItem;
	Vector3 startPosition;
	public Transform startParent;
	public Vector2 dragOffset;
	public ItemSlot slot = null;

	public void OnBeginDrag(PointerEventData eventData) {
		heldItem = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		slot = this.transform.parent.gameObject.GetComponent<ItemSlot> ();
		
		print(this.transform.parent.gameObject.name);

		this.transform.SetParent (this.transform.parent.parent.parent);
		dragOffset = Input.mousePosition - transform.position;
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData enventData) {
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		//transform.position += new Vector3(dragOffset.x, dragOffset.y, 0);
		this.transform.SetParent (startParent);
	}
}