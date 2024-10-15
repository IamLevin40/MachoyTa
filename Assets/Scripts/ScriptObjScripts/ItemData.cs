using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;            // Name of the item
    public Sprite buttonSprite;        // Sprite for the button
    public Sprite instantiatedSprite;  // Sprite for the instantiated item
    public float scaleX = 1.0f;        // Scale factor on X
    public float scaleY = 1.0f;        // Scale factor on Y
}