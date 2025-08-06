using System.Collections;
using UnityEngine;

public class ThrowBoxState : State
{
    public ThrowBoxState(AgentController controller) : base(controller)
    {
    }

    public override IEnumerator OnEnter()
    {
        controller.Stop();
        var boxHeld = controller.touchingBox;
        controller.GrabBox(boxHeld);

        //comedic pause
        yield return new WaitForSeconds(0.5f);

        if (boxHeld.GetComponent<BoxScript>().isBlue)
        {
            controller.ThrowBoxRight();
        }
        else
        {
            controller.ThrowBoxLeft();
        }

        //if we threw the target box, assume it went to the correct side and clear our target
        if (boxHeld == controller.target)
        {
            controller.target = null;
        }

        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator OnExit()
    {
        yield break;
    }

    public override IEnumerator OnMaintain()
    {
        yield break;

    }
}
