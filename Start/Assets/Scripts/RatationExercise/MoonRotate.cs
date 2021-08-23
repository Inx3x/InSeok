using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotate : MonoBehaviour
{
    private GameObject earthObj;

    private void Awake()
    {
        earthObj = GameObject.Find("Earth");
    }
    void Start()
    {
        transform.parent = earthObj.transform;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
