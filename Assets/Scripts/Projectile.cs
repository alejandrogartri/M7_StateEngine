using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 5;

    private void OnCollisionEnter(Collision collision)
    {
        CubeMovement player = collision.gameObject.GetComponent<CubeMovement>();
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if (player)
        {
            // Projectile has hit the player
            if (player.health > 0)
            {
                player.health -= damage;
            }
        }
        else if (enemy)
        {
            // Projectile has hit an enemy
            enemy.health -= damage;
            if (enemy.health <= 0)
            {
                Destroy(enemy.gameObject);
            }
        }

        Destroy(gameObject);
    }
}
