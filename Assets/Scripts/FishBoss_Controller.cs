using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
public class FishBoss_Controller : EnemyBase
{
    public List<Timer> timers=new List<Timer>();
    public Transform player;
    [NonSerialized]
    public Animator _anim;
    public float dash_y;
    public float dash_spped;
    public Transform shootpos;
    public Transform[] dashpos;
    public Transform[] shootmovepos;
    public GameObject BossTrigger;
    public Transform diePos;
    public GameObject DashBook;
    public int ShootCount;
  public  Vector3 startpos;
    // Start is called before the first frame update
    private void Awake()
    {
        startpos = transform.localPosition;
           _anim = GetComponent<Animator>();
    }
    new void Start()
    {
        base.Start();
       
         
        _hurtcontroller._DieCallBack = new DieCallBack(() => {
            _anim.SetTrigger("disappear");
            CancelInvoke();
            Timer.Register(1, () => { transform.position = new Vector3(diePos.position.x, diePos.position.y+15); _anim.SetTrigger("die"); GetComponent<BoxCollider2D>().enabled = false;Timer.Register(0.5f, () => { transform.DOMoveY(diePos.position.y, 0.5f).SetEase(Ease.Linear);Timer.Register(0.5f, () =>
            {
                AudioManager._instance.PlayBgm("普通");
                DOTween.Shake(() => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);

            });
            });  });
            Timer.Register(4.5f, () => {
                
                _anim.SetTrigger("diedisappear"); game.Scene._instance.ChangeCamera(0); DashBook.SetActive(true); BossTrigger.SetActive(false); });
            Destroy(gameObject,5);  

        });
        _hurtcontroller._HurtCallBack = new HurtCallBack(() => {
        
            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

        
            GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
            if(_anim.IsAnim("下冲出现"))
            {
                if (NextTimer != null)
                    NextTimer.Cancel();
                ReleaseSkill();
              
            }

        });
       
        
        
    }
    private void OnEnable()
    {
        isReset = false;
        _machine.RegisterState(new FishBoss_ShootState("shoot", this));
        _machine.RegisterState(new FishBoss_DashDownState("dashdown", this));
        _machine.RegisterState(new FishBoss_AttackState("attack", this));
        Timer.Register(2f, () => { _anim.SetTrigger("disappear"); });
        Timer.Register(3f, () => { ReleaseSkill(); });
    
    }
    bool isReset = false;
    public void ResetBoss()
    {
     
        isReset = true;
        _machine.ResetState();
        ShootIndex = 0;
        lastindex = 0;
        AudioManager._instance.PlayBgm("普通");
        _hurtcontroller.Health = _hurtcontroller.MaxHealth;
        CancelInvoke();
        if (NextTimer != null)
         NextTimer.Cancel();
        BossTrigger.GetComponent<Trigger>().ResetTrigger();
        transform.rotation = Quaternion.identity;
        gameObject.transform.localPosition = startpos;
        _anim.SetTrigger("idle");
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
        NextTimer.Cancel();
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
    public void HideCollider()
    {
        AudioManager._instance.PlayAudio("消失");
        GetComponent<BoxCollider2D>().enabled = false;
    
    }
    public void ShowCollider()
    {
        AudioManager._instance.PlayAudio("出现");
        GetComponent<BoxCollider2D>().enabled = true;

    }
    int lastindex;
    Timer NextTimer;
    public void ReleaseSkill()
    {
        int index = UnityEngine.Random.Range(0, 3);
        while(index== lastindex)
        {
            index = UnityEngine.Random.Range(0, 3);
        }
        string[] skillname = new string[] {"shoot","dashdown","attack" };
        switch (skillname[index])
        {
            case "shoot":
                NextTimer= Timer.Register(5
                    , () => { ReleaseSkill(); });
                break;
            case "dashdown":
                NextTimer= Timer.Register(2.5f, () => { ReleaseSkill(); });
                break;
            case "attack":
                NextTimer= Timer.Register(3.5f, () => { ReleaseSkill(); });
                break;
            default:
                break;
        }
        lastindex = index;
         if(!isReset)
        _machine.ChangeState(skillname[index]);
    }
   
    public  new void OnTriggerEnter2D(Collider2D collision)
    {
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
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer ==LayerMask.NameToLayer( "ground")&&_machine.NowStateIs("dashdown")&&!(_machine.GetState("dashdown") as FishBoss_DashDownState).isProduce)
        {
              DOTween.Shake(() =>game. Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);
            Debug.Log("sb!");
            (_machine.GetState("dashdown") as FishBoss_DashDownState).isProduce = true;
            GameObject temp = GameObjectPool.GetInstance().GetGameObject("DashEffect",new Vector3(transform.position.x,dashpos[0].position.y) + new Vector3(0, 3.5f), Quaternion.identity);
          
            GameObjectPool.GetInstance().ReleaseGameObject("DashEffect", temp, 1);

            GameObject temp1 = GameObjectPool.GetInstance().GetGameObject("DashAttack", new Vector3(transform.position.x, dashpos[0].position.y)+new Vector3(0,0.25f), Quaternion.identity);
            temp1.transform.rotation = Quaternion.identity;
            GameObjectPool.GetInstance().ReleaseGameObject("DashAttack", temp1, 1);

            GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("DashAttack", new Vector3(transform.position.x, dashpos[0].position.y) + new Vector3(0,0.25f), Quaternion.identity);
            temp2.transform.rotation = Quaternion.Euler(0,180,0);
            GameObjectPool.GetInstance().ReleaseGameObject("DashAttack", temp2, 1);
        }
    }


    public int GetDashPointIndex()
    {
        float maxDistance = float.MinValue;
        int Index = -1;
        for (int i = 0; i < dashpos.Length; i++)
        {
            if(Vector3.Distance(player.transform.position, dashpos[i].position)>= maxDistance)
            {
                maxDistance = Vector3.Distance(player.transform.position, dashpos[i].position);
                Index = i;
            }
        }
        return Index;
    }
    [NonSerialized]
   public int ShootIndex=0;
    public void Shoot()
    {
        if (ShootIndex >= ShootCount)
            return;

        _anim.SetTrigger("shoot");
        ShootIndex++;
    }
    public void ShootWaterBall()
    {
        AudioManager._instance.PlayAudio("水球");
        DOTween.Shake(() => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.3f, 1f);
        GameObject temp = GameObjectPool.GetInstance().GetGameObject("水球", shootpos.position, shootpos.rotation);
        temp.transform.right = ( player.transform.position- transform.position ).normalized;
        GameObjectPool.GetInstance().ReleaseGameObject("水球",temp, 1);
    }
    
}
