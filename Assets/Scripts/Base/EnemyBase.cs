using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float _maxhealth;
    public HurtController _hurtcontroller;
    

    public void Start()
    {
        _hurtcontroller = new HurtController(_maxhealth);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="enemyhurt")
        {
            _hurtcontroller.GetHurt(collision.gameObject.GetComponent<IAttackable>().Attack);
        }
    }

}
