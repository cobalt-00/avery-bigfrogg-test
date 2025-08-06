using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AgentController))]
public class StateMachineDriver : MonoBehaviour
{
    /* State Machine Map
                             Acquire target
                                    |
                             Move to target       
                                    |
                              (touched a box)
                                    v
                     throw box apropriate direction
                                   /\
                                  /  \
   acquire target <- it was target    it wasn't target -> move 
     */

    private AgentController agentController;
    private State currentState;

    //Data shared across states


    private void Start()
    {
        agentController = GetComponent<AgentController>();


        //construct states and transitions
        var acquireState = new AcquireTargetState(agentController);
        var movingState = new MoveToTargetState(agentController);
        var throwingState = new ThrowBoxState(agentController);

        //transition between acquire and move
        var AcquireToMove = new Transition(acquireState, movingState, HasTarget);
        acquireState.AddTransition(AcquireToMove);

        //transition between move and throw
        var MoveToThrow = new Transition(movingState, throwingState, TouchingBox);
        movingState.AddTransition(MoveToThrow);

        //transition between throw and move
        var ThrowToMove = new Transition(throwingState, movingState, HasTarget);
        throwingState.AddTransition(ThrowToMove);

        //transition between throw and acquire
        var ThrowToAcquire = new Transition(throwingState, acquireState, NoTarget);
        throwingState.AddTransition(ThrowToAcquire);

        currentState = acquireState;

        //kick off our state machine
        StartCoroutine(RunStateLoop());

    }

    private IEnumerator RunStateLoop()
    {
        //engine for the state machine
        //theoretically in a larger project this might have an exit contidion involving hitting a certain state before the agent is destroyed
        while (true)
        {
            //first, check if we've met any of the transition requirements
            var activeTransition = currentState.EvaluateTransitions();
            if (activeTransition != null)
            {
                //if we have a transition, call current state's OnExit, then new state's OnEnter, then set the new state as the current state
                yield return currentState.OnExit();
                yield return activeTransition.to.OnEnter();
                currentState = activeTransition.to;
            }
            else
            {
                //if we dont have a transition, run the maintain function for our current state
                yield return currentState.OnMaintain();
            }

            //wait for the next frame before continuing the loop
            yield return null;
        }
    }


    private bool HasTarget()
    {
        return agentController.target != null;
    }

    private bool NoTarget()
    {
        return agentController.target == null;
    }

    private bool TouchingBox()
    {
        return agentController.touchingBox != null;
    }
}
