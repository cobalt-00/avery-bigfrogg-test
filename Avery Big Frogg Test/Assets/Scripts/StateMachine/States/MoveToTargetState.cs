using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MoveToTargetState : State
{
    public MoveToTargetState(AgentController controller) : base(controller) 
    {
    }

    public override IEnumerator OnEnter()
    {
        yield break;
    }

    public override IEnumerator OnExit()
    {
        yield break;
    }

    public override IEnumerator OnMaintain()
    {
        //get our general direction
        var direction = controller.transform.position - controller.target.transform.position;

        if (direction.x < 0)
        {
            //when we store the direction we want it to be exact and absolute 
            controller.direction = Vector3.right;
            controller.MoveRight();
        } 
        //make sure we dont stutter in place
        else if (Mathf.Abs(direction.x) < 0.2f)
        {
            controller.Stop();
        }
        else
        {
            controller.direction = Vector3.left;
            controller.MoveLeft();
        }

        yield break;
    }
}
