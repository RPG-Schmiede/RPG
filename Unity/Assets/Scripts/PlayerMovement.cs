using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rBody;
    Animator anim;

    Vector2 movementVector;

    private bool lockMovement;
    public bool Lock
    {
        set
        {
            lockMovement = value;
            anim.SetBool("is_walking", false);
        }
        get
        {
            return lockMovement;
        }
    }

	// Use this for initialization
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (lockMovement)
            return;

        movementVector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));

        if(movementVector != Vector2.zero)
        {
            anim.SetBool("is_walking", true);
            anim.SetFloat("input_x", movementVector.x);
            anim.SetFloat("input_y", movementVector.y);
        }
        else
        {
            anim.SetBool("is_walking", false);
        }

        rBody.MovePosition(rBody.position + movementVector * Time.deltaTime);
    }
}
