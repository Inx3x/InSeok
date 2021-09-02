using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ** �ش� ���۳�Ʈ�� ���� : ���� Rigidbody
[RequireComponent(typeof(Rigidbody))]
public class MoveController : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody Rigid;

    // ** Enemy ������Ʈ �������� �߰�.
    public GameObject EnemyPrefab;


    void Awake()
    {
        // ** ���� ������Ʈ�� �������� ���۳�Ʈ�� �޾ƿ�
        Rigid = GetComponent<Rigidbody>();

        // ** Resources ���� �ȿ� �ִ� ���ҽ��� �ҷ���.
        // ** Resources.Load("���") as GameObject;  <= �� ���� 
        EnemyPrefab = Resources.Load("Prefab/EnemyPrefabs/TurtleShellPBR") as GameObject;
    }

    void Start()
    {
        // ** ���������� �߷��� ��Ȱ��ȭ.
        Rigid.useGravity = false;

        // ** �̵��ӵ�
        Speed = 5.0f;

        // ** ���̶�Ű �信 "EnemyList" �̸��� �� ���� ������Ʈ�� �߰�
        //GameObject ViewObject = new GameObject("EnablsList");
        new GameObject("EnableList");
        new GameObject("DisableList");


        for (int i = 0; i < 5; ++i)
        {
            // ** Instantiate = �����Լ�
            // ** EnemyPrefab �� ������Ʈ�� ������
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

        // ** �����̽� Ű �Է��� �޾�����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ** Stack �� �����Ͱ� �����ִ��� Ȯ���ϰ� ���»��¶�� �߰��Ѵ�.
            if (ObjectManager.GetInstance.GetDisableList.Count == 0)
                for (int i = 0; i < 5; ++i)
                    ObjectManager.GetInstance.AddObject(
                        Instantiate(EnemyPrefab));

            // ** GetDisableList �� �ִ� ��ü �ϳ��� ������
            GameObject Obj = ObjectManager.GetInstance.GetDisableList.Pop();

            // ** ������ ��ü�� Ȱ��ȭ ���� �����·� ����
            Obj.gameObject.SetActive(true);

            // ** Ȱ��ȭ�� ������Ʈ�� �����ϴ� ����Ʈ�� ���Խ�Ŵ.
            ObjectManager.GetInstance.GetEnableList.Add(Obj);
        }
        // ** ��Ȱ��ȭ ���¿��� Ȱ��ȭ ���·� �����ϰ�, ����� ������Ʈ�� 
        // ** Ȱ��ȭ�� ������Ʈ�� ���ִ� ����Ʈ���� ����� ���������� ���� �ȴ�.
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // ** EnableList�� �ִ� ��ü�� DisableList �� ����
            other.transform.parent = GameObject.Find("DisableList").transform;

            // ** ��ü�� DisableList �̵�
            ObjectManager.GetInstance.GetDisableList.Push(other.gameObject);

            // ** EnableList �� �ִ� ��ü ������ ����
            ObjectManager.GetInstance.GetEnableList.Remove(other.gameObject);

            // ** �̵��� �Ϸ�Ǹ� ��ü�� ��Ȱ��ȭ
            other.gameObject.SetActive(false);
        }
    }
}


