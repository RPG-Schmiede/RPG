using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ActorController : MonoBehaviour
{
    // --- Components ---
    protected PhysicsRaycaster movementRaycaster;
    protected NavMeshAgent navMeshAgent;
    protected DialogController dialogController;
    protected Animator animator;

    protected ActorController target;
    protected float targetStoppingDistance;
    protected bool locked = false;

    // --- Properties ---
    public float StoppingDistance
    {
        get
        {
            return navMeshAgent != null ? navMeshAgent.radius + 2.0f : 0.0f;
        }
    }

    public bool HasDialog
    {
        get
        {
            return dialogController != null ? true : false;
        }
    }

    // Use this for initialization
    protected void Start()
    {
        movementRaycaster = Camera.main.GetComponent<PhysicsRaycaster>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        dialogController = GetComponent<DialogController>();
        animator = GetComponent<Animator>();
    }

    public void StartConversation(UnityAction callback)
    {
        dialogController.StartDialog(callback);
    }
}
