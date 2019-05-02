using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
    bool isWuDi = false;
    public Transform diePos;
    public int ButterFlyAmount;
    public Transform WindAttackPos;
    // Start is called before the first frame update
    private void Awake()
    {
        startpos = transform.position;
    }
    public void AirAttackEffect()
    {
        GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("风刃", WindAttackPos.position, Quaternion.identity);
        Vector3 pos = player.transform.position - transform.position;
        pos.z = 0;
        temp2.transform.right = pos;
        GameObjectPool.GetInstance().ReleaseGameObject("风刃", temp2, 2f);
    }
    new void Start()
    {
        base.Start();
        _anim = GetComponent<Animator>();
        _hurtcontroller._DieCallBack += () => {
         
            if (nexttimer != null)
                nexttimer.Cancel();
            _anim.SetTrigger("firstdisappear");
            CancelInvoke();
            Timer.Register(1, () => 
            {
                transform.position = new Vector3(diePos.position.x, diePos.position.y ); _anim.SetTrigger("die"); GetComponent<BoxCollider2D>().enabled = false; Timer.Register(0.5f, () => {
                    transform.DOMoveY(diePos.position.y, 0.5f).SetEase(Ease.Linear); Timer.Register(0.15f, () =>
                    {
                        DOTween.Shake(() => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);
                        AudioManager._instance.PlayBgm("普通");
                       
                    });
                });
            });
            Timer.Register(1.5f, () => {

                NPC npc = ConfigManager.npc_config.npcs.Find((a) => { return a.ID == 103; });

                DialogView view = UIManager._instance.OpenView<DialogView>();
                view.SetContenct(npc._callback_name, npc.talks.ToArray());


            });
          


         
          
        };

        _hurtcontroller._HurtCallBack += () => 
        {

            if (_hurtcontroller.Health > 0&&!isToSecond)
            {
                if (_hurtcontroller.Health <= 10 && Stage == 1)
                {
                    isToSecond = true;
                    if (nexttimer != null)
                        nexttimer.Cancel();
                    CancelInvoke();
                    lastindex = -1;
                    isReset = true;

                    _anim.SetTrigger("disappear");
                    Timer.Register(1, () =>
                    {
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
                                Timer.Register(2f, () =>
                                {
                                    _anim.SetTrigger("disappear");
                                    Timer.Register(1, () => { isToSecond = false; isReset = false; ReleaseSkill(); });
                                });
                            });
                        });

                    });


                }

                else if (_anim.IsAnim("Attack2"))
                {
                    _anim.SetTrigger("firstdisappear");


                    if (nexttimer != null)
                        nexttimer.Cancel();


                    (_machine.GetState("attack") as Boss_AttackState).isattack = false;
                    Timer.Register(1f, () => { ReleaseSkill(); });


                }
            }
           
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
        };
    }
     public void Died()
    {
        _anim.SetTrigger("diedisappear");
        game.Scene._instance.ChangeCamera(0);
        bosstrigger.SetActive(false);
        Destroy(gameObject, 1);
    }
  public  bool isTalk = false;
    public void StartPk()
    {
        isReset = false;
        _machine.RegisterState(new Boss_BulletState("bullet", this));
        _machine.RegisterState(new Boss_AttackState("attack", this));
        _machine.RegisterState(new Boss_AirAttackState("airattack", this));
        _anim.SetTrigger("disappear");
        Timer.Register(1, () => { ReleaseSkill(); });
    }
    private void OnEnable()
    {


        if (isTalk)
        {
            isReset = false;
            _machine.RegisterState(new Boss_BulletState("bullet", this));
            _machine.RegisterState(new Boss_AttackState("attack", this));
            _machine.RegisterState(new Boss_AirAttackState("airattack", this));
            Timer.Register(2, () => { _anim.SetTrigger("disappear"); });
            Timer.Register(3, () => { ReleaseSkill(); });
        }
        else
        {
            NPC npc = ConfigManager.npc_config.npcs.Find((a) => { return a.ID == 102; });
      
            DialogView view = UIManager._instance.OpenView<DialogView>();
            view.SetContenct(npc._callback_name, npc.talks.ToArray());
        }
        
     
    }
   public bool isReset;
    public void ResetBoss()
    {
        Debug.Log("Reset!!!");
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
        if (Stage == 2)
        {
            GameObject temp = GameObjectPool.GetInstance().GetGameObject("SecondAirAttack", new Vector2(transform.position.x - 2.5f, effect.position.y + 5), Quaternion.identity);
            GameObjectPool.GetInstance().ReleaseGameObject("SecondAirAttack", temp, 1);
        }
    }
    int shootindex = 0;
    public void ShootButterFly()
    {
        if (shootindex == 0)
        {
            for (int i = 0; i < ButterFlyAmount; i++)
            {
                GameObject temp = GameObjectPool.GetInstance().GetGameObject("蝴蝶", transform.position + new Vector3(1.5f, 0), Quaternion.Euler(new Vector3(0, 0, i * 15)));
                GameObjectPool.GetInstance().ReleaseGameObject("蝴蝶", temp, 5);

            }
            shootindex=1;
        }
        else
        {
            shootindex = 0;
            for (int i = 0; i < ButterFlyAmount; i++)
            {
                GameObject temp = GameObjectPool.GetInstance().GetGameObject("蝴蝶", transform.position + new Vector3(1.5f, 0), Quaternion.Euler(new Vector3(0, 0, i * 15 -7.5f)));
                GameObjectPool.GetInstance().ReleaseGameObject("蝴蝶", temp, 5);

            }
        }

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
                nexttimer= Timer.Register(10
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
        {
            _machine.ChangeState(skillname[index]);
 
        }
        
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
