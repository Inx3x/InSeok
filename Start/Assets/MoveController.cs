using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float force;

    private Vector3 position;

    //private Rigidbody rigid;

    private void Awake()
    {
        //rigid = this.GetComponent<Rigidbody>();
    }

    void Start()
    {
        //rigid.useGravity = false;

        speed = 15.0f;
        force = 2000.0f;

        //rigid.AddForce(Vector3.forward * force);
    }
    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        //transform.Translate(0, 0, Time.deltaTime);
        //transform.Translate(0, 0, Time.deltaTime, Space.World);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        transform.Translate(hor * Time.deltaTime * speed, 0.0f, ver * Time.deltaTime * speed);
    }
}
