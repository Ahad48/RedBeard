using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    public Inventory inventoryprefab;
    Inventory inventory;
    HealthBar healthBar;
    public HitPoints hitPoints;

    private void Start()
    {
        hitPoints.value = startingHitPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
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
                        shouldDisappear = inventory.AddItem(hitObject);
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

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints.value -= damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
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

    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        hitPoints.value = startingHitPoints;
        inventory = Instantiate(inventoryprefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }
}
