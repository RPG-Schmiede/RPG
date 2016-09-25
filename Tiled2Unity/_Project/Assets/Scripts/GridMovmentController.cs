using UnityEngine;
using System.Collections;

public class GridMovmentController : MonoBehaviour
{
    public enum GridMovementDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    private GridMovementDirection curMovementDirection = GridMovementDirection.Up;

    [Header("Map Properties")]
    public int GridHeight = 64;
    public int GridWidth = 64;

    public int MapStartX = 0;
    public int MapStartY = 0;

    public Vector2 GridPosition = Vector2.zero;

    [Header("Character Properties")]
    public float speed = 1.0f;
    private float endTimeMove = 0.0f;

    [Header("Animator Reference")]
    public Animator animatorReference;

    private Rigidbody2D rigidBody2D;
    private Vector2 newPosition;

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        Teleport(GridPosition);

        if (animatorReference == null)
            animatorReference = GetComponentInChildren<Animator>();
    }

	private void Update()
    {
        if(Time.time <= endTimeMove && Time.time > 0f)
        {
            Moving();
            return;
        }

	    if(Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
        }
	}

    public void MoveUp()
    {
        Move(Vector2.up);
        curMovementDirection = GridMovementDirection.Up;
    }

    public void MoveRight()
    {
        Move(Vector2.right);
        curMovementDirection = GridMovementDirection.Right;
    }

    public void MoveDown()
    {
        Move(Vector2.down);
        curMovementDirection = GridMovementDirection.Down;
    }

    public void MoveLeft()
    {
        Move(Vector2.left);
        curMovementDirection = GridMovementDirection.Left;
    }

    private void Move(Vector2 destination)
    {
        RaycastHit2D hit;
        float originPoint = 0.0f;

        switch (curMovementDirection)
        {
            case GridMovementDirection.Up:
                originPoint = GridHeight * 0.55f;
                break;
            case GridMovementDirection.Right:
                originPoint = GridWidth * 0.55f;
                break;
            case GridMovementDirection.Down:
                originPoint = GridHeight * 0.55f;
                break;
            case GridMovementDirection.Left:
                originPoint = GridWidth * 0.55f;
                break;
            default:
                break;
        }

        // Check Collision
        hit = Physics2D.Raycast(rigidBody2D.position + (destination * originPoint), destination);
        
        // Destination Point
        if (hit.collider == null || hit.distance >= (originPoint * 2.0f))
        {
            GridPosition += destination;
            newPosition = CoordinatesToPosition(GridPosition);
            endTimeMove = Time.time + speed;
        }

        // Animation
        animatorReference.SetTrigger(curMovementDirection.ToString());
    }

    #region Movement Methodes

    /// <summary>
    /// Teleport unit imeditily
    /// </summary>
    /// <param name="destination">Grid coordinates</param>
    public void Teleport(Vector2 destination)
    {
        rigidBody2D.position = CoordinatesToPosition(destination);
        GridPosition = destination;
    }

    /// <summary>
    /// Do tick movement
    /// </summary>
    private void Moving()
    {
        rigidBody2D.MovePosition(Vector2.Lerp(rigidBody2D.position, newPosition, Time.time / endTimeMove));
    }

    #endregion

    #region Ultilities

    /// <summary>
    /// Convert grid coordinates to unity position
    /// </summary>
    /// <param name="destination">Grid coordinates</param>
    /// <returns></returns>
    private Vector2 CoordinatesToPosition(Vector2 destination)
    {
        Vector2 gridPosition = new Vector2((destination.x * GridWidth) + (GridWidth * 0.5f),
                                             (destination.y * GridHeight) - (GridHeight * 0.5f));

        return gridPosition;
    }

    #endregion
}
