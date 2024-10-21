using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
}
