using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoController : MonoBehaviour
{
    public   PlayerInfo pi;
    public static PlayerInfoController _instance;
    public GameObject AddHealthEffect;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;

        if (SaveData.isHaveData())
        {

            pi = SaveData.data.info;
        }
        else
        {
            pi = new PlayerInfo();
        }

        if (pi.ItemDic.Count == 0)
        {
            
            pi. ItemDic.Add("drug", 0);
         
        }
        if(pi.Teach.Count==0)
        {
            pi.Teach.Add("Equip", false);
        }
        if (pi.SkillDic.Count == 0)
        {

            pi. SkillDic.Add("sitdown", false);
            pi. SkillDic.Add("doublejump", false);
            pi.SkillDic.Add("dash", false);
            pi. SkillDic.Add("walljump", false);
        }
    }
    void Start()
    {
        if (SaveData.isHaveData())
        {

            transform.position = SaveData.data._playerpos.ToVector3();
        }

    }
    public bool isMaxHealth()
    {
        return GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health == GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth;
    }
    public void MinusMP(int amount)
    {
        pi.mp -= amount;
        UIManager._instance.GetView<PlayerInfoView>().SetMpSlider();
    }

    public void AddMP(int amount)
    {
        if (pi.mp >= pi.max_mp)
            return;
        if (pi.mp + amount > pi.max_mp)
        {
            pi.mp = pi.max_mp;
            UIManager._instance.GetView<PlayerInfoView>().SetMpSlider();
            return;
        }
        pi. mp += amount;
        UIManager._instance.GetView<PlayerInfoView>().SetMpSlider();
    }
    public void AddHealth(int amount)
    {
        if (GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health + amount >= GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth)
            GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health = GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth;
        else
            GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health += amount;
        AudioManager._instance.PlayAudio("恢复");
        AddHealthEffect.SetActive(true);
        Timer.Register(1, () => { AddHealthEffect.SetActive(false); });
        pi.health = GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
