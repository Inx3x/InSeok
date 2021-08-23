using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private bool Move;

    private GameObject TargrtPoint;

    private Vector3 Step;

    private Rigidbody Rigid;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();

        TargrtPoint = GameObject.Find("TargrtPoint");
    }

    void Start()
    {
        Rigid.useGravity = false;
        TargrtPoint.transform.position = transform.position;
        Step = new Vector3(0.0f, 0.0f, 0.0f);
        Speed = 15.0f;
        Move = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            // ** ȭ�鿡 �ִ� ���콺 ��ġ�κ��� Ray�� ���������� ������ �����.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        if(Move)    transform.position += Step * Time.deltaTime * Speed;
    }

    void RayPoint(Ray ray)
    {
        // ** Ray�� Ÿ�ٰ� �浹������ ��ȯ ���� �����ϴ� ��.
        RaycastHit hit;

        // ** Physics.Raycast( Ray���� ��ġ�� ���� , �浹�� ������ ����, Mathf.Infinity = ������)
        // ** �ؼ� : ray�� ��ġ�� �������κ��� RayPoint�� �����ϰ� �߻��ϰ� �⵿�� �Ͼ�� Hit�� ������ ������.
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Ground")
            {
                // ** �ؼ� : ray�� ��ġ�� ���� hit�� ��ġ���� ���� �׸�. ���� ���ӿ����� �Ⱥ���.
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log(hit.point);

                TargrtPoint.transform.position = hit.point;


                Move = true;
                Step = TargrtPoint.transform.position - this.transform.position;
                Step.Normalize();
                Step.y = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Move = false;
    }
}