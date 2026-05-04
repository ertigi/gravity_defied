using System;
using UnityEngine;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Game/Configs/Player Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [field: SerializeField] public float JumpForce { get; private set; } = 8f;
    [field: SerializeField] public float GravityForce { get; private set; } = 25f;
    [field: SerializeField] public float GroundCheckDistance { get; private set; } = 0.15f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 12f;
}
