using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    public GameObject WayPoint;

    private bool Move;

    private Vector3 Step;

    private float Speed;

    private Rigidbody Rigid;

    public GameObject FistPrefab;

    private bool FistallCheck;

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

        FistPrefab = Resources.Load("Prefabs/Fist") as GameObject;
    }

    private void Start()
    {
        Speed = 0.05f;

        Rigid.useGravity = false;

        FistallCheck = false;

        this.transform.parent = GameObject.Find("EnableList").transform;

        this.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        Initialize();

        StartCoroutine("Fistall");
    }

    private void OnEnable()
    {
        this.transform.parent = GameObject.Find("EnableList").transform;

        this.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        Initialize();
    }
    private void Update()
    {
        if (FistallCheck)
        {
            GameObject Obj = Instantiate(FistPrefab);

            Obj.gameObject.AddComponent<FistController>();

            FistallCheck = false;

            StartCoroutine("Fistall");
        }

    }
    private void FixedUpdate()
    {
        if (Move == true)
        {
            this.transform.position += Step * Speed;
            Debug.DrawLine(this.transform.position, WayPoint.transform.position);
        }
    }

    private void Initialize()
    {
        WayPoint.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        Move = true;

        Step = WayPoint.transform.position - this.transform.position;

        Step.Normalize();

        Step.y = 0;

        WayPoint.transform.position.Set(
            WayPoint.transform.position.x,
            0.0f,
            WayPoint.transform.position.z);

        this.transform.LookAt(WayPoint.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WayPoint")
        {
            Move = false;
            StartCoroutine("EnemyState");
        }

        if (other.tag == "Ground")
        {
            Destroy(other.gameObject);
        }
    }

    IEnumerator EnemyState()
    {
        Debug.Log("���� ��ȯ");
        yield return new WaitForSeconds(Random.Range(3, 5));

        Initialize();
    }

    IEnumerator Fistall()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));

        FistPrefab.transform.position = this.transform.position;

        FistPrefab.transform.LookAt(WayPoint.transform.position);

        FistallCheck = true;
    }
}