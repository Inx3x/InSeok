using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float Speed;
    private bool Move;

    private Vector3 Step;

    private Rigidbody Rigid;

    public GameObject TargrtPoint;
    
    public GameObject Target;

    void Awake()
    {
        Rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Rigid.useGravity = false;
        TargrtPoint.transform.position = this.transform.position;
        Step = new Vector3(0.0f, 0.0f, 0.0f);
        Speed = 15.0f;
        Move = false;

        //** ���� ���Ͽ� �̵���Ŵ.
        //Force = 2000.0f;
        //Rigid.AddForce(Vector3.forward * Force);
        //** Update �Լ��� �����Ӹ��� ȣ�� �Ǳ� ������ AddForce �Լ��� Update�Լ����� ȣ���ϰ� �Ǹ�
        //** �� ������ ���� ���� ���ϰ� �ǹǷ� �ӵ��� ���ߵ�.
    }

    private void FixedUpdate()
    {
        float key = Input.GetAxis("Q");

        Debug.Log("Q" + key);
        // ** ���� ������Ʈ �������� �̵�.  (����)
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        // ** ���� ��ǥ �������� �̵�.  (����)
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);

        // ** ��ü�� ���� �������� �̵�.  (����)
        //transform.Translate(0, 0, Time.deltaTime * Speed); //** Translate(x, y, z);

        // ** ��ü�� ���� �������� �̵�.  (����)
        //transform.Translate(0, 0, Time.deltaTime * Speed, Space.World); //** Translate(x, y, z, Space);

        // ** ī�޶� �������� ��ü�� �������� �̵�.
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed, Camera.main.transform);

        // ** Ű �Է¿� ���� �̵����.
        /*
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");
        transform.Translate(
            fHor * Time.deltaTime * Speed,
            0.0f,
            fVer * Time.deltaTime * Speed);
        */


        // ** ���콺 Ű �Է� ���.
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("��Ŭ��");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("��Ŭ��");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("�� Ŭ��");
        }
        */

        if (Input.GetMouseButton(1))
        {
            // ** ȭ�鿡 �ִ� ���콺 ��ġ�κ��� Ray�� ���������� ������ �����.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

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
                }
            }

            Move = true;

            Step = TargrtPoint.transform.position - this.transform.position;
            Step.Normalize();
            Step.y = 0;
        }

        if (Move) this.transform.position += Step * Time.deltaTime * Speed;
    }

    //�浹������
    private void OnTriggerEnter(Collision collision)
    {
        Move = false;
    }
}