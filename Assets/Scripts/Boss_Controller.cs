using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : EnemyBase
{
    public Transform[] dashpos;
    public float dash_spped;
    public Transform player;
    public Animator _anim;
    public GameObject bosstrigger;
    public Transform effect;
    Vector3 startpos;
    Timer nexttimer;
    public bool isToSecond;
    public Transform ToSecondPos;
    int Stage = 1;
    // Start is called before the first frame update
    private void Awake()
    {
        startpos = transform.position;
    }
    new void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _hurtcontroller._DieCallBack += () => { _anim.SetTrigger("die");  bosstrigger.SetActive(false); };

        _hurtcontroller._HurtCallBack += () => {

            if(_hurtcontroller.Health<=10)
            {
                isToSecond = true;
                if (nexttimer != null)
                    nexttimer.Cancel();
                CancelInvoke();
                lastindex = -1;
                isReset = true;

                _anim.SetTrigger("disappear");
                Timer.Register(1, () => {
                    transform.rotation = Quaternion.identity;
                    transform.position = ToSecondPos.position;
                    _anim.SetTrigger("appear");
                    Timer.Register(0.5f, () =>
                    {
                        _anim.SetTrigger("idle");
                        Timer.Register(2, () =>
                        {
                            _anim.SetTrigger("tosecond");
                            Stage = 2;
                            _anim.SetInteger("stage", Stage);
                            Timer.Register(2f, () => { _anim.SetTrigger("disappear");Timer.Register(1, () => { isToSecond = false; isReset = false; ReleaseSkill(); }); });
                        });
                    });
                  
                });
                
               
            }
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
        };
    }
     
    private void OnEnable()
    {
        isReset = false;
        _machine.RegisterState(new Boss_BulletState("bullet", this));
        _machine.RegisterState(new Boss_AttackState("attack", this));
        _machine.RegisterState(new Boss_AirAttackState("airattack", this));
        Timer.Register(2, () => { _anim.SetTrigger("disappear"); });
        Timer.Register(3, () => { ReleaseSkill(); });
    }
    bool isReset;
    public void ResetBoss()
    {
        Stage = 1;
        _anim.SetInteger("stage", Stage);
        if (nexttimer != null)
        nexttimer.Cancel();
        lastindex = -1;
        isReset = true;
        transform.rotation = Quaternion.identity;
        bosstrigger.GetComponent<Trigger>().ResetTrigger();
        AudioManager._instance.PlayBgm("普通");
        _hurtcontroller.Health = _hurtcontroller.MaxHealth;
        CancelInvoke();
        _machine.ResetState();
        _anim.SetTrigger("idle");
        transform.position = startpos;
        gameObject.SetActive(false);
    }
    public void InstanteEffect()
    {
      GameObject temp=  GameObjectPool.GetInstance().GetGameObject("SecondAirAttack",new Vector2(transform.position.x-2.5f, effect.position.y+5),Quaternion.identity);
        GameObjectPool.GetInstance().ReleaseGameObject("SecondAirAttack", temp, 2);
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
        string[] skillname;
        if (Stage==1)
       skillname = new string[] { "attack" ,"airattack"};
        else
            skillname = new string[] { "attack", "airattack" ,"bullet"};
        int index = UnityEngine.Random.Range(0, skillname.Length);
        while (index == lastindex)
        {
            index = UnityEngine.Random.Range(0, skillname.Length);
        }
        switch (skillname[index])
        {
            case "bullet":
                nexttimer= Timer.Register(8
                    , () => { ReleaseSkill(); });
                break;
            case "airattack":
                nexttimer= Timer.Register(5f, () => { ReleaseSkill(); });
                break;
            case "attack":
                nexttimer= Timer.Register(4f, () => { ReleaseSkill(); });
                break;
            default:
                break;
        }
        lastindex = index;
      
        if (!isReset)
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
