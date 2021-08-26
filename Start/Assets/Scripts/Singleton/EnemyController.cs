using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ** 해당 컴퍼넌트를 삽입 : 현재 Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public GameObject WayPoint;

    private bool Move;

    private Vector3 Step;

    private float Speed;

    private Rigidbody Rigid;

    private float IdleTime;

    public GameObject Bullet;

    private void Awake()
    {
        // ** 현재 오브젝트의 물리엔진 컴퍼넌트를 받아옴
        Rigid = GetComponent<Rigidbody>();

        // ** WayPoint 라는 이름의 가상의 목표지점을 생성.
        WayPoint = new GameObject("WayPoint");

        // ** WayPoint 의 tag 를 WayPoint로 설정
        WayPoint.transform.tag = "WayPoint";

        // ** 가상의 목표지점에 콜라이더를 삽입.
        WayPoint.AddComponent<SphereCollider>();

        // ** 삽인된 콜라이더에 정보를 받아옴
        SphereCollider Sphere = WayPoint.GetComponent<SphereCollider>();

        // ** 콜라이더의 크기를 변경
        Sphere.radius = 0.2f;

        // ** isTrigger = true
        Sphere.isTrigger = true;

    }

    private void Start()
    {
        // ** 대기 상태 시간.
        IdleTime = 3.0f;

        Speed = 0.05f;

        Rigid.useGravity = false;

        //Initialize();
    }

    private void OnEnable()
    {
        Initialize();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Move == true)
        {
            this.transform.position += Step * Speed;
            Debug.DrawLine(
                this.transform.position,
                WayPoint.transform.position);
        }
        else
        {
            IdleTime -= Time.deltaTime;

            if (IdleTime < 0)
            {
                // ** WayPoint 이동 목표위치 :  난수 함수 = Random.Range(Min, Max)
                WayPoint.transform.position = new Vector3(
                    Random.Range(-25, 25),
                    0.0f,
                    Random.Range(-25, 25));

                //** 타겟이 생성되었으니 움직일수 있도록 true로 변경
                Move = true;

                // ** 타겟의 방향을 바라보는 벡터를 구함.
                Step = WayPoint.transform.position - this.transform.position;

                // ** 방향만 남겨주고
                Step.Normalize();

                // ** 남은 방향에 Y값은 그 값조차 없애버림. 오작동 방지.
                Step.y = 0;

                // ** 대기 시간 셋팅.
                IdleTime = Random.Range(3, 5);
            }
        }
    }

    private void Initialize()
    {
        this.transform.parent = GameObject.Find("EnableList").transform;

        // ** WayPoint 이동 목표위치 :  난수 함수 = Random.Range(Min, Max)
        WayPoint.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        // ** 현재 자신의 위치 : 난수 함수 = Random.Range(Min, Max)
        this.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        //** 타겟이 생성되었으니 움직일수 있도록 true로 변경
        Move = true;

        // ** 타겟의 방향을 바라보는 벡터를 구함.
        Step = WayPoint.transform.position - this.transform.position;

        // ** 방향만 남겨주고
        Step.Normalize();

        // ** 남은 방향에 Y값은 그 값조차 없애버림. 오작동 방지.
        Step.y = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            Move = false;
        }

        if (other.tag == "Ground")
        {
            Destroy(other.gameObject);
        }
    }
}