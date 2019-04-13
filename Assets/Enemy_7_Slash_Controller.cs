using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7_Slash_Controller : EnemyBase
{
  
    public Animator _anim;
 
    public int Sprite_ID;
    new void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _machine.RegisterState(new Enemy_7_SlashState("attack", this));
        _machine.RegisterState(new Enemy_7_SlashIdleState("partol", this));
        _machine.ChangeState("partol");

        _hurtcontroller._HurtCallBack += () =>
        {
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
        };
        _hurtcontroller._DieCallBack += () =>
        {
            GetComponent<BoxCollider2D>().enabled = false; GetComponent<Rigidbody2D>().gravityScale = 0;
            _anim.SetTrigger("die");
            Destroy(gameObject, 1.5f);
        };

    }
    [ContextMenu("Change")]
    public void ChangeCharacter()
    {
        SpriteRenderer[] Body_Sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in Body_Sprites)
        {
           
            item.sprite = Resources.Load<Sprite>("Sprite/Enemy_Character/Vector Parts " + Sprite_ID + "/" + item.gameObject.name);
        }

    }
    void SlashAttack()
    {
        if (_hurtcontroller.isdie)
            return;
        _anim.SetTrigger("attack");
    }
}
