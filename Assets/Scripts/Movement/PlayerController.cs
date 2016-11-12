using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public bool touchMovement = false;

    private ActorMovement actorMovement;
    private Vector2 movementVector;
    private bool movementStart;

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
            if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                touchStartPosition = Input.mousePosition;
                movementStart = true;
            }

            if(movementStart && Input.GetMouseButton(0))
            {
                // Gets a vector that points from the player's position to the target's.
                Vector3 heading = Input.mousePosition - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f);
                float distance = heading.magnitude;
                Vector2 direction = heading / distance; // This is now the normalized direction.

                actorMovement.Move(direction);
            }

            if(Input.GetMouseButtonUp(0))
            {
                // Reset Animator
                actorMovement.Move(Vector2.zero);
                movementStart = false;
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
