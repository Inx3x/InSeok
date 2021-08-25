using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Tank
{
    public float speed = 15.0f;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        Tank tank1 = new Tank();
        Tank tank2 = new Tank();
        Tank tank3 = new Tank();
        Tank tank4 = new Tank();
        Tank tank5 = new Tank();
    }

    float _yAngle = 0.0f;
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnKeyboard()
    {
        _yAngle += Time.deltaTime * _speed;

        //transform.eulerAngles = new Vector3(0.0f, Time.deltaTime * _speed, 0.0f);
        // Quaternion qt = transform.rotation;
        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += (Vector3.forward * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += (Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += (Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += (Vector3.right * Time.deltaTime * _speed);
        }
    }
}
