using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    int health = 100;

    public UnityEvent dieEvent;

    public AI ai;

    public void TakeDamage(int dmg)
    {
        ai.Hit();
        health -= dmg;
        if (health <= 0)
        {
            dieEvent?.Invoke();
        }
    }
}
