using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class State
{
    protected AgentController controller;

    protected List<Transition> transitions = new List<Transition>();

    public State(AgentController controller)
    {
        this.controller = controller;
    }

    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }

    public Transition EvaluateTransitions()
    {
        //iterate over transitions and invoke their evaluate delegate to find if any have their conditions met
        foreach (var transition in transitions) 
        {
            if (transition.evaluateConditions.Invoke())
            {
                return transition;
            }
        }

        return null;
   
    }

    public abstract IEnumerator OnEnter();

    public abstract IEnumerator OnMaintain();

    public abstract IEnumerator OnExit();
}
