using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player class, inherits from the character class
/// </summary>
public class Player : Character
{
    // The prefab of Health bar which is a Healthbar Object Prefab
    public HealthBar healthBarPrefab;

    // The prefab of Inventory which is an Inventory Object Prefab
    public Inventory inventoryprefab;

    // Creating an inventory object
    Inventory inventory;

    // Creating a helathbar object
    HealthBar healthBar;

    // The hit points of the character, Scriptable object
    public HitPoints hitPoints;

    private void Start()
    {
        // Setting the players starting Hitpoints
        hitPoints.value = startingHitPoints;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with any pickups i.e. coins or hearts
        if (collision.gameObject.CompareTag("Pickups"))
        {
            // Storing the hitObject
            Item hitObject = collision.gameObject.GetComponent<Consumables>().item;

            if (hitObject != null)
            {
                // If the object should disappear by default set to false
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.Coin:

                        // Add the item to the inventory if it is a collectable
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;

                    case Item.ItemType.Health:

                        // Check if the helath pickup is full 
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;

                    default:
                        break;

                }

                // Ceck if the gameobject is should disappear after it has been added to the playe
                if (shouldDisappear)
                {
                    // set the gameObject to not active
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// <para>Adjusts the hitpoints.value of the player and returns a bool whether the gameobject should diappear or not</para>
    /// </summary>
    /// <param name="amount">The amount to be added</param>
    /// <returns>
    /// <para>returns a bool whether the gameobject should diappear or not</para>
    /// </returns>
    public bool AdjustHitPoints(int amount)
    {
        // Checks the current hitpoint value of the player
        if (hitPoints.value < maxHitPoints)
        {
            // Take the health if the current hitpoint is less than maximum
            hitPoints.value += amount;

            // If the current helath after taking the health is greater than max value
            if (hitPoints.value > maxHitPoints)
            {
                // Make the hitpoints value to maxHitPoints
                hitPoints.value = maxHitPoints;
            }

            // Return True if the Item has been taken
            return true;
        }

        // If nothing is being absorbed return false
        return false;
    }

    /// <summary>
    /// A coroutine function that damages tha player at specific time interval
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="interval"></param>
    /// <returns></returns>
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            // Subtracts the health
            hitPoints.value -= damage;

            // If the value is less than the lowest value kill the character
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            // Wait for the interval
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Destroys the player gameobject and the healthbar and inventory associated with it
    /// </summary>
    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    /// <summary>
    /// Resets the player adds the inventory, helathbar and the sets the hitpoints of the character
    /// </summary>
    public override void ResetCharacter()
    {
        hitPoints.value = startingHitPoints;
        inventory = Instantiate(inventoryprefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }
}
