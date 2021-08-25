using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private GameObject TargetPoint;

    private bool Move;

    private Vector3 Step;
    private Rigidbody Rigid;



    // ** Enemy ������Ʈ �������� �߰�.
    public GameObject EnemyPrefab;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();

        TargetPoint = GameObject.Find("TargetPoint");

        EnemyPrefab = Resources.Load("Prefabs/Enemy") as GameObject;

    }

    void Start()
    {
        Rigid.useGravity = false;

        TargetPoint.transform.position = this.transform.position;

        Step = new Vector3(0.0f, 0.0f, 0.0f);

        Speed = 15.0f;

        Move = false;

        new GameObject("EnableList");
        new GameObject("DisableList");

        for (int i = 0; i < 5; i++)
        {
            ObjectManager.GetInstance.AddObject(
                Instantiate(EnemyPrefab));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = ObjectManager.GetInstance.GetDisableList.Pop();
            obj.gameObject.SetActive(true);
            obj.transform.parent = GameObject.Find("EnableList").transform;
            ObjectManager.GetInstance.GetEnableList.Add(obj);
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            // ** ȭ�鿡 �ִ� ���콺 ��ġ�κ��� Ray�� ���������� ������ �����.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        if (Move == true)
            this.transform.position += Step * Speed;
    }


    void RayPoint(Ray _ray)
    {
        // ** Ray�� Ÿ�ٰ� �浹������ ��ȯ ���� �����ϴ� ��.
        RaycastHit hit;

        // ** Physics.Raycast( Ray���� ��ġ�� ���� , �浹�� ������ ����, Mathf.Infinity = ������)
        // ** �ؼ� : ray�� ��ġ�� �������κ��� RayPoint�� �����ϰ� �߻��ϰ� �⵿�� �Ͼ�� Hit�� ������ ������.
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Ground")
            {
                // ** �ؼ� : ray�� ��ġ�� ���� hit�� ��ġ���� ���� �׸�. ���� ���ӿ����� �Ⱥ���.
                Debug.DrawLine(_ray.origin, hit.point);
                //Debug.Log(hit.point);

                TargetPoint.transform.position = hit.point;

                Move = true;
                Step = TargetPoint.transform.position - this.transform.position;
                Step.Normalize();
                Step.y = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") Destroy(other.gameObject);
        else Move = false;
    }
}