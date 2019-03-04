using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtTrigger : MonoBehaviour
{
    public float _maxhealth;
    public HurtController _hurtcontroller;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到怪物掉1滴血
        if (collision.gameObject.tag == "enemy")
        {
            _hurtcontroller.GetHurt(1);
        }

        if (collision.gameObject.tag == "playerhurt")
        {
            //其他的受伤方式 比如障碍物 以及怪物的攻击
        }
    }
    
    // Use this for initialization
    void Start ()
    {
        _hurtcontroller = new HurtController(_maxhealth);

        _hurtcontroller._HurtCallBack = new HurtCallBack(() => { GetComponent<Rigidbody2D>().AddForce(-transform.right * 30, ForceMode2D.Impulse); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
