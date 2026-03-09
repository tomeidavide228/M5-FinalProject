using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    int _damage = 1;
    private void OnTriggerEnter(Collider player)
    {
        Debug.Log("Touch");
        if (player.gameObject.CompareTag("Player"))
        {
            LifeController playerLife = player.gameObject.GetComponentInChildren<LifeController>();
            if (playerLife != null)
            {
                playerLife.TakeDamage(_damage);
            }
        }
    }
}
