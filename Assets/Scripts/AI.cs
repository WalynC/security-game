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
    public LayerMask mask;
    bool fleeing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetNextObjective();
    }

    public void Hit()
    {
        if (fleeing) return;
        Transform esc = null;
        float dist = float.MaxValue;
        for (int i = 0; i < escapeCont.childCount; ++i)
        {
            Transform ch = escapeCont.GetChild(i);
            Ray ray = new Ray(ch.position, PlayerController.instance.transform.position - ch.position);
            if (Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(transform.position, PlayerController.instance.transform.position), mask))
            {
                float chdist = Vector3.Distance(ch.position, transform.position);
                if (chdist < dist)
                {
                    esc = ch;
                    dist = chdist;
                }
            }
        }
        if (esc != null)
        {
            fleeing = true;
            next = null;
            target = esc;
            agent.destination = target.position;
        }
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
                fleeing = false;
                GetNextObjective();
            }
        }
    }
}
