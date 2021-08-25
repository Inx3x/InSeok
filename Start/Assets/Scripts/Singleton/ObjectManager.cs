using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    private static ObjectManager Instance = null;

    public static ObjectManager GetInstance { 
        get {
            if (Instance == null) Instance = new ObjectManager();
            return Instance;
        } 
    }

    private ObjectManager() { }

    private List<GameObject> enableList = new List<GameObject>();
    public List<GameObject> GetEnableList { get { return enableList; } }

    private Stack<GameObject> disableList = new Stack<GameObject>();
    public Stack<GameObject> GetDisableList { get { return disableList; } }

    public void AddObject(GameObject _object)
    {
        _object.AddComponent<EnemyController>();

        _object.transform.parent = GameObject.Find("DisableList").transform;

        _object.GetComponent<BoxCollider>().isTrigger = true;

        _object.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        _object.gameObject.SetActive(false);

        disableList.Push(_object);
    }
}
