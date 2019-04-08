using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBallCtr : MonoBehaviour
{
    public float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime,Space.World);
    }
}
