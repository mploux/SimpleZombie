using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

	public bool isUsed = false;
	public int id = -1;
	public Item item;

	public bool hasContraint;
	public string contraint;

	public ItemSlot(int id) {
		this.id = id;
	}

	public void Start() {
		if (this.transform.childCount != 0)
			isUsed = true;
	}

	public void OnDrop(PointerEventData eventData) {
		DragHandler drag = eventData.pointerDrag.GetComponent<DragHandler> ();
		Item item = eventData.pointerDrag.GetComponent<Item> ();
		if (hasContraint) {
			if (item.type.Equals(contraint)) {
				if (drag != null && isUsed == false) {
					drag.slot.isUsed = false;
					drag.startParent = this.transform;
					isUsed = true;
				}
			}
		} else {
			if (drag != null && isUsed == false) {
				drag.slot.isUsed = false;
				drag.startParent = this.transform;
				isUsed = true;
			}
		}
	}


}