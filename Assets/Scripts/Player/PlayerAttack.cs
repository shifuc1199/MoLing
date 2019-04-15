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
    
        AudioManager._instance.PlayAudio("挥舞小刀1");
        
    }
    public void AttackEnableFalse()
    {
        gameObject.SetActive(false); 
    }
  
    // Use this for initialization
    void Start () {
       
        this._attackcallback = (t) =>
        {

            if (t.GetComponent<EnemyBase>().hitoffable)
            {
                GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, GetComponentInParent<Rigidbody2D>().velocity.y);

                t.GetComponent<Rigidbody2D>().velocity = Vector2.zero; t.GetComponent<Rigidbody2D>().AddForce(-t.transform.right * 30, ForceMode2D.Impulse);
            }
           PlayerInfoController._instance.AddMP(5);
            int a = Random.Range(1, 3);
            AudioManager._instance.PlayAudio("击中"+a);
            game.Scene._instance.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            game.Scene._instance.player.GetComponent<Rigidbody2D>().AddForce(-game.Scene._instance.player.transform.right * 5, ForceMode2D.Impulse);
        };
    }
    
    private void OnEnable()
    {
       
        
    }
    // Update is called once per frame
    void Update () {
       
    }
}
