using UnityEngine;
using System.Collections.Generic;

public class CheckZoneHandler : MonoBehaviour
{
    [Tooltip("List of valid ItemData objects for this zone.")]
    public List<ItemData> validItems;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Checks if the item is inside the zone's bounds and if it is valid.
    /// </summary>
    /// <param name="itemPosition">The world position of the item.</param>
    /// <param name="itemData">The data of the item being checked.</param>
    /// <returns>True if the item is within bounds and valid, otherwise false.</returns>
    public bool IsItemValidInZone(Vector3 itemPosition, ItemData itemData)
    {
        if (!IsWithinBounds(itemPosition))
        {
            Debug.Log("Item is not within the zone bounds.");
            return false;
        }

        if (!validItems.Contains(itemData))
        {
            Debug.Log($"Item '{itemData.itemName}' is not valid for this zone.");
            return false;
        }

        Debug.Log($"Item '{itemData.itemName}' is valid and within the zone.");
        return true;
    }

    /// <summary>
    /// Checks if the item is within the bounds of the zone.
    /// </summary>
    /// <param name="worldPosition">The world position of the item.</param>
    /// <returns>True if within bounds, false otherwise.</returns>
    private bool IsWithinBounds(Vector3 worldPosition)
    {
        Vector3 localPosition = rectTransform.InverseTransformPoint(worldPosition);
        return rectTransform.rect.Contains(localPosition);
    }
}