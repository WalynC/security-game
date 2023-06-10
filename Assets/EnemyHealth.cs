using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int health = 100;

    public AI ai;

    public void TakeDamage(int dmg)
    {
        ai.Hit();
        health -= dmg;
        if (health <= 0) { } //we winnered
    }
}
