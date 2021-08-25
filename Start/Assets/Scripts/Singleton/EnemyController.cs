using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnEnable()
    {
        this.transform.position = new Vector3(
            Random.Range(-25, 25),
            0.0f,
            Random.Range(-25, 25));

        this.transform.parent = GameObject.Find("EnableList").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
