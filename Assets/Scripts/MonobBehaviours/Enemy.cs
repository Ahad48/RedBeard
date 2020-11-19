using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The enemy class, inherits from the character class
/// </summary>
public class Enemy : Character
{
    // The hitPoints of the enemy
    float hitPoints;

    // How much can the enemy damge the player
    public int damageStrenth;

    // A coroutine for damaging the charater
    Coroutine damageCoriutine;

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            // Damage the character
            hitPoints -= damage;

            // If the health of the character is less than 0 destroy it
            if (hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            // Wait for interval
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
    /// Reset the character with the hitpoints to the starting hitpoints
    /// </summary>
    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Take the instance of the player
            Player player = collision.gameObject.GetComponent<Player>();

            if(damageCoriutine == null)
            {
                // Damage the player character
                damageCoriutine = StartCoroutine(player.DamageCharacter(damageStrenth, 1.0f));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (damageCoriutine != null)
            {
                StopCoroutine(damageCoriutine);
                damageCoriutine = null;
            }
        }
    }


}
