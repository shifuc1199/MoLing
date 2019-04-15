using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using game;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
public class PlayerHurtTrigger : MonoBehaviour
{
  
    public HurtController _hurtcontroller;
    Rigidbody2D _rigi;
    PlayerCtr player;
     

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //碰到怪物掉1滴血
        if (collision.gameObject.tag == "enemy")
        {
            if (transform.position.x > collision.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
            _hurtcontroller.GetHurt(1);
        }

        if (collision.gameObject.tag == "playerhurt" || collision.gameObject.tag == "trap")
        {
            if (transform.position.x > collision.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
            _hurtcontroller.GetHurt(collision.gameObject.GetComponent<IAttackable>().Attack); //其他的受伤方式 比如障碍物 以及怪物的攻击
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.tag == "playerhurt" || collision.gameObject.tag == "trap")
        {

            if (transform.position.x > collision.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.identity;
            }
            _hurtcontroller.GetHurt(collision.gameObject.GetComponent<IAttackable>().Attack); //其他的受伤方式 比如障碍物 以及怪物的攻击
        }

    }

    // Use this for initialization
    void Start()
    {
        player = GetComponent<PlayerCtr>();
        _rigi = GetComponent<Rigidbody2D>();
        _hurtcontroller = new HurtController(PlayerInfoController._instance.pi.health, PlayerInfoController._instance.pi.maxhelath);
        _hurtcontroller._DieCallBack = new DieCallBack(
              () =>
              {

                  GetComponent<PlayerCtr>().SitDownEffect.SetActive(false);
                  GetComponent<PlayerCtr>().recovertimer = 0;
                      GetComponent<PlayerCtr>().isSitDown = false;
                
                  player.Inputable = false;
                  GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                  GetComponent<Rigidbody2D>().gravityScale = 0;
                  GetComponentInChildren<Animator>().SetTrigger("die");
                  UIManager._instance.OpenView<DieView>();
                  Timer.Register(2, () => { game.Scene._instance.ResetGame(); }, null, false, true );
              }
              );
                  _hurtcontroller._HurtCallBack = new HurtCallBack(
            () =>
            {
                if (GetComponent<PlayerCtr>().isSitDown)
                {
                    GetComponentInChildren<Animator>().SetTrigger("up");

                    GetComponent<PlayerCtr>().    recovertimer = 0;
                    GetComponent<PlayerCtr>().isSitDown = false;
                    GetComponent<PlayerCtr>().  Inputable = true;
                }
                GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("主角攻击特效", transform.position, Quaternion.identity);

                AudioManager._instance.PlayAudio("受伤");
                GameObjectPool.GetInstance().ReleaseGameObject("主角攻击特效", temp2, 0.5f);
                Timer.Register(0.25f, () => { GetComponentInChildren<SpriteRenderer>().material.DisableKeyword("_EMISSION"); },null,false,true);
                GetComponentInChildren<SpriteRenderer>().material.EnableKeyword("_EMISSION");
                DOTween.Shake(()=>game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => game.Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.1f,2);
                PlayerInfoController._instance.pi.health = _hurtcontroller.Health;
                player.Inputable = false;
                _hurtcontroller.isInvincible = true;
                Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = true;
                Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration =25;
              
                Time.timeScale = 0.5f;
                _rigi.velocity = Vector2.zero;
                _rigi.AddForce((-transform.right + transform.up) * 30, ForceMode2D.Impulse);
               
                Timer.Register(0.25f, () =>
                {
                    Time.timeScale = 1;
                    Camera.main.GetComponent<VignetteAndChromaticAberration>().enabled = false;
                    Camera.main.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0;
                  
                },null,false,true);
                Timer.Register(0.5f, () =>
                {
                    
                    _hurtcontroller.isInvincible = false;
                    if (!_hurtcontroller.isdie)
                     player.Inputable = true;
                }, null, false, true);
                
            });
  }
	
	// Update is called once per frame
	void Update () {
     
        _hurtcontroller.MaxHealth = PlayerInfoController._instance.pi.ItemDic["maxhealth"] + 4;
        PlayerInfoController._instance.pi.maxhelath = _hurtcontroller.MaxHealth;
    }
}
