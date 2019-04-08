using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_ProduceTrigger : MonoBehaviour
{
    public Enemy_2_Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (controller._machine == null)
                return;
            if (controller._hurtcontroller.isdie)
                return;

            controller. _machine.ChangeState("shoot");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (controller._machine == null)
                return;
            if (controller._hurtcontroller.isdie)
                return;

            controller. _machine.ChangeState("partol");

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
