using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ColliderTrigger : MonoBehaviour
{
    [TagField]
    public string tag = string.Empty;

    public UnityEvent events;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag==tag)
        {
            events.Invoke();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
