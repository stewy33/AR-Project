using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    readonly int force = 7;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void forward()
    {
        _rigidbody.AddForce(new Vector3(0, 0, force), ForceMode.VelocityChange);
    }

    public void backward()
    {
        _rigidbody.AddForce(new Vector3(0, 0, -force), ForceMode.VelocityChange);
    }

    public void right()
    {
        _rigidbody.AddForce(new Vector3(force, 0, 0), ForceMode.VelocityChange);
    }

    public void left()
    {
        _rigidbody.AddForce(new Vector3(-force, 0, 0), ForceMode.VelocityChange);
    }
}
