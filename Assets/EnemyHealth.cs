using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int health = 100;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) //we winnered
    }
}
