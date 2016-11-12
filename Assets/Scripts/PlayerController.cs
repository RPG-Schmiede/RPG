using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerController : ActorController
{
    // Use this for initialization
    protected void Start()
    {
        base.Start();
    }
	
	// Update is called once per frame
	void Update()
    {
        if (locked)
            return;

	    if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast(ray, out hit, 100.0f, movementRaycaster.eventMask))
            {
                CancelInvoke("MoveToTarget");
                CancelInvoke("CheckDestination");

                target = hit.transform.GetComponent<ActorController>();
                
                if(target != null)
                {
                    targetStoppingDistance = target.StoppingDistance;

                    // Move to target
                    InvokeRepeating("MoveToTarget", 0.0f, 0.5f);
                }
                else
                {
                    targetStoppingDistance = 0.0f;

                    // Move to Position
                    navMeshAgent.SetDestination(hit.point);
                    navMeshAgent.Resume();
                }

                animator.SetFloat("Velocity", 1.0f);
                InvokeRepeating("CheckDestination", 0.5f, 0.1f);
            }
        }
	}

    private void MoveToTarget()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void CheckDestination()
    {
        float dist = navMeshAgent.remainingDistance;
        
        if (dist != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && 
            navMeshAgent.remainingDistance <= targetStoppingDistance) // Arrived.
        {
            navMeshAgent.Stop();
            animator.SetFloat("Velocity", 0.0f);

            CancelInvoke("MoveToTarget");
            CancelInvoke("CheckDestination");
            Debug.Log("At destination");

            if(target != null)
            {
                if(target.HasDialog)
                {
                    // Speak
                    StartConversation();
                }
                else
                {
                    // Wattebällchen ???
                }
            }
        }    
    }

    private void StartConversation()
    {
        target.StartConversation(QuitConversation);
        locked = true;
    }

    private void QuitConversation()
    {
        locked = false;
    }
}
