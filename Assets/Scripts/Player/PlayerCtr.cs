using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class PlayerCtr : MonoBehaviour
{
    

    [Header("--------角色数值----------")]
    [SerializeField]
    private float _movespeed;
    [SerializeField]
    private float _jumpspeed;
    [SerializeField]
    private float _dashspeed;
    [SerializeField]
    private float _maxdashtime;
    [SerializeField]
    private float _dashcooltime;
    public int _maxjumpindex;
   
    
    [Header("--------检测位置----------")]
    public Transform CheckGround;
     
    public Transform CheckWall;
    public Transform OnWallJumpEffectPos;
    [Header("--------玩家状态----------")]
    public bool Inputable = true;
 
    [Header("--------复活点----------")]
    public Vector3 ResetPoint;
   
    [Header("--------预制体----------")]

    private GameObject StandWall;
    private Rigidbody2D _rigi;
    private Animator _anim;
    [Header("--------人物固定物体----------")]
    public GameObject Sword;
    public GameObject SitDownEffect;
    public GameObject AddHealthEffect;
    private int _jumpindex;
    private float dashtime;
    private float horizontal;
  
    private float dashtimer;
    private float _gravityscale;

    private bool isGround;
    private bool isOnWall;
    private bool ismoveright = true;
    private bool ismoveleft = true;
  
    //冲刺键！！！！！！
    private bool isDown;
    private bool isUp;
    //
    private bool isMaxDash;
    bool isSitDown;
    
    public GameObject attack;
    [Header("--------时间间隔 ----------")]
    public float attackTimer;//攻击间隔时间
    public float RecoverHealthTime;//恢复生命时间间隔
    private bool isCanAttack=true;
    private GameObject GatherEffect;
    private GameObject GroundEffect;
    private GameObject DashEffectTemp;
    private void Awake()
    {
        //  _healthindex = HealthImage.Length;
        
        _anim = GetComponentInChildren<Animator>();
        _rigi = GetComponentInChildren<Rigidbody2D>();
        ResetPoint = transform.position;
        _gravityscale = _rigi.gravityScale;
        
    }
     
  public  void Attack()
    {
        if (Inputable&& isCanAttack)
        {
            attack.SetActive(true);
            isCanAttack = false;
           
        }
        
    }
  
    IEnumerator Rotate(Quaternion euler)
    {
        yield return new WaitForSeconds(0.1f);
        transform.localRotation = euler;
    }
    void Move()
    {
        if (horizontal > 0)
        {
            if (!ismoveright)
            {
                return;
            }
            if (transform.eulerAngles.y == 180)
            {
                _anim.SetTrigger("Turn");
                StartCoroutine(Rotate(Quaternion.identity));
            }

             
        }
        if (horizontal < 0)
        {
            if (!ismoveleft)
            {
                return;
            }

            if (transform.eulerAngles.y == 0)
            {
                _anim.SetTrigger("Turn");
                StartCoroutine(Rotate(Quaternion.Euler(0, 180, 0)));

            }

           

        }
        

        if (horizontal != 0&&!isOnWall)
        {
            if(!isForwardWall)
            _rigi.velocity = new Vector2(horizontal * _movespeed, _rigi.velocity.y);

        }


    }





    void Dash()
    {
       

        if (dashtimer < _dashcooltime)
        {
            isDown = false;
            isUp = false;
            dashtimer += Time.deltaTime;
            return;
        }

        if (isOnWall)
        {
            isUp = false;
            isDown = false;
            return;
        }


        if (isMaxDash)
            return;


        if (isDown)
        {
            Inputable = false;
            _rigi.velocity = Vector2.zero;
            _rigi.gravityScale = 0f;
            dashtime += Time.deltaTime;
            if (dashtime >= _maxdashtime)
            {
                Debug.Log("超级冲刺!");
                _anim.SetTrigger("isDash");
                dashtime = 0;
                dashtimer = 0;
                isMaxDash = true;
                isDown = false;
                _rigi.AddForce(transform.right * _dashspeed * _maxdashtime * 0.4f, ForceMode2D.Impulse);
             

            }
        }
        else

            return;

        if (isUp)
        {
            Debug.Log("普通冲刺!");
            if (dashtime <= 0.2f)
                {
                    _rigi.AddForce(transform.right * _dashspeed * 0.2f, ForceMode2D.Impulse);
                }
                else
                {
                    if (dashtime <= _maxdashtime / 2)
                        dashtime = _maxdashtime / 2;
                    _rigi.AddForce(transform.right * _dashspeed * dashtime * 0.4f, ForceMode2D.Impulse);
                }
                isDown = false;
                _anim.SetTrigger("isDash");
                dashtimer = 0;
                dashtime = 0;
                isUp = false;
                return;
          
        }

       

         

      


    }








   
    public void OnDashEnter()
    {
        AudioManager._instance.PlayAudio("冲刺");
        Inputable = false;
        horizontal = 0;
        _rigi.gravityScale = 0;
        if (DashEffectTemp == null || !DashEffectTemp.activeSelf)
        {

            DashEffectTemp = GameObjectPool.GetInstance().GetGameObject("冲刺特效", transform.position + new Vector3(transform.right.x * 0.5f, -1.25f, 0), Quaternion.Euler(180-transform.eulerAngles.y, 90, 90));
            GameObjectPool.GetInstance().ReleaseGameObject("冲刺特效", DashEffectTemp, 0.5f);


        }
    }

    public void OnDashReadyEnter()
    {
        Inputable = false;
        _rigi.gravityScale = 0;
        if (GatherEffect == null||!GatherEffect.activeSelf)
        GatherEffect = GameObjectPool.GetInstance().GetGameObject("蓄力特效", transform.position + new Vector3(transform.right.x * 0.5f, 0.5f, 0), transform.rotation);
       
    }

    public void OnDashReadyExit()
    {
         
        if (DashEffectTemp == null)
        {

            DashEffectTemp = GameObjectPool.GetInstance().GetGameObject("冲刺特效", transform.position + new Vector3(transform.right.x * 0.5f, -1.25f, 0), Quaternion.Euler(180 - transform.eulerAngles.y, 90,90));

             GameObjectPool.GetInstance().ReleaseGameObject("冲刺特效",DashEffectTemp, 0.25f);
        }
        if (GatherEffect != null)
            GameObjectPool.GetInstance().ReleaseGameObject("蓄力特效",GatherEffect, 0);
    }
    public void OnFallExit()
    {
        if (isGround)
        {
            GroundEffect = GameObjectPool.GetInstance().GetGameObject("落地特效", transform.position+new Vector3(0,-0.5f,0), transform.rotation);

            GameObjectPool.GetInstance().ReleaseGameObject("落地特效", GroundEffect, 1f);
            AudioManager._instance.PlayAudio("落地");
        }
    }
   
    public void OnDashExit()
    {
        Inputable = true;
        _rigi.velocity = Vector2.zero;
       
        if(!isOnWall)
        _rigi.gravityScale = _gravityscale;
    }
    public void OnIdleEnter()
    {
        _rigi.gravityScale = _gravityscale;
        _rigi.velocity = Vector2.zero;


    }
    public void OnWallEnter()
    {
 
        _rigi.gravityScale = 1f;
     //   _jump_key_press_timer = _maxjumptimer;
        _rigi.velocity = Vector2.zero;
       
    }

    public void OnWallExit()
    {
       
        _rigi.gravityScale = _gravityscale;
        _jumpindex = 1;
        //_rigi.sharedMaterial.friction = 1;
    }
    IEnumerator MoveControlRight()
    {
      
        ismoveright = false;
        yield return new WaitForSeconds(0.25f);
        ismoveright = true;
    }
    IEnumerator MoveControlleft()
    {
        ismoveleft = false;
        yield return new WaitForSeconds(0.25f);
        ismoveleft = true;
    }
  public  bool isForwardWall = false;
    public void OnWallUpdate()
    {
        
        // _rigi.sharedMaterial.friction = 0;
      
            if (horizontal == 0&&_rigi.velocity.y<0)
                _rigi.velocity = new Vector2(0, _rigi.velocity.y);
        
      
    }
    // key down 计时  按下的时间 如果超过最大时间 直接按照最大高度起跳
    // key up 判断 是否超过最大时间 如果超过的话  什么都不做  否则 按照按下的时间 进行跳跃
    void Jump()
    {
        if (isjumpstate)
        { 
            if (_jumpindex == 1 && !isOnWall)
            {
                if (!PlayerInfo.info.SkillDic["doublejump"])
                {
                    return;
                }
                GameObject temp = GameObjectPool.GetInstance().GetGameObject("二段跳特效", transform);
                AudioManager._instance.PlayAudio("二段跳");
                temp.transform.localPosition = new Vector3(-0.5f, -3, 0);
                GameObjectPool.GetInstance().ReleaseGameObject("二段跳特效", temp, 1.5f);
            }
        _rigi.gravityScale = _gravityscale;
        AudioManager._instance.PlayAudio("跳跃");
       
        _jumpindex++;

        if (isOnWall && !isGround)
        {
            
            if (horizontal > 0)
            {
                StartCoroutine(MoveControlRight());

            }
            if (horizontal < 0)
            {
                StartCoroutine(MoveControlleft());
            }
            _rigi.velocity = new Vector2(-transform.right.x * 5, _rigi.velocity.y);
        }
            isjumpstate = false;
    }
 
           
            
       
    }



    public void AnimSet()
    {
        _anim.SetBool("isOnWall", isOnWall);
        _anim.SetBool("isGround", isGround);
        _anim.SetFloat("YSpeed", _rigi.velocity.y);
        _anim.SetFloat("XSpeed",Mathf.Abs(horizontal));
        _anim.SetFloat("DashTime", dashtime);

      

    }
    
    public void GroundAndWallCheck()
    {
        isGround = Physics2D.OverlapCircle(CheckGround.position, 0.1f, LayerMask.GetMask("ground"));
        isForwardWall =
             Physics2D.Linecast(transform.position + new Vector3(0, 0.5f, 0), transform.right + transform.position, LayerMask.GetMask("ground"))||
            Physics2D.Linecast(transform.position, transform.right + transform.position, LayerMask.GetMask("ground")) ||
            Physics2D.Linecast(transform.position + new Vector3(0, -1f, 0), transform.right + transform.position + new Vector3(0, -1, 0), LayerMask.GetMask("ground")) ||
           Physics2D.Linecast(transform.position + new Vector3(0, -2f, 0), transform.right + transform.position + new Vector3(0, -2f, 0), LayerMask.GetMask("ground"));
       

        isOnWall = Physics2D.OverlapCircle(CheckWall.position, 0.1f, LayerMask.GetMask("ground")) && PlayerInfo.info. SkillDic["walljump"];
        if (isGround)
        { 
            _jumpindex = 0;
        }
    }

    float _jump_key_press_timer = 0;
    public float _maxjumptimer;
    bool ispressjump = false;
    bool isjumpstate = false;
    public void Mobile_Jump_Up()
    {
         
        if (!Inputable)
            return;
      


        _jump_key_press_timer = 0;

        ispressjump = false;
    }

    public void Mobile_Jump_Stay()
    {
       
        if (!Inputable)
            return;
        if (!ispressjump)
            return;
      
        if (_jump_key_press_timer<_maxjumptimer)
        {
           
            _jump_key_press_timer += Time.deltaTime;
            if(!isOnWall)
            _rigi.velocity += new Vector2(0, Multi_speed * Time.deltaTime );

        }
        else
        {
 
            ispressjump = false;


        }
      

    }
    public float Multi_speed;
    public void Mobile_Jump_Down()
    {
       
        if (!Inputable)
            return;

        if (isOnWall || isGround || (_jumpindex < _maxjumpindex))
        {
           
            if (_jumpindex == 1 && !isOnWall)
            {
                 if(!PlayerInfo.info.SkillDic["doublejump"])
                {
                    return;
                }
            }

            GameObject temp = GameObjectPool.GetInstance().GetGameObject("跳跃灰尘", transform.position-new Vector3(0,1f,0),Quaternion.identity);
      
            GameObjectPool.GetInstance().ReleaseGameObject("跳跃灰尘", temp, 1.5f);

            _rigi.velocity = Vector2.zero;
            ispressjump = true;
            isjumpstate = true;
            _rigi.velocity += new Vector2(0,_jumpspeed);
          
        }
    }
     public void OnStandExit()
    {
        recovertimer = 0;
        isSitDown = false;
        Inputable = true;
    }
    public void OnStandEnter()
    {
        isSitDown = true;
        
        Inputable = false;
    }
    public void Mobile_Sit_Down()
    {
        if (GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health == GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth)
        {
            return;
        }
        if (Inputable)
        {
            Timer.Register(0.5f, () => { SitDownEffect.SetActive(true);   });
            _anim.SetTrigger("down");
        }
        else
        {
             SitDownEffect.SetActive(false); 
            _anim.SetTrigger("up");
        }
    }
    public void Mobile_Sword_Mutli_Shoot()
    {
       

        if ( !isOnWall && Sword.GetComponent<SwordCtr>().isCanMutli)
        {
            if (PlayerInfo.info.mp >= 20)
            {
              PlayerInfo.info.  MinusMP(20);
            }
            else
                return;
            Sword.GetComponent<SwordCtr>().Skill(AttackType.群剑发射);
            if (!isGround)
            {
                Inputable = false;
                _rigi.velocity = Vector2.zero;
                _rigi.gravityScale = 0;
            }
            Timer.Register(0.2f, () =>
            {

              
                Timer.Register(0.25f, () =>
                {
                    Inputable = true;
                    _rigi.gravityScale = _gravityscale;
                });
            });

        }
    }
    public void  Mobile_Sword_Shoot()
    {
       

        if (!isOnWall &&Sword.GetComponent<SwordCtr>().isCanSingal)
        {
            if (PlayerInfo.info.mp >= 20)
            {
                PlayerInfo.info.MinusMP(10);
            }
            else
                return;

            Sword.GetComponent<SwordCtr>().Skill(AttackType.单剑发射);


            if (!isGround)
            {
                Inputable = false;
                _rigi.velocity = Vector2.zero;
                _rigi.gravityScale = 0;
            }
            Timer.Register(0.2f, () =>
            {
              
                GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("飞剑枪火特效", transform.position+new Vector3(transform.right.x*4,-0.7f,0), Quaternion.Euler(0,-90,90));
                GameObjectPool.GetInstance().ReleaseGameObject("飞剑枪火特效", temp2, 1.5f);
                Timer.Register(0.25f, () =>
                {
                    Inputable = true;
                    _rigi.gravityScale = _gravityscale;
                });
            });
         
        }
    }

     

    public void Mobile_Dash_Down()
    {
        if (!Inputable)
            return;

        isDown = true;
    }
    public void Mobile_Dash_Up()
    {
 

        isUp = true;
        isMaxDash = false;
    }
    float timer;
    float recovertimer;

    public void SitDownAddHealth(int amount)
    {
        PlayerInfo.info.AddHealth(amount);
        AddHealthEffect.SetActive(true);
        Timer.Register(1, () => { AddHealthEffect.SetActive(false); });
        if (GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health == GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth)
        {
            SitDownEffect.SetActive(false);
            _anim.SetTrigger("up");
        }
    }

    private void Update()
    {
        if(isSitDown)
        {
             recovertimer += Time.deltaTime;
            if(recovertimer>=RecoverHealthTime)
            {
                recovertimer = 0;
                SitDownAddHealth(1);
            }
        }

        if(!isCanAttack)
        {
            timer += Time.deltaTime;
            if(timer>=attackTimer)
            {
                isCanAttack = true;
                timer = 0;
            }
        }
        if (_rigi.velocity.y<0&&!isOnWall)
        {

            _rigi.velocity += Vector2.up * Physics2D.gravity.y * 1f * Time.deltaTime;
            
        }
        else if (_rigi.velocity.y > 0 && !isjumpstate)
        {
            _rigi.velocity += Vector2.up * Physics2D.gravity.y * (  1) * Time.deltaTime;
        }

        GroundAndWallCheck();
        

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Mobile_Jump_Down();
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            Mobile_Jump_Up();
        }
        if (Input.GetKey(KeyCode.K))
        {
            Mobile_Jump_Stay();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            isDown = true;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            isUp = true;
            isMaxDash = false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
       
#if UNITY_ANDROID
        if (ETCInput.GetAxis("Horizontal")>0)
        {
             
                horizontal = 1;
        }
        else if (ETCInput.GetAxis("Horizontal") < 0)
        {
           
                horizontal = -1;
        }
       else if (ETCInput.GetAxis("Horizontal") ==0)
        {
             
                horizontal = 0;
        }
#endif

#if UNITY_EDITOR
       
        horizontal = Input.GetAxisRaw("Horizontal");
#endif

    }




    void FixedUpdate()
    {
         
        Sword.SetActive(PlayerInfo.info.ItemDic["sword"]);
        AnimSet();
      

        if (Inputable)
        { 
            Move();
            Jump();
        }

        Dash();

 


    }


}
 
 