using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dash_Attack_Trigger : MonoBehaviour,IAttackable
{

    public float _attack;

    public float Attack
    {
        get
        {
            return _attack;
        }
        set
        {
            _attack = value;
        }
    }

    public AttackCallBack _attackcallback
    {
        get;


        set;

    }

    // Start is called before the first frame update
    void Start()
    {
        this._attackcallback = (t) => { Debug.Log((GetComponentInParent<Enemy_7_Controller>()._machine.GetState("attack") as Enemy_7_AttackState).id); (GetComponentInParent<Enemy_7_Controller>()._machine.GetState("attack") as Enemy_7_AttackState).AttackStop(); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
