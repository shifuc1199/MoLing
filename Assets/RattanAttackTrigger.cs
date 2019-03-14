using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RattanAttackTrigger : MonoBehaviour,IAttackable
{
    public AttackCallBack _attackcallback
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }
    public float _attack;
    public float Attack { get { return _attack; } set { _attack = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
