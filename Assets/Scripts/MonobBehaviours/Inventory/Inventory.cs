using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// The inventory of the player
/// </summary>
public class Inventory : MonoBehaviour
{
    // Slot Prefab
    public GameObject slotPrefab;

    // NU,ber of slots to be created
    public const int numSlots = 5;

    // The image of the Item which will be placed in the slot
    Image[] itemImages = new Image[numSlots];

    // The item stored in the slot
    Item[] items = new Item[numSlots];

    // The slot object
    GameObject[] slots = new GameObject[numSlots];

    // Start is called before the first frame update
    public void Start()
    {
        CreateSlots();
    }


    /// <summary>
    /// Creates numSlots number of slots
    /// </summary>
    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            // Looping and creating slots
            for (int i = 0; i < numSlots; i++)
            {
                // Instantiate the slot
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i;

                /* Makes the object the child of InventoryBackground
                 * InventoryObject
                 * --InventoryBackground <--- Child (0)
                 */
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = newSlot;

                /* Gets the image object from the new slot and sets it to the ItemImages 
                 * InventoryObject
                 * --InventoryBackground <--- Child (0)
                 *      --ItemSlot_i
                 *          --BackgroundImage <--- Child (0)
                 *          --ItemImage <-- Child (1)
                 */
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    /// <summary>
    /// Adds the Item to the Inventory
    /// </summary>
    /// <param name="itemToAdd"></param>
    public bool AddItem(Item itemToAdd)
    { 
        for (int i = 0; i < items.Length; i++)
        {
            // Checks if the Item is already in the inventory and if it exists and is stackable then stacks it
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {
                // Increases the quantity of the item
                items[i].quantity+= itemToAdd.quantity;

                // Gets the slot gameobject
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();

                // Gets the instance of the TextmeshProUGUI of the slot object
                TextMeshProUGUI quantityText = slotScript.qytText;

                // Enables the text
                quantityText.enabled = true;

                // Sets the text to the item quantity
                quantityText.text = items[i].quantity.ToString();

                // Returns true if the item has been added
                return true;
            }

            // If there are no items matching
            if (items[i] == null )
            {

                // Instantiates the item
                items[i] = Instantiate(itemToAdd);

                // Add the quantity of the Item
                items[i].quantity = itemToAdd.quantity;

                // Add the sprite
                itemImages[i].sprite = itemToAdd.sprite;

                // Get the slot component
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();

                // Get an instance of the text
                TextMeshProUGUI quantityText = slotScript.qytText;

                // Enable and set the text
                quantityText.enabled = true;
                quantityText.text = items[i].quantity.ToString();

                // Enable the sprite of the item 
                itemImages[i].enabled = true;

                return true;
            }
        }

        return false;
    }
}
