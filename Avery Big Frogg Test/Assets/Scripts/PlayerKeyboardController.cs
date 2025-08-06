using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(AgentController))]
public class PlayerKeyboardController : MonoBehaviour
{
    private AgentController agentController;

    private void Start()
    {
        agentController = GetComponent<AgentController>();
    }

    //quick and dirty agent input controller to test movement
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.A))
       {
            agentController.MoveLeft();
       }

        if (Input.GetKeyDown(KeyCode.D))
        {
            agentController.MoveRight();
        }

    }
}
