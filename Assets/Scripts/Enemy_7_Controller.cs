using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_7_Controller : EnemyBase
{
    
    public float _speed;
    public Animator _anim;
    public Transform[] points;
    public Transform _player;
    public float dash_x;
    public int Sprite_ID;
    // Start is called before the first frame update

    
    new void Start()
    {
        base.Start();
        int [] index = new int[] {1,5,7,8 };
        Sprite_ID = index[UnityEngine.Random.Range(0, 4)];
        ChangeCharacter();
        _anim = GetComponent<Animator>();
        _machine.RegisterState(new Enemy_7_AttackState("attack", this));
        _machine.RegisterState(new Enemy_7_PartolState("partol",this));
        _machine.ChangeState("partol");
        _player = game.Scene._instance.player.transform;
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
    public  void ChangeCharacter()
    {
        SpriteRenderer[] Body_Sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (var item in Body_Sprites)
        {
 
            item.sprite = Resources.Load<Sprite>("Sprite/Enemy_Character/Vector Parts "+ Sprite_ID + "/" +item.gameObject.name);
        }
     
    }


   
}
