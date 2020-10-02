using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField]
    private FloatingJoystick floatingJoystick;

   /* [SerializeField]
    private ISheepInput sheepInput;*/

    [SerializeField]
    [Range(1f,6f)]
    private float moveSpeed = 4f;
    [SerializeField]
    [Range(1f, 5f)]
    private float turnSpeed = 1f;
    [SerializeField]
    private float forcePush = 200f;
    
    private Vector3 _moveDirection;
    private Rigidbody _rb;

    public void Initialize(FloatingJoystick joystick)
    {
        floatingJoystick = joystick;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ReadInput();
        Move();
        Rotation();
    }

    private void Rotation()
    {
        if (_moveDirection.sqrMagnitude <= Mathf.Epsilon)
            return;
        Quaternion lookRotation = Quaternion.LookRotation(_moveDirection);
        /*Quaternion turnRotation*/ transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed*Time.deltaTime);
        //_rb.MoveRotation(turnRotation);
    }

    private void ReadInput()
    {
        if (floatingJoystick == null)
        {
            _moveDirection = Vector3.zero;
            return;
        }
        _moveDirection = new Vector3(floatingJoystick.Horizontal, 0, floatingJoystick.Vertical);
    }

    private void Move()
    {
        Vector3 movement = transform.forward * _moveDirection.sqrMagnitude * Time.deltaTime * moveSpeed;
        transform.position += movement;
        //_rb.MovePosition(_rb.position + movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Sheep anotherSheep = collision.gameObject.GetComponent<Sheep>();
        if (anotherSheep != null)
        {
            Vector3 direction = collision.transform.position - transform.position;
            _rb.AddForce(-direction * forcePush);
            anotherSheep.GetComponent<Rigidbody>().AddForce(direction * forcePush);
        }
    }
}
