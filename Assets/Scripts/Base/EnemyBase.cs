using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using game;
using Cinemachine;
public class EnemyBase : MonoBehaviour
{
    public float _maxhealth;
    public HurtController _hurtcontroller;
    public bool hitoffable=false;
  
    public FSMMachines _machine = new FSMMachines();
    public void Start()
    {
        _hurtcontroller = new HurtController(_maxhealth);
    }
    List<Timer> timers = new List<Timer>();
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("受伤!");
        if (collision.gameObject.tag=="enemyhurt" || collision.gameObject.tag == "trap")
        {
          
            if (collision.gameObject.GetComponent<IAttackable>()._attackcallback!=null)
            {
             
                collision.gameObject.GetComponent<IAttackable>()._attackcallback(gameObject);
            }
            if (_hurtcontroller.isdie)
                return;

            Time.timeScale = 0f;
            timers.Add( Timer.Register(0.1f, () => { Time.timeScale = 1;   },null,false,true));
            DOTween.Shake(() => Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset, x => Scene._instance.VirtualCamera.GetComponent<CinemachineCameraOffset>().m_Offset = x, 0.1f, 0.5f);
            timers.Add(Timer.Register(0.25f, () => { GetComponentInChildren<SpriteRenderer>().material.DisableKeyword("_EMISSION"); }));
            GetComponentInChildren<SpriteRenderer>().material.EnableKeyword("_EMISSION");
             
            _hurtcontroller.GetHurt(collision.gameObject.GetComponent<IAttackable>().Attack);
        }
    }
    private void OnDestroy()
    {
        foreach (var item in timers)
        {
            item.Cancel();
        }
    }
    private void Update()
    {
 
        if(_machine.m_curretstate!=null)
        _machine.m_curretstate.OnUpdate();
    }
}
