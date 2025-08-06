using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public bool isBlue;
    public bool onWrongSide;

    private void Update()
    {
        //check to see if this box is on the wrong side
        if (isBlue)
        {
            onWrongSide = transform.position.x <= 0;
        }
        else
        {
            onWrongSide = transform.position.x > 0;
        }
    }
}
