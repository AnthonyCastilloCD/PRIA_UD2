using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //References Patrol
    public Transform PatrolRoute;
    public List <Transform> Locations;
    private int _locationIndex = 0;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if(_agent.remainingDistance < 0.2f && !_agent.pathPending)
        {
            MoveToNextPatrolLocation();
        }
    }

    void InitializePatrolRoute()
    {
        foreach (Transform child in PatrolRoute)
        {
            Locations.Add (child);
        }
    }
    void MoveToNextPatrolLocation()
    {
        if (Locations.Count == 0)
        return;
        _agent.destination = Locations [_locationIndex].position;
        _locationIndex = (_locationIndex + 1) % Locations.Count;
    }
 void  OnTriggerEnter(Collider other)
 {
    if(other.name == "Player")
    {
        Debug.Log("Player detected - attack!");
    }
 }
 void OnTriggerExit(Collider other)
 {
    if(other.name == "Player")
    {
        Debug.Log("Player out of range, resume patrol");
    }
 }
}
