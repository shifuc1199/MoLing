using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
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
    [Header("--------角色按键----------")]
    [SerializeField]
    private string _jumpkey;
    [SerializeField]
    private string _dashkey;
    [Header("--------检测位置----------")]
    public Transform CheckGround;
    public Transform CheckWall;
    public Transform OnWallJumpEffectPos;
    [Header("--------玩家状态----------")]
    public bool Inputable = true;
    public bool isDie = false;
    public bool isHurt = false;
    
    [Header("--------复活点----------")]
    public Vector3 ResetPoint;
   
    [Header("--------预制体----------")]

    private GameObject StandWall;
    private Rigidbody2D _rigi;
    private Animator _anim;
   
    private int _jumpindex;
    private float dashtime;
    public float horizontal;
    private float movetimer;
    private float dashtimer;
    private float _gravityscale;

    private bool isGround;
    private bool isOnWall;
    private bool ismoveright = true;
    private bool ismoveleft = true;
    private bool isJump;
    //冲刺键！！！！！！
    private bool isDown;
    private bool isUp;
    //
    private bool isMaxDash;
 
    public bool isStop;
 
    public GameObject attack;
    private GameObject GatherEffect;
    private GameObject GroundEffect;
    private GameObject DashEffectTemp;
    private void Awake()
    {
        //  _healthindex = HealthImage.Length;

        _anim = GetComponentInChildren<Animator>();
        _rigi = GetComponent<Rigidbody2D>();
        ResetPoint = transform.position;
        _gravityscale = _rigi.gravityScale;

    }
  public  void Attack()
    {
        if(!attack.activeSelf)
        attack.SetActive(true);
        
    }
    public void GetHurt()//主角受伤
    {
       
            
                Camera.main.transform.DOShakePosition(0.5f, 1f, 10, 90);

     
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

            if (isGround)
            {
                movetimer += Time.fixedDeltaTime;
                if (movetimer > 0.3f)
                {
                    AudioManager._instance.PlayAudio("跑步");
                    movetimer = 0;
                }
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

            if (isGround)
            {
                movetimer += Time.fixedDeltaTime;
                if (movetimer > 0.3f)
                {
                    AudioManager._instance.PlayAudio("跑步");
                    movetimer = 0;
                }
            }

        }
        if (horizontal == 0)
            movetimer = 0;

        if (horizontal != 0&&!isOnWall)
        {
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

            DashEffectTemp = GameObjectPool.GetInstance().GetGameObject("冲刺特效", transform.position + new Vector3(transform.right.x * 0.5f, 0.5f, 0), transform.rotation);
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

            DashEffectTemp = GameObjectPool.GetInstance().GetGameObject("冲刺特效", transform.position + new Vector3(transform.right.x * 0.5f, 0.5f, 0), transform.rotation);

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
     
    public void OnWallUpdate()
    {
       // _rigi.sharedMaterial.friction = 0;
        if (isJump)
        {
            GameObject temp = GameObjectPool.GetInstance().GetGameObject("蹭墙跳特效", OnWallJumpEffectPos.transform.position, transform.rotation);
            GameObjectPool.GetInstance().ReleaseGameObject("蹭墙跳特效", temp, 0.5f);
            if (horizontal > 0)
                {
                    StartCoroutine(MoveControlRight());
                
                }
                if (horizontal < 0)
                {
                    StartCoroutine(MoveControlleft());
                }
         
        }else
        {
            if (horizontal == 0&&_rigi.velocity.y<0)
                _rigi.velocity = new Vector2(0, _rigi.velocity.y);
        }
      
    }

    void Jump()
    {
       if(isJump)
             {
            if (isOnWall || isGround || (_jumpindex < _maxjumpindex))
            {
               
                if (_jumpindex == 1&&!isOnWall)
                {
                   
                    GameObject  temp=GameObjectPool.GetInstance().GetGameObject("二段跳特效", transform);
                    AudioManager._instance.PlayAudio("二段跳");
                    temp.transform.localPosition = new Vector3(-0.5f, -3, 0);
                    GameObjectPool.GetInstance().ReleaseGameObject("二段跳特效", temp, 1.5f);
                }
                _rigi.gravityScale = _gravityscale;
                AudioManager._instance.PlayAudio("跳跃");
                 _rigi.velocity = Vector2.zero;
                _jumpindex++;
               
                if (isOnWall&&!isGround)
                {   
                    _rigi.velocity = new Vector2(-transform.right.x*10, _jumpspeed);
                }
                else
                {
                    _rigi.velocity = new Vector2(_rigi.velocity.x, _jumpspeed);
                }

            }
            isJump = false;
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
        isOnWall = Physics2D.OverlapCircle(CheckWall.position, 0.1f, LayerMask.GetMask("ground"));
        if (isGround)
        {
          _jumpindex = 0;
        }
    }




    public void Mobile_Jump()
    {
        if (isStop)
            return;
        isJump = true;
    }
    public void Mobils_Dash_Pressed()
    {

    }
    public void Mobile_Dash_Down()
    {
        if (isStop)
            return;
        isDown = true;
    }
    public void Mobile_Dash_Up()
    {
        if (isStop)
            return;
        isUp = true;
        isMaxDash = false;
    }
    private void Update()
    {
      

        GroundAndWallCheck();
        if (isStop)
            return;
        if (Input.GetKeyDown(_jumpkey))
        {
            isJump = true;
        }
        if (Input.GetKeyDown(_dashkey))
        {
            isDown = true;
        }
        if (Input.GetKeyUp(_dashkey))
        {
            isUp = true;
            isMaxDash = false;     
        }
        
       if(ETCInput.GetAxis("Horizontal")>0)
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
    }
    
       
  
    
    void FixedUpdate()
    {
      

        AnimSet();
        if (isStop)
            return;

        if (Inputable)
        { 
            Move();
            Jump(); 
        }

        Dash();

 


    }


}
 
 