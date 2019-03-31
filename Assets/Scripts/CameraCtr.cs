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
        
        transform.position = Vector3.Lerp(transform.position, temp, Time.deltaTime * _speed);
         
	}
}
