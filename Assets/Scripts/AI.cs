using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public Objective main;
    public Objective next;
    NavMeshAgent agent;

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
        if (next != null)
        {
            if (next.complete)
            {
                next = null;
                GetNextObjective();
            }
            else if (Vector3.Distance(agent.transform.position, next.targetLocation.position) < 0.5f)
            {
                next.Interact();
            }
        }
    }
}
