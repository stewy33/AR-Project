using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    readonly int force = 10;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = _rigidbody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetFoot()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.transform.position = _startPosition;
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
