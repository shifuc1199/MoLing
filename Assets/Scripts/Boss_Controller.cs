using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : EnemyBase
{
    public Transform[] dashpos;
    public float dash_spped;
    public Transform player;
    public Animator _anim;
    public Transform effect;
    // Start is called before the first frame update
   new void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _machine.RegisterState(new Boss_AttackState("attack", this));
        _machine.RegisterState(new Boss_AirAttackState("airattack", this));

       
    }
    
    public void InstanteEffect()
    {
      GameObject temp=  GameObjectPool.GetInstance().GetGameObject("SecondAirAttack",new Vector2(transform.position.x, effect.position.y),Quaternion.identity);
        GameObjectPool.GetInstance().ReleaseGameObject("SecondAirAttack", temp, 2);
    }
    private void Awake()
    {
        Timer.Register(1, () => { _anim.SetTrigger("disappear"); });
        Timer.Register(2, () => { ReleaseSkill(); });
      
    }
    public void HideCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;

    }
    public void ShowCollider()
    {
        GetComponent<BoxCollider2D>().enabled = true;

    }
    int lastindex=-1;
    public void ReleaseSkill()
    {
     
        string[] skillname = new string[] { "attack" ,"airattack"};
        int index = UnityEngine.Random.Range(0, skillname.Length);
        while (index == lastindex)
        {
            index = UnityEngine.Random.Range(0, skillname.Length);
        }
        switch (skillname[index])
        {
            case "shoot":
                Timer.Register(4
                    , () => { ReleaseSkill(); });
                break;
            case "airattack":
                Timer.Register(5f, () => { ReleaseSkill(); });
                break;
            case "attack":
                Timer.Register(4f, () => { ReleaseSkill(); });
                break;
            default:
                break;
        }
        lastindex = index;
        Debug.Log("转状态:" + skillname[index]);
        _machine.ChangeState(skillname[index]);
    }
    private new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                collision.gameObject.transform.rotation = Quaternion.identity;
            }
            collision.gameObject.GetComponent<PlayerHurtTrigger>()._hurtcontroller.GetHurt(1);
        }
    }
    public int GetDashPointIndex()
    {
        float maxDistance = float.MinValue;
        int Index = -1;
        for (int i = 0; i < dashpos.Length; i++)
        {
            if (Vector3.Distance(player.transform.position, dashpos[i].position) >= maxDistance)
            {
                maxDistance = Vector3.Distance(player.transform.position, dashpos[i].position);
                Index = i;
            }
        }
        return Index;
    }
}
