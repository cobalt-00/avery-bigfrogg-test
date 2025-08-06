using UnityEngine;

public delegate bool EvaluateConditions();
public class Transition
{
    public State from { get; private set; }
    public State to { get; private set; }

    //maybe adds some initial cognitive load but prevents huge bloat of having a class for each transition, also makes data access easier
    public EvaluateConditions evaluateConditions { get; private set; }

    public Transition(State from, State to, EvaluateConditions evalFunction)
    {
        this.to = to;
        this.from = from;
        evaluateConditions = evalFunction;
    }
}
