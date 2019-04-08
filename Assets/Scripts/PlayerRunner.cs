using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class PlayerRunner : MonoBehaviour
{
    public float _up_down_speed;
    public float _speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector3(0,1,0)*ETCInput.GetAxis("Vertical") * _up_down_speed * Time.deltaTime);
        transform.Translate(transform.right * _speed * Time.deltaTime);
    }
}
