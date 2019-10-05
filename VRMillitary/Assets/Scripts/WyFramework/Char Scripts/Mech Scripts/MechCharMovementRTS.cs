﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechCharMovementRTS : MonoBehaviour
{
    public GameObject unit;

    public bool unitSelected;
    // current object is selected or not 
    public float moveSpeed = 20f;
    public float reachDestinationRadius = 10f;
    //Allow NPC to randomly move and roam around the map.
    public bool allowRandomMovement = true;
    //Walking Radius limit for randomMove
    public float randomMovementRadius = 500f;
    private Ray ray;
    private RaycastHit hit;
    private NavMeshAgent navMeshAgent;
    private Rigidbody myRB;

    // object that is right clicked by player // can be an enemy or an objects in world
    private GameObject targetObject;
    private Vector3 initialPosition;

    private bool IsPlayerTeam;
    private bool IsEnemyTeam;
    private bool IsNeutralTeam;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        myRB = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        navMeshAgent.speed = moveSpeed;

        // Don't move at start at all
        ResetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        myRB.velocity = Vector3.zero;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Player team will move by mouse
        if (Input.GetMouseButtonDown(1))
        {
            if (unitSelected)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    targetObject = hit.collider.gameObject;
                    if (IsPlayerTeam)
                    {
                        // Move character i
                        SetDestination(hit.point);
                    }
                }
            }
        }

        // Neutral team will randomlyMove 
        if (CheckReachedDestination())
        {
            ResetDestination();
            if (!IsPlayerTeam && allowRandomMovement)
            {
                SetDestination(RandomNavPoint(randomMovementRadius));
            }
            //enemy team will go back to initial location is walked too far away
            if (IsEnemyTeam)
            {
                if (Vector3.Distance(initialPosition, transform.position) > randomMovementRadius)
                {
                    ResetDestination();
                    SetDestination(initialPosition);
                }
            }
        }

        //if (IsPlayerTeam)
        //{
        //    if (unit.selectionCircle != null)
        //    {
        //        unit.selectionCircle.SetActive(unitSelected);
        //        unit.selectionCircle.GetComponent<SpriteRenderer>().color = Color.green;
        //    }
        //}

    }

    private void OnMouseEnter()
    {
        //if (IsPlayerTeam)
        //{
        //    unit.selectionCircle.SetActive(true);
        //    unit.selectionCircle.GetComponent<SpriteRenderer>().color = Color.green;
        //}
        //else if (IsNeutralTeam)
        //{
        //    unit.selectionCircle.SetActive(true);
        //    unit.selectionCircle.GetComponent<SpriteRenderer>().color = Color.black;
        //}
        //else if (IsEnemyTeam)
        //{
        //    unit.selectionCircle.SetActive(true);
        //    unit.selectionCircle.GetComponent<SpriteRenderer>().color = Color.red;
        //}
    }

    private void OnMouseExit()
    {
        //if (IsNeutralTeam)
        //{
        //    unit.selectionCircle.SetActive(false);
        //    unit.selectionCircle.GetComponent<SpriteRenderer>().color = Color.black;
        //}
        //else if (IsEnemyTeam)
        //{
        //    unit.selectionCircle.SetActive(false);
        //    unit.selectionCircle.GetComponent<SpriteRenderer>().color = Color.red;
        //}
    }
    public bool CheckReachedDestination()
    {
        // Check if we've reached the destination
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == reachDestinationRadius)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Random point in navmesh
    public Vector3 RandomNavPoint(float walkRadius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);

        return hit.position;
    }

    public void ResetDestination()
    {
        // reset destination
        navMeshAgent.Stop();
        navMeshAgent.ResetPath();
    }

    public void SetDestination(Vector3 location)
    {
        ResetDestination();
        navMeshAgent.destination = location;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed;
        }
    }
}