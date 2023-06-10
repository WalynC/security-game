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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNextObjective();
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
        agent.destination = next.targetLocation.position;
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
            else if (Vector3.Distance(agent.transform.position, next.targetLocation.position) < 0.5f)
            {
                next.Interact(this);
            }
        }
    }
}
