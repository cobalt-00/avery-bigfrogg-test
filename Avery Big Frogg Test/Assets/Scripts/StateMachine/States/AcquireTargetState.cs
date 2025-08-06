using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AcquireTargetState : State
{
    public AcquireTargetState(AgentController controller) : base(controller)
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
        //find all boxes.
        var boxes = GameObject.FindGameObjectsWithTag("Box");
        //Look for the closest one that's on the wrong side.
        Vector3 lowestDistance = new Vector3(9999,9999,9999);
        
        foreach (var box in boxes)
        {
            var boxScript = box.GetComponent<BoxScript>();
            //wait for boxes to fall on the wrong side before pathing to them
            if (boxScript.onWrongSide && box.transform.position.y < 2)
            {   
                Vector3 newDist = controller.transform.position - boxScript.transform.position;
                //if this is the closest box we've checked that's on the wrong side, set it as our new target
                if (Mathf.Abs(newDist.x) < Mathf.Abs(lowestDistance.x))
                {
                    lowestDistance = newDist;
                    controller.target = boxScript.gameObject;
                }
            }
        }
        yield break;
    }
}
