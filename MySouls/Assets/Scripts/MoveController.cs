using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** 해당 컴퍼넌트를 삽입 : 현재 Rigidbody
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
        // ** 현재 오브젝트의 물리엔진 컴퍼넌트를 받아옴
        Rigid = GetComponent<Rigidbody>();

        // ** TargetPoint 라는 이름의 객체를 찾는다.
        // ** 이렇게 받아올때에는 이름을 신경써야한다.
        // ** 같은 이름의 객체가 있다면 문제가 생길 수 있음.
        TargetPoint = GameObject.Find("TargetPoint");


        // ** Resources 폴더 안에 있는 리소스를 불러옴.
        // ** Resources.Load("경로") as GameObject;  <= 의 형태 
        EnemyPrefab = Resources.Load("Prefab/EnemyPrefabs/TurtleShellPBR") as GameObject;
    }

    void Start()
    {
        // ** 물리엔진의 중력을 비활성화.
        Rigid.useGravity = false;

        // ** 시작할때 TargetPoint 위치를 현재 오브젝트의 위치로 초기화
        TargetPoint.transform.position = this.transform.position;

        // ** Step = 방향 : 시작할때에는 그 방향을 갖지 않는다.
        Step = new Vector3(0.0f, 0.0f, 0.0f);

        // ** 이동속도
        Speed = 0.5f;

        // ** Move = 이동상태 : 시작할때 정지상태로 만듬
        Move = false;



        // ** 하이라키 뷰에 "EnemyList" 이름의 빈 게임 오브젝트를 추가
        //GameObject ViewObject = new GameObject("EnablsList");
        new GameObject("EnableList");
        new GameObject("DisableList");


        for (int i = 0; i < 5; ++i)
        {
            // ** Instantiate = 복제함수
            // ** EnemyPrefab 의 오브젝트를 복제함
            //GameObject Obj = Instantiate(EnemyPrefab);
            //ObjectManager.GetInstance.AddObject(Obj);

            ObjectManager.GetInstance.AddObject(
                Instantiate(EnemyPrefab));
        }
    }


    private void Update()
    {
        // ** 스페이스 키 입력을 받았을때
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ** Stack 에 데이터가 남아있는지 확인하고 없는상태라면 추가한다.
            if (ObjectManager.GetInstance.GetDisableList.Count == 0)
                for (int i = 0; i < 5; ++i)
                    ObjectManager.GetInstance.AddObject(
                        Instantiate(EnemyPrefab));

            // ** GetDisableList 에 있는 객체 하나를 버리고
            GameObject Obj = ObjectManager.GetInstance.GetDisableList.Pop();

            // ** 버려진 객체를 활성화 시켜 사용상태로 변경
            Obj.gameObject.SetActive(true);

            // ** 활성화된 오브젝트를 관리하는 리스트에 포함시킴.
            ObjectManager.GetInstance.GetEnableList.Add(Obj);
        }
        // ** 비활성화 상태에서 활성화 상태로 변경하고, 변경된 오브젝트는 
        // ** 활성화된 오브젝트만 모여있는 리스트에서 사용이 끝날때까지 관리 된다.
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            // ** 화면에 있는 마우스 위치로부터 Ray를 보내기위해 정보를 기록함.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RayPoint(ray);
        }

        // ** Move 가 true일때는 이동 가능한 상태를 말한다.
        if (Move == true)
            // ** Step = 방향, Speed = 속도 
            // ** Step 방향으로 Speed 만큼 이동시킴
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
                Debug.Log(hit.point);

                // ** hit된 위치를 타겟 좌표로 받아옴
                TargetPoint.transform.position = hit.point;

                //** 타겟이 생성되었으니 움직일수 있도록 true로 변경
                Move = true;

                // ** 타겟의 방향을 바라보는 벡터를 구함.
                Step = TargetPoint.transform.position - this.transform.position;

                // ** 방향만 남겨주고
                Step.Normalize();

                // ** 남은 방향에 Y값은 그 값조차 없애버림. 오작동 방지.
                Step.y = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ** 충돌된 객체의 이름이 TargetPoint 가 아니라면 무시하고 TargetPoint 일때 멈춤.
        if (other.name == "TargetPoint")
            Move = false;

        if (other.tag == "Enemy")
        {
            // ** EnableList에 있던 객체를 DisableList 로 변경
            other.transform.parent = GameObject.Find("DisableList").transform;

            // ** 객체를 DisableList 이동
            ObjectManager.GetInstance.GetDisableList.Push(other.gameObject);

            // ** EnableList 에 있던 객체 참조를 삭제
            ObjectManager.GetInstance.GetEnableList.Remove(other.gameObject);

            // ** 이동이 완료되면 객체를 비활성화
            other.gameObject.SetActive(false);
        }
    }
}


