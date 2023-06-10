using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticleWhenDone : MonoBehaviour
{
    public ParticleSystem sys;
    public bool enemyHitParticle;

    void Update()
    {
        if (!sys.IsAlive())
        {
            PlayerController.instance.ReturnParticleObject(sys, enemyHitParticle);
        }
    }
}
