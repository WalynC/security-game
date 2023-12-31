using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    public List<Objective> prereqs;
    public bool complete { get; private set; }
    public Transform targetLocation;

    public UnityEvent completeEvent;

    void Awake()
    {
        List<Objective> pre = new List<Objective>();
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).TryGetComponent<Objective>(out Objective obje)) pre.Add(obje);
        }
        prereqs = pre;
    }

    public bool IsAvailable()
    {
        foreach (Objective o in prereqs)
        {
            if (!o.complete) return false;
        }
        return true;
    }

    public List<Objective> GetNextSteps()
    {
        List<Objective> ret = new List<Objective>();
        List<Objective> list = new List<Objective>();
        list.Add(this);
        while (list.Count > 0)
        {
            List<Objective> newList = new List<Objective>();
            foreach (Objective o in list)
            {
                if (o.IsAvailable())
                {
                    if (!o.complete) ret.Add(o);
                } else
                {
                    foreach (Objective n in o.prereqs) newList.Add(n);
                }
            }
            list = newList;
        }
        return ret;
    }

    public virtual void StopInteract()
    {

    }

    public virtual void Complete()
    {
        complete = true;
        completeEvent?.Invoke();
    }

    public virtual void Interact(AI ai) {
        Complete();
    }
}
