using UnityEngine;
using System;

public class DragAndDropHandler : MonoBehaviour
{
    public LayerMask m_DragLayers;   // Specify layers for draggable items
    public float m_Damping = 1.0f;   // Damping for TargetJoint2D
    public float m_Frequency = 5.0f; // Frequency for TargetJoint2D

    private GameObject currentlyDraggedItem;
    private TargetJoint2D m_TargetJoint;
    private Rigidbody2D body;

    public void StartDragging(ItemData itemData, GameObject itemPrefab)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        currentlyDraggedItem = Instantiate(itemPrefab, mousePosition, Quaternion.identity);
        currentlyDraggedItem.GetComponent<SpriteRenderer>().sprite = itemData.instantiatedSprite;
        currentlyDraggedItem.transform.localScale = new Vector3(itemData.scaleX, itemData.scaleY, 1);
        currentlyDraggedItem.AddComponent<ItemComponent>().itemData = itemData;
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider = Physics2D.OverlapPoint(mousePosition, m_DragLayers);
            if (!collider) return;

            body = collider.attachedRigidbody;
            if (!body) return;

            if (m_TargetJoint) Destroy(m_TargetJoint);

            m_TargetJoint = body.gameObject.AddComponent<TargetJoint2D>();
            m_TargetJoint.dampingRatio = m_Damping;
            m_TargetJoint.frequency = m_Frequency;
            m_TargetJoint.anchor = m_TargetJoint.transform.InverseTransformPoint(mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (currentlyDraggedItem != null)
            {
                NotifyZones(currentlyDraggedItem);
                Destroy(currentlyDraggedItem);
            }

            if (m_TargetJoint) Destroy(m_TargetJoint);
            m_TargetJoint = null;
            body = null;
        }

        if (m_TargetJoint) m_TargetJoint.target = mousePosition;
    }

    private void NotifyZones(GameObject item)
    {
        var itemData = item.GetComponent<ItemComponent>().itemData;
        var allZones = FindObjectsOfType<CheckZoneHandler>();
        Vector3 itemPosition = item.transform.position;

        foreach (var zone in allZones)
        {
            if (zone.IsItemValidInZone(itemPosition, itemData))
            {
                Debug.Log("Item successfully placed in the correct zone!");
                return;  // Exit after finding the correct zone
            }
        }

        Debug.LogWarning($"No valid zone found for item '{itemData.name}'.");
    }
}