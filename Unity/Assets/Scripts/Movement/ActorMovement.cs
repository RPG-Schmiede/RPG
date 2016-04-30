using UnityEngine;

public class ActorMovement : MonoBehaviour
{
    private Rigidbody2D rBody;
    private Animator anim;

    [Range(0.0f, 10.0f)]
    public float speed = 1.0f;

    private Vector2 movementVector;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start ()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
	}
	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movementVector"></param>
	public void Move(Vector2 movementVector)
    {
        if(anim)
        {
            // -- Animator --
            if (movementVector != Vector2.zero)
            {
                anim.SetBool("is_walking", true);
                //anim.speed = movementVector.sqrMagnitude;
                anim.SetFloat("input_x", movementVector.x);
                anim.SetFloat("input_y", movementVector.y);
            }
            else
            {
                anim.SetBool("is_walking", false);
            }
        }

        // -- RigidBody --
        rBody.MovePosition(rBody.position + movementVector * speed * Time.deltaTime);
    }

    /// <summary>
    /// Lock Movement Animation
    /// </summary>
    public void LockMovement()
    {
        anim.SetBool("is_walking", false);
    }
}
