using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class CollisionExercise : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float speed;
    private bool move;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        rigidbody.useGravity = false;
        speed = 10.0f;
        move = true;
    }

    private void FixedUpdate()
    {
        if(move) transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!!");
        move = false;
    }
}
