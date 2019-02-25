using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour {
    public Transform _player;
    public float _speed;
    Vector3 _offset;
	// Use this for initialization
	void Start () {
        
        _offset = transform.position - _player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 temp = _player.transform.position + _offset;
        if (temp.y < -223)
        {
            temp = new Vector3(temp.x, -223, temp.z);
        }
        if (temp.x > 320)
        {
            temp = new Vector3(320, temp.y, temp.z);
        }
        transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * _speed);
         
	}
}
