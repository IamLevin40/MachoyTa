using UnityEngine;
using UnityEngine.EventSystems;

public class InstantiateButtonHandler : MonoBehaviour, IPointerDownHandler
{
    public ItemData itemData;         // ScriptableObject for this button
    public GameObject itemPrefab;     // Prefab to instantiate
    public DragAndDropHandler dragAndDropHandler;  // Reference to drag handler

    public void OnPointerDown(PointerEventData eventData)
    {
        dragAndDropHandler.StartDragging(itemData, itemPrefab);
    }
}