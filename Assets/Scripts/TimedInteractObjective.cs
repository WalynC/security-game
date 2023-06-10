using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedInteractObjective : Objective
{
    bool isInteracting = false;
    public float timeToComplete = 5f;
    float timeElapsed = 0f;
    AI current = null;

    public override void Interact(AI ai)
    {
        current = ai;
        ai.anim.SetBool("interact", true);
        isInteracting = true;
    }

    public override void Complete()
    {
        base.Complete();
        current.anim.SetBool("interact", false);
        isInteracting = false;
    }

    private void Update()
    {
        if (isInteracting)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed >= timeToComplete) Complete();
        }
    }
}
