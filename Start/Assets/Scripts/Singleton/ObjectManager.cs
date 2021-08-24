using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager Instance = null;

    public static ObjectManager GetInstance { 
        get {
            if (Instance == null) Instance = new ObjectManager();
            return Instance;
        } 
    }

    private ObjectManager() { }

    public GameObject enemyPrefab;

    private List<GameObject> enemyList = new List<GameObject>();

    private void Start()
    {
        
        for (int i = 0; i < 5; i++)
        {
            GameObject gameObj = Instantiate(enemyPrefab);

            gameObj.transform.parent = GameObject.Find("EnemyList").transform;
            
            gameObj.transform.position = new Vector3(Random.Range(-25, 25), 0.0f, Random.Range(-25, 25));

            enemyList.Add(gameObj);
        }
    }

}
