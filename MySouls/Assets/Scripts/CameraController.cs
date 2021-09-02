using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    private GameObject Target;
    private Camera MainCamera;

    private void Awake()
    {
        Target = GameObject.Find("Player");
        this.transform.position = Camera.main.transform.position;
    }
    private void Start()
    {
        this.transform.position = new Vector3(
            0.0f + Target.transform.position.x,
            6.5f + Target.transform.position.y,
            -4.0f + Target.transform.position.z
            );
    }
    private void Update()
    {
        this.transform.position = Target.transform.position + (Vector3.forward * -4.0f) + (Vector3.up * 6.5f);
    }
}
