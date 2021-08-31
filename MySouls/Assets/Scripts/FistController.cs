using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** 해당 컴퍼넌트를 삽입 : 현재 Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class FistController : MonoBehaviour
{
    private Rigidbody Rigid;

    private void Awake()
    {
        // ** 현재 오브젝트의 물리엔진 컴퍼넌트를 받아옴
        Rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Rigid.useGravity = false;

        Collider CollObj = GetComponent<SphereCollider>();

        CollObj.isTrigger = true;

        Rigid.AddForce(this.transform.forward * 500.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
