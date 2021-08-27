using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FistController : MonoBehaviour
{
    private Rigidbody Rigid;
    private void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Rigid.useGravity = false;

        Collider CollObj = GetComponent<SphereCollider>();

        CollObj.isTrigger = true;

        Rigid.AddForce(this.transform.forward * 500.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground") Destroy(this.gameObject);
    }

}
