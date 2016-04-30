using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private ActorMovement actorMovement;
    private Vector2 movementVector;

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
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
        if (lockMovement)
            return;

        movementVector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
        actorMovement.Move(movementVector);
    }
}
