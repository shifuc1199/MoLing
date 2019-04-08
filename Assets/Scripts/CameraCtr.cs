using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtr : MonoBehaviour {
    public Transform _player;
    public float _speed;
    float _offset_x;
	// Use this for initialization
	void Start () {

        _offset_x = transform.position.x - _player.transform.position.x;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(_player.transform.position.x+ _offset_x,transform.position.y,transform.position.z), Time.deltaTime * _speed);
         
	}
}
