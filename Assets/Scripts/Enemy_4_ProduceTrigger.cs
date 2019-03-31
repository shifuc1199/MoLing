using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4_ProduceTrigger : MonoBehaviour
{
    public Enemy_4_Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
         
            if (!controller.IsInvoking())
                controller.InvokeRepeating("Produce", 0, 5);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (controller.IsInvoking())
                controller.CancelInvoke();

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
