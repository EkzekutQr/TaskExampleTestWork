using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour, IPushable
{
    [SerializeField] private float _maxSpeed = 1f;
    [SerializeField] private float _acceleration = 1f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = _rb.velocity;
        direction.y = 0;
        if(_rb.velocity.magnitude < _maxSpeed)
        _rb.AddForce(direction * _acceleration * Time.fixedDeltaTime);
    }

    public void Push(Vector3 direction, float power)
    {
        _rb.AddForce(direction * power, ForceMode.Impulse);
    }
}

public interface IPushable
{
    void Push(Vector3 direction, float power);
}