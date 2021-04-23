using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _startPosition;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = _rigidbody.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.transform.position = _startPosition;
        }
    }
}
