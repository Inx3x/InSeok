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



    // ** Enemy 오브젝트 프리팹을 추가.
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
            // ** 화면에 있는 마우스 위치로부터 Ray를 보내기위해 정보를 기록함.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        if (Move == true)
            this.transform.position += Step * Speed;
    }


    void RayPoint(Ray _ray)
    {
        // ** Ray가 타겟과 충돌했을때 반환 값을 저장하는 곳.
        RaycastHit hit;

        // ** Physics.Raycast( Ray시작 위치와 방향 , 충돌한 지점의 정보, Mathf.Infinity = 무한한)
        // ** 해석 : ray의 위치와 방향으로부터 RayPoint를 무한하게 발사하고 출동이 일어나면 Hit에 정보를 저장함.
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Ground")
            {
                // ** 해석 : ray의 위치로 부터 hit된 위치까지 선을 그림. 실제 게임에서는 안보임.
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