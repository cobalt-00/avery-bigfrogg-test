using UnityEngine;

public delegate bool EvaluateConditions();
public class Transition
{
    public State from { get; private set; }
    public State to { get; private set; }

    //sacrificing some debugability for readability in setup
    public EvaluateConditions evaluateConditions { get; private set; }

    public Transition(State from, State to, EvaluateConditions evalFunction)
    {
        this.to = to;
        this.from = from;
        evaluateConditions = evalFunction;
    }
}
