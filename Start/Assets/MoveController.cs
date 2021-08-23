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

        //** 힘을 가하여 이동시킴.
        //Force = 2000.0f;
        //Rigid.AddForce(Vector3.forward * Force);
        //** Update 함수는 프레임마다 호출 되기 때문에 AddForce 함수를 Update함수에서 호출하게 되면
        //** 매 프레임 마다 힘을 가하게 되므로 속도가 가중됨.
    }

    private void FixedUpdate()
    {
        float key = Input.GetAxis("Q");

        Debug.Log("Q" + key);
        // ** 게임 오브젝트 기준으로 이동.  (로컬)
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        // ** 절대 좌표 기준으로 이동.  (월드)
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);

        // ** 물체를 앞쪽 방향으로 이동.  (로컬)
        //transform.Translate(0, 0, Time.deltaTime * Speed); //** Translate(x, y, z);

        // ** 물체를 앞쪽 방향으로 이동.  (월드)
        //transform.Translate(0, 0, Time.deltaTime * Speed, Space.World); //** Translate(x, y, z, Space);

        // ** 카메라를 기준으로 개체를 앞쪽으로 이동.
        //transform.Translate(Vector3.forward * Time.deltaTime * Speed, Camera.main.transform);

        // ** 키 입력에 의한 이동방법.
        /*
        float fHor = Input.GetAxis("Horizontal");
        float fVer = Input.GetAxis("Vertical");
        transform.Translate(
            fHor * Time.deltaTime * Speed,
            0.0f,
            fVer * Time.deltaTime * Speed);
        */


        // ** 마우스 키 입력 방법.
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("좌클릭");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("우클릭");
        }
        if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("휠 클릭");
        }
        */

        if (Input.GetMouseButton(1))
        {
            // ** 화면에 있는 마우스 위치로부터 Ray를 보내기위해 정보를 기록함.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ** Ray가 타겟과 충돌했을때 반환 값을 저장하는 곳.
            RaycastHit hit;

            // ** Physics.Raycast( Ray시작 위치와 방향 , 충돌한 지점의 정보, Mathf.Infinity = 무한한)
            // ** 해석 : ray의 위치와 방향으로부터 RayPoint를 무한하게 발사하고 출동이 일어나면 Hit에 정보를 저장함.
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Ground")
                {
                    // ** 해석 : ray의 위치로 부터 hit된 위치까지 선을 그림. 실제 게임에서는 안보임.
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

    //충돌했을때
    private void OnTriggerEnter(Collision collision)
    {
        Move = false;
    }
}