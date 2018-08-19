using Assets.Scripts.FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {
    private IStateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;

    // <Serialized> fields: will be populated by the inspector
    public Transform Eyes;
    public AgentStats Stats;
    // public List<Transform> WayPointList;
    public GameObject BulletPrefab;

    // public Transform Target { get; set; }

    public Transform GetTransform() {
        return GetComponent<Transform>();
    }

    public Transform GetEyes() {
        ValidateComponent(Eyes, "Eyes Transform");

        return Eyes;
    }

    /*public List<Transform> GetWayPoints() {
        ValidateComponent(WayPointList, "Patrol Waypoints");

        return WayPointList;
    }*/

    public NavMeshAgent GetNavMeshAgent() {
        ValidateComponent(_navMeshAgent, "Agent Navmesh");

        return _navMeshAgent;
    }

    public AgentStats GetStats() {
        ValidateComponent(Stats, "Agent Stats");

        return Stats;
    }

    public void DefaultAttack()
    {
        var spawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z) + transform.forward;
        Instantiate(BulletPrefab, spawnPosition, transform.rotation);
    }

    public void RotateTowardsTarget()
    {
        var destination = _navMeshAgent.destination;

        if ((destination - transform.position).magnitude < 0.1f) return;

        var direction = (destination - transform.position).normalized;
        var rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Stats.RotationSpeed);
    }

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new StateMachine();
        _stateMachine.Init(this);
        _stateMachine.Start();
    }

    // Little helper method here to warn you if you've forgotten to add the Inspector Fields.
    private void ValidateComponent<T>(T component, string componentName) {
        if (component == null) {
            Debug.LogError($"Missing component[{componentName}] in {this.gameObject.name}");
        }
    }

    void Update()
    {
        _stateMachine.Update();
    }
}
