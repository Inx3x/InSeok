using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    // ** 싱글톤 패턴 
    private static ObjectManager Instance = null;

    public static ObjectManager GetInstance 
    {
        // ** Getter = 반환만 가능
        get
        {
            if (Instance == null)
                Instance = new ObjectManager();

            return Instance;
        }
    }


    // ** 생성자 private 셋팅 : 외부생성을 허용하지 않음.
    private ObjectManager() { }

    

    // ** Enemy를 관리할 리스트.
    //private List<GameObject> EnemyList = new List<GameObject>();


    private List<GameObject> EnableList = new List<GameObject>();
    public List<GameObject> GetEnableList
    {
        get
        {
            return EnableList;
        }
    }


    private Stack<GameObject> DisableList = new Stack<GameObject>();
    public Stack<GameObject> GetDisableList
    {
        get
        {
            return DisableList;
        }
    }

    // ** 오브젝트를 리스트에 추가.
    public void AddObject(GameObject _Object) 
    {
        // ** EnemyController 이름의 스크립트를 복제된 오브젝트에 추가
        _Object.AddComponent<EnemyController>();

        // ** 하이라키부에 추가된 EnemyList의 빈 게임오브젝트를 부모로 셋팅 : 계층구조
        _Object.transform.parent = GameObject.Find("DisableList").transform;

        // ** 생성된 Enemy의 충돌체에 있는 Trigger 기능을 켬.
        _Object.GetComponent<BoxCollider>().isTrigger = true;

        // ** 생성된 오브젝트를 비활성화 설정
        _Object.gameObject.SetActive(false);

        // ** 리스트에 추가
        //EnemyList.Add(Obj);
        DisableList.Push(_Object);
    }
}
