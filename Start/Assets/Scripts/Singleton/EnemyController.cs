using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ** �ش� ���۳�Ʈ�� ���� : ���� Rigidbody
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
        // ** ���� ������Ʈ�� �������� ���۳�Ʈ�� �޾ƿ�
        Rigid = GetComponent<Rigidbody>();

        // ** WayPoint ��� �̸��� ������ ��ǥ������ ����.
        WayPoint = new GameObject("WayPoint");

        // ** WayPoint �� tag �� WayPoint�� ����
        WayPoint.transform.tag = "WayPoint";

        // ** ������ ��ǥ������ �ݶ��̴��� ����.
        WayPoint.AddComponent<SphereCollider>();

        // ** ���ε� �ݶ��̴��� ������ �޾ƿ�
        SphereCollider Sphere = WayPoint.GetComponent<SphereCollider>();

        // ** �ݶ��̴��� ũ�⸦ ����
        Sphere.radius = 0.2f;

        // ** isTrigger = true
        Sphere.isTrigger = true;

    }

    private void Start()
    {
        // ** ��� ���� �ð�.
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
                // ** WayPoint �̵� ��ǥ��ġ :  ���� �Լ� = Random.Range(Min, Max)
                WayPoint.transform.position = new Vector3(
                    Random.Range(-25, 25),
                    0.0f,
                    Random.Range(-25, 25));

                //** Ÿ���� �����Ǿ����� �����ϼ� �ֵ��� true�� ����
                Move = true;

                // ** Ÿ���� ������ �ٶ󺸴� ���͸� ����.
                Step = WayPoint.transform.position - this.transform.position;

                // ** ���⸸ �����ְ�
                Step.Normalize();

                // ** ���� ���⿡ Y���� �� ������ ���ֹ���. ���۵� ����.
                Step.y = 0;

                // ** ��� �ð� ����.
                IdleTime = Random.Range(3, 5);
            }
        }
    }

    private void Initialize()
    {
        this.transform.parent = GameObject.Find("EnableList").transform;

        // ** WayPoint �̵� ��ǥ��ġ :  ���� �Լ� = Random.Range(Min, Max)
        WayPoint.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        // ** ���� �ڽ��� ��ġ : ���� �Լ� = Random.Range(Min, Max)
        this.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        //** Ÿ���� �����Ǿ����� �����ϼ� �ֵ��� true�� ����
        Move = true;

        // ** Ÿ���� ������ �ٶ󺸴� ���͸� ����.
        Step = WayPoint.transform.position - this.transform.position;

        // ** ���⸸ �����ְ�
        Step.Normalize();

        // ** ���� ���⿡ Y���� �� ������ ���ֹ���. ���۵� ����.
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