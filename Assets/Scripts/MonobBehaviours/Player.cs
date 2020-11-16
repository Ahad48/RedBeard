using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    private void Start()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumables>().item;

            if (hitObject != null)
            {
                print("Hit: " + hitObject.objectName);
                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.Coin:
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.Health:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;

                    default:
                        break;

                }
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value += amount;
            return true;
        }
        return false;
    }
}
