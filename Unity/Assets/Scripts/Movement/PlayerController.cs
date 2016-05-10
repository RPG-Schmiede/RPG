using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool touchMovement = false;

    private ActorMovement actorMovement;
    private Vector2 movementVector;

    private Vector3 touchStartPosition;

    private bool lockMovement;
    public bool Lock
    {
        set
        {
            lockMovement = value;
            actorMovement.LockMovement();
        }
        get
        {
            return lockMovement;
        }
    }

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        actorMovement = GetComponent<ActorMovement>();

        if(Application.isMobilePlatform)
        {
            touchMovement = true;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
        if (lockMovement)
            return;

        if(touchMovement)
        {
            // --- Touch Movement ---
            if(Input.GetMouseButtonDown(0))
            {
                touchStartPosition = Input.mousePosition;
            }

            if(Input.GetMouseButton(0))
            {
                // Gets a vector that points from the player's position to the target's.
                Vector3 heading = Input.mousePosition - touchStartPosition;
                float distance = heading.magnitude;
                Vector2 direction = heading / distance; // This is now the normalized direction.

                actorMovement.Move(direction);
            }

            if (Input.GetMouseButtonUp(0))
            {
                actorMovement.Move(Vector2.zero);
            }
        }
        else
        {
            // --- Keyboard / Analogstick movement ---
            movementVector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
            actorMovement.Move(movementVector);
        }
    }
}
