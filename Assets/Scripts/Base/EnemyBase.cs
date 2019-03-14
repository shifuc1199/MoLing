using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float _maxhealth;
    public HurtController _hurtcontroller;
    public bool hitoffable=false;
    public FSMMachines _machine = new FSMMachines();
    public void Start()
    {
        _hurtcontroller = new HurtController(_maxhealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="enemyhurt")
        {
            if(collision.gameObject.GetComponent<IAttackable>()._attackcallback!=null)
            {
                collision.gameObject.GetComponent<IAttackable>()._attackcallback(gameObject);
            }

             

            _hurtcontroller.GetHurt(collision.gameObject.GetComponent<IAttackable>().Attack);
        }
    }
    private void Update()
    {
        if(_machine.m_curretstate!=null)
        _machine.m_curretstate.OnUpdate();
    }
}
