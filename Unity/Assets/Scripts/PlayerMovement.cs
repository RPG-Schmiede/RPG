using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rBody;
    Animator anim;

    Vector2 movementVector;

	// Use this for initialization
	void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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
