using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumables>().item;

            if (hitObject != null)
            {
                print("Hit: " + hitObject.objectName);

                switch (hitObject.itemType)
                {
                    case Item.ItemType.Coin:
                        break;

                    case Item.ItemType.Health:
                        AdjustHitPoints(hitObject.quantity);
                        break;

                    default:
                        break;

                }

                collision.gameObject.SetActive(false);
            }
        }
    }

    public void AdjustHitPoints(int amount)
    {
        if (hitPoints < maxHitPoints)
        {
            hitPoints++;
            print("Adjusted hitpoints by: " + amount + ".New value: " + hitPoints);
        }

    }
}
