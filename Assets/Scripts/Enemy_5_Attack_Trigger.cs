using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5_Attack_Trigger : MonoBehaviour
{
    public Enemy_5_Controller controller;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((controller._machine.GetState("partol") as Enemy_5_PartolState).isSleep)
                return;
            if (controller._machine == null)
                return;
            if (controller._hurtcontroller.isdie)
                return;

            controller._machine.ChangeState("attack");

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if ((controller._machine.GetState("partol") as Enemy_5_PartolState).isSleep)
                return;
            if (controller._machine == null)
                return;
            if (controller._hurtcontroller.isdie)
                return;

            if(controller!=null)
            controller._machine.ChangeState("partol");

        }
    }
}
