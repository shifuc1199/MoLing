using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtTrigger : MonoBehaviour
{
    public float _maxhealth;
    public HurtController _hurtcontroller;
     Rigidbody2D _rigi;
    PlayerCtr player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到怪物掉1滴血
        if (collision.gameObject.tag == "enemy")
        {
            _hurtcontroller.GetHurt(1);
        }

        if (collision.gameObject.tag == "playerhurt")
        {
            _hurtcontroller.GetHurt(collision.gameObject.GetComponent<IAttackable>().Attack); //其他的受伤方式 比如障碍物 以及怪物的攻击
        }
    }
    
    // Use this for initialization
    void Start ()
    {
        player = GetComponent<PlayerCtr>();
        _rigi = GetComponent<Rigidbody2D>();
        _hurtcontroller = new HurtController(_maxhealth);
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => { Debug.Log("sb");player.Inputable = false;Timer.Register(0.5f, () => { player.Inputable = true; }); _rigi.velocity = Vector2.zero;  _rigi.AddForce(-transform.right * 30, ForceMode2D.Impulse); });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
