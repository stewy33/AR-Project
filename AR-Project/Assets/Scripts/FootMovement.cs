using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void forward()
    {
        _rigidbody.transform.position = new Vector3(_rigidbody.transform.position.x, _rigidbody.transform.position.y, _rigidbody.transform.position.z +1);
    }
}
