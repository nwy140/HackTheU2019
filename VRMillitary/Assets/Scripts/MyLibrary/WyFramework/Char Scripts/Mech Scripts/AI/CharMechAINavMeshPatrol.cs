using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharMechAINavMeshPatrol : MonoBehaviour
{
	public List<Transform> patrolPoints;
	public int patrolIndex = 0;
	public float destinationRadius ; 
	
	private NavMeshAgent mNavMeshAgent;
    void Start()
	{
	    mNavMeshAgent = GetComponent<NavMeshAgent>();
        
	    setPatrolDestinationByIndex(patrolIndex);
    }
    
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(CheckHasReachDestination(destinationRadius)){
			patrolIndex++;
			setPatrolDestinationByIndex(patrolIndex);
			
			if(patrolIndex >= patrolPoints.Count - 1) { // if it's a last point
				patrolIndex = 0;
			}
		}
	}
	
	public void setPatrolDestinationByIndex(int indexVal){
		mNavMeshAgent.destination = patrolPoints[indexVal].position;
	}
    
	// TODO fix destinationRadius
	public bool CheckHasReachDestination(float destinationRadius){
		// Check if we've reached the destination
		if (!mNavMeshAgent.pathPending)
		{
			if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance - destinationRadius)
			{
				if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude == 0f)
				{
					// Done
					return true;
				}
			}
		}
		return false;
	}

}

