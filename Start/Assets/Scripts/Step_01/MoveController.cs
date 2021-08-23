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
            // ** 화면에 있는 마우스 위치로부터 Ray를 보내기위해 정보를 기록함.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        if(Move)    transform.position += Step * Time.deltaTime * Speed;
    }

    void RayPoint(Ray ray)
    {
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