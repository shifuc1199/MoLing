using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour,IAttackable {
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

    public void AttackEnableTrue ()
    {
        AudioManager._instance.PlayAudio("挥舞小刀");
        GetComponent<PolygonCollider2D>().enabled = true;
    }
    public void AttackEnableFalse()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Use this for initialization
    void Start () {
        this._attackcallback = (t) => {if(t.GetComponent<EnemyBase>().hitoffable) t.GetComponent<Rigidbody2D>().AddForce(-t.transform.right * 30, ForceMode2D.Impulse); };
    }
    
    private void OnEnable()
    {
       
        
    }
    // Update is called once per frame
    void Update () {
		
	}
}
