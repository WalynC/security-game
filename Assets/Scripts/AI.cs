using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Objective main;
    public Objective next;
    NavMeshAgent agent;
    public Animator anim;

    public Transform escapeCont;
    Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNextObjective();
    }

    public void Hit()
    {
        next = null;
        Transform esc = escapeCont.GetChild(0);
        float dist = Vector3.Distance(transform.position, esc.position);
        for (int i = 1; i < escapeCont.childCount; ++i)
        {
            Transform ch = escapeCont.GetChild(i);
            float chdist = Vector3.Distance(ch.position, transform.position);
            if (chdist < dist)
            {
                esc = ch;
                dist = chdist;
            }
        }
        target = esc;
        agent.destination = target.position;
    }

    public void GetNextObjective()
    {
        List<Objective> objectives = main.GetNextSteps();
        if (objectives.Count > 0)
        {
            next = objectives[Random.Range(0, objectives.Count)];
            GetPathToObjective();
        }
        else
        {
            //all objectives complete, game should be over by now
        }
    }

    public void GetPathToObjective()
    {
        target = next.targetLocation;
        agent.destination = target.position;
    }

    private void Update()
    {
        bool isMoving = agent.velocity.magnitude > 0.5f;
        anim.SetBool("move", isMoving);
        if (next != null)
        {
            if (next.complete)
            {
                next = null;
                GetNextObjective();
            }
            else if (Vector3.Distance(agent.transform.position, target.position) < 0.5f)
            {
                next.Interact(this);
            }
        }
        else
        {
            if (Vector3.Distance(agent.transform.position, target.position) < 0.5f)
            {
                GetNextObjective();
            }
        }
    }
}
