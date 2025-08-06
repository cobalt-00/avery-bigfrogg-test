using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//This script does nothing on its own, only provides control functions for the state machine or a theoretical player input script to use
[RequireComponent(typeof(Rigidbody2D))]
public class AgentController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject boxAnchor;
    private GameObject heldBox = null;
    private Rigidbody2D rigidbody;
    private bool grounded = false;

    //These three properties are used as shared state across the state machine
    public Vector3 direction { get; set; }
    public GameObject target { get; set; }
    public GameObject touchingBox { get; private set; }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        if (playerData == null) 
        {
            Debug.LogError("Player has no data! Game will not function");
        }
    }

    public void MoveLeft()
    {
        rigidbody.linearVelocityX = -playerData.moveSpeed;
    }

    public void MoveRight()
    {
        rigidbody.linearVelocityX = playerData.moveSpeed;
    }

    public void Stop()
    {
        rigidbody.linearVelocityX = 0;
    }

    public void ThrowBoxLeft()
    {
        heldBox.transform.parent = null;
        heldBox.GetComponent<Rigidbody2D>().AddForce(new Vector2(-0.7f, 0.7f) * playerData.throwForce, ForceMode2D.Impulse);
    }

    public void ThrowBoxRight()
    {
        heldBox.transform.parent = null;
        heldBox.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.7f, 0.7f) * playerData.throwForce, ForceMode2D.Impulse);
    }

    public void GrabBox(GameObject box)
    {
        //set our held box, and set its position and rotation. Leave velocity alone mostly for humor reasons
        heldBox = box;
        box.transform.parent = boxAnchor.transform;
        box.transform.position = boxAnchor.transform.position;
        box.transform.rotation = Quaternion.identity;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            touchingBox = null;
        }
    }

    //we need to check both enter and stay so that we cant get stuck pressed up against boxes
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            touchingBox = collision.gameObject.gameObject;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            touchingBox = collision.gameObject.gameObject;
        }
    }
}
