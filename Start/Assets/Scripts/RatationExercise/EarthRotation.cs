using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotation : MonoBehaviour
{
    private GameObject sunObj;

    private void Awake()
    {
        sunObj = GameObject.Find("Sun");
    }
    void Start()
    {
        transform.parent = sunObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * Time.deltaTime * 15);
    }
}
