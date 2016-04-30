using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class NPCController : MonoBehaviour
{
    private ActorMovement actorMovement;

    // -- Movement Settings --
    public enum MovementFrequenz
    {
        Lowest = 0,
        Low = 1,
        Normal = 2,
        High = 3,
        Higher = 4
    }

    private MovementFrequenz moveFrequenz = MovementFrequenz.Normal;

    private float nextMoveTime = 0.0f;

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

    public int movementRepeat = 0;
    private int curMovementRepeatCount = 0;

    // -- Waypoint system --
    public enum WaypointMovementOrder
    {
        None = 0,
        Random = 1,
        Forward = 2,
        Backward = 3,
        Patrolling = 4,
        Follow = 5,
        Custom = 6
    }

    private Vector2 movementDestination;
    private Vector2 waypointDestination;

    [Header("-- Waypoint System --")]
    public Transform[] waypoints;
    public WaypointMovementOrder[] waypointMovementOrder;

    private WaypointMovementOrder lastWaypointMovementOrder;
    private int lastWaypointIndex = 0;
    private int curWaypointIndex = 0;
    private bool movingtoWaypoint = false;
    private bool patrollingForward = true;

    private int waypointMovementCount = 0;
    private int waypointMovementOrderIndex = 0;

    public float followDistance = 1.0f;

    // -- Freeform system --
    public enum FreeformMovementOrder
    {
        None = 0,
        RandomMove = 1,
        MoveForward = 2,
        MoveBackward = 3,
        MoveUp = 4,
        MoveDown = 5,
        MoveLeft = 6,
        MoveRight = 7,
        RandomLook = 8,
        LookForward = 9,
        LookBackward = 10,
        LookUp = 11,
        LookDown = 12,
        LookLeft = 13,
        LookRight = 14,
    }

    [Header("-- Freeform System --")]
    public FreeformMovementOrder[] freeFormMovementOrder;

    
    private Vector2 lookDestination;

    private int curMovementIndex = 0;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        actorMovement = GetComponent<ActorMovement>();
        curMovementRepeatCount = movementRepeat;

        if (waypointMovementOrder != null && waypointMovementOrder.Length > 0)
        {
            lastWaypointMovementOrder = waypointMovementOrder[waypointMovementOrderIndex];
            CalculateWaypoint();
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (lockMovement)
            return;

        if (waypointMovementOrder != null && waypointMovementOrder.Length > 0 && waypointMovementOrder[waypointMovementOrderIndex] != WaypointMovementOrder.None && waypoints != null && waypoints.Length > 0)
        {
            // Waypoint movement
            if ((waypointMovementOrder[waypointMovementOrderIndex] == WaypointMovementOrder.Follow && Vector2.Distance(transform.position, waypointDestination) <= followDistance) || (Vector2.Distance(transform.position, waypointDestination) <= 0.1f))
            {
                CalculateWaypoint();
            }
            else
            {
                MoveTo(waypointDestination);
            }
        }
        else
        {
            // Freeform movement
            if (Vector2.Distance(transform.position, movementDestination) <= 0.1f)
            {
                CalculateFreeformMovement();
            }
            else
            {
                MoveTo(movementDestination);
            }
        }
    }

    /// <summary>
    /// Change Direction by Collision
    /// </summary>
    /// <param name="collision"></param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        movementDestination *= -1;
        nextMoveTime = Time.time + GameManager.Instance.moveFrequenzValues[2];
    }

    public void SetDestination()
    {
        if (waypoints.Length > 0)
        {
            MoveTo(waypoints[curWaypointIndex].position);
            movingtoWaypoint = true;
        }

        /*
        if (waypoints.Length > 0)
        {
            // --- Movement with waypoints ---
            for (int i = 0; i < waypoints.Length; i++)
            {
                switch (movementOrder[curMovementIndex])
                {
                    case MovementOrder.Random:
                        MoveTo(waypoints[Random.Range(0, waypoints.Length)].position);
                        break;
                    case MovementOrder.Forward:
                        MoveTo(waypoints[Random.Range(0, waypoints.Length)].position);
                        break;
                    case MovementOrder.Backward:
                        break;
                    case MovementOrder.Patrolling:
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            // --- Movement without waypoints ---
            switch (movementOrder[curMovementIndex])
            {
                case MovementOrder.None:
                    // None movement
                    break;
                case MovementOrder.Random:
                    // Random freeform
                    ChangeMovementDirection();
                    break;
                default:
                    Debug.LogWarning(string.Format("{0}: No waypoints assigend.", gameObject.name));
                    break;
            }
        }
        */

        nextMoveTime = Time.time + GameManager.Instance.moveFrequenzValues[(int)moveFrequenz];
    }

    #region Waypoint Movement

    /// <summary>
    /// Calculate the next Movement
    /// </summary>
    public void CalculateWaypoint()
    {
        if (waypoints.Length <= 0)
            return;

        int nextWaypointIndex = 0;
        bool startAgain = false;
        lastWaypointMovementOrder = waypointMovementOrder[waypointMovementOrderIndex];

        // --- Calculate current Position in MovementOrderArray ---
        if (lastWaypointMovementOrder != waypointMovementOrder[waypointMovementOrderIndex])
        {
            waypointMovementCount = 1;
        }
        else
        {
            int orderLength = waypointMovementOrder[waypointMovementOrderIndex] == WaypointMovementOrder.Patrolling ? ((waypoints.Length * 2) - 2) : (waypoints.Length-1);

            if (waypointMovementCount >= orderLength)
            {
                if (waypointMovementOrderIndex + 1 > waypointMovementOrder.Length - 1)
                {
                    if (movementRepeat == 0 || curMovementRepeatCount > 0)
                    {
                        waypointMovementOrderIndex = 0;
                        waypointMovementCount = 1;
                    }
                    else
                    {
                        Lock = true;
                        return;
                    }
                }
                else
                {
                    // Next MovementOrder
                    waypointMovementOrderIndex = waypointMovementOrderIndex + 1 > (waypointMovementOrder.Length - 1) ? 0 : waypointMovementOrderIndex + 1;
                    curMovementRepeatCount = movementRepeat > 0 ? curMovementRepeatCount - 1 : 0;
                }

                waypointMovementCount = 1;
                curWaypointIndex = 0;
                startAgain = true;
            }
            else
            {
                ++waypointMovementCount;
            }
        }

        // --- Set next Waypoint ---
        switch (waypointMovementOrder[waypointMovementOrderIndex])
        {
            case WaypointMovementOrder.Random:
                nextWaypointIndex = RandomWaypoint();
                break;
            case WaypointMovementOrder.Forward:
                nextWaypointIndex = NextWaypoint();
                break;
            case WaypointMovementOrder.Backward:
                nextWaypointIndex = PreviousWaypoint();
                break;
            case WaypointMovementOrder.Patrolling:
 
                if(curWaypointIndex >= waypoints.Length - 1)
                {
                    // Backward
                    patrollingForward = false;
                }
                else if (curWaypointIndex <= 0)
                {
                    // Forward
                    patrollingForward = true;
                }

                if (startAgain)
                {
                    nextWaypointIndex = 0;
                }
                else
                {
                    nextWaypointIndex = patrollingForward ? NextWaypoint() : PreviousWaypoint();
                }
                break;
            case WaypointMovementOrder.Follow:
                nextWaypointIndex = 0;
                break;
            default:
                break;
        }

        lastWaypointIndex = curWaypointIndex;
        curWaypointIndex = nextWaypointIndex;
        waypointDestination = waypoints[curWaypointIndex].position;
    }

    /// <summary>
    /// Return random waypoint that is not like the current and the last
    /// </summary>
    /// <returns></returns>
    public int RandomWaypoint()
    {
        int nextWaypointIndex = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            nextWaypointIndex = Random.Range(0, waypoints.Length);

            if (nextWaypointIndex != curWaypointIndex && nextWaypointIndex != lastWaypointIndex)
            {
                break;
            }
        }

        return nextWaypointIndex;
    }

    /// <summary>
    /// Return the previous waypoint. (if end start from first)
    /// </summary>
    /// <returns></returns>
    public int NextWaypoint()
    {
        return (curWaypointIndex + 1) > waypoints.Length - 1 ? 0 : curWaypointIndex + 1;
    }

    /// <summary>
    /// Return the previous waypoint. (if end start from last)
    /// </summary>
    /// <returns></returns>
    public int PreviousWaypoint()
    {
        return (curWaypointIndex - 1) < 0 ? waypoints.Length - 1 : curWaypointIndex - 1;
    }

    #endregion

    #region Freeform Movement

    /// <summary>
    /// Calculate next freefrom move
    /// </summary>
    public void CalculateFreeformMovement()
    {
        Vector2 newMovementVector = Vector2.zero;
        Vector2 newLookDestination = Vector2.zero;

        switch (freeFormMovementOrder[0])
        {
            case FreeformMovementOrder.None:
                break;
            case FreeformMovementOrder.RandomMove:
                newMovementVector = RandomMovementDirection();
                break;
            case FreeformMovementOrder.MoveForward:
                newMovementVector = transform.forward;
                break;
            case FreeformMovementOrder.MoveBackward:
                newMovementVector = -transform.forward;
                break;
            case FreeformMovementOrder.MoveUp:
                newMovementVector = Vector2.up;
                break;
            case FreeformMovementOrder.MoveDown:
                newMovementVector = Vector2.down;
                break;
            case FreeformMovementOrder.MoveLeft:
                newMovementVector = Vector2.left;
                break;
            case FreeformMovementOrder.MoveRight:
                newMovementVector = Vector2.right;
                break;
            case FreeformMovementOrder.RandomLook:
                newLookDestination = RandomMovementDirection();
                break;
            case FreeformMovementOrder.LookForward:
                newLookDestination = transform.forward;
                break;
            case FreeformMovementOrder.LookBackward:
                newLookDestination = -transform.forward;
                break;
            case FreeformMovementOrder.LookUp:
                newLookDestination = Vector2.up;
                break;
            case FreeformMovementOrder.LookDown:
                newLookDestination = Vector2.down;
                break;
            case FreeformMovementOrder.LookLeft:
                newLookDestination = Vector2.left;
                break;
            case FreeformMovementOrder.LookRight:
                newLookDestination = Vector2.right;
                break;
            default:
                break;
        }

        movementDestination = newMovementVector;
        lookDestination = newLookDestination;
    }

    /// <summary>
    /// Get random move direction for next step
    /// </summary>
    public Vector2 RandomMovementDirection()
    {
        Vector2 newMovementVector = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        newMovementVector.Normalize();
        return newMovementVector;
    }

    #endregion

    /// <summary>
    /// Move one Step to Target Position
    /// </summary>
    /// <param name="target">Target Transform</param>
    public void MoveTo(Vector3 targetPosition)
    {
        // Gets a vector that points from the player's position to the target's.
        Vector3 heading = targetPosition - transform.position;

        float distance = heading.magnitude;
        Vector3 direction = heading / distance; // This is now the normalized direction.

        actorMovement.Move(new Vector2(direction.x, direction.y));
    }
}
