using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** 해당 컴퍼넌트를 삽입 : 현재 Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody Rigid;

    // ** Enemy 오브젝트 프리팹을 추가.
    public GameObject EnemyPrefab;


    void Awake()
    {
        // ** 현재 오브젝트의 물리엔진 컴퍼넌트를 받아옴
        Rigid = GetComponent<Rigidbody>();

        // ** Resources 폴더 안에 있는 리소스를 불러옴.
        // ** Resources.Load("경로") as GameObject;  <= 의 형태 
        EnemyPrefab = Resources.Load("Prefab/EnemyPrefabs/TurtleShellPBR") as GameObject;
    }

    void Start()
    {
        // ** 물리엔진의 중력을 비활성화.
        Rigid.useGravity = false;

        // ** 이동속도
        Speed = 5.0f;

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
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        this.transform.Translate(
            Hor * Speed * Time.deltaTime, 
            0.0f, 
            Ver * Speed * Time.deltaTime);

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
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


