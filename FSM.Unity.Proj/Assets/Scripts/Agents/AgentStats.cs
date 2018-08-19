using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Finite State Machine/Agent Stats")]
public class AgentStats : ScriptableObject
{
    public float MovementSpeed = 1;
    public float RotationSpeed = 0.5f;
    public float ViewDistance = 40f;
    public float ViewRadius = 1f;

    public float AttackRange = 40;
    public float AttackSpeed = 1f;
    public int AttackDamage = 50;

    public float VisionDropDuration = 2f;
    public float SearchDuration = 4f;
    public float SearchingTurnSpeed = 120f;

    public float PursueStoppingDistance = 5.0f;
    public float PatrolStoppingDistance = 0.0f;
}
