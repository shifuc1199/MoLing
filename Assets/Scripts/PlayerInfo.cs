using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo info;
    [Header("----------------生命值---------------")]
    public  float health;
    public float maxhelath;
    [Header("----------------魔法值---------------")]
    public float max_mp;
    public float mp;
    public Dictionary<string, bool> ItemDic = new Dictionary<string, bool>();
    public  Dictionary<string, bool> SkillDic = new Dictionary<string, bool>();
    public GameObject AddHealthEffect;
    // Start is called before the first frame update
    private void Awake()
    {
        info = this;
        if (ItemDic.Count == 0)
        {
            ItemDic.Add("sword", false);
        }
        if (SkillDic.Count==0)
        {
            SkillDic.Add("doublejump", true);
            SkillDic.Add("dash", false);
            SkillDic.Add("walljump", false);
        }
    }
  public bool isMaxHealth()
    {
        return GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health == GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth;
    }
    public void AddHealth(int amount)
    {
        if (GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health + amount >= GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth)
            GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health = GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth;
        else
        GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health += amount;

        AddHealthEffect.SetActive(true);
        Timer.Register(1, () => { AddHealthEffect.SetActive(false); });
        PlayerInfo.info.health = GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health;
        UIManager._instance.GetView<PlayerInfoView>().SetLifeHead();
    }
    public void MinusMP(int amount)
    {
        mp -= amount;
        UIManager._instance.GetView<PlayerInfoView>().SetMpSlider();
    }
    public void AddMP(int amount)
    {
        if (mp >= max_mp)
            return;
        if(mp+amount>max_mp)
        {
            mp = max_mp;
            UIManager._instance.GetView<PlayerInfoView>().SetMpSlider();
            return;
        }
        mp += amount;
        UIManager._instance.GetView<PlayerInfoView>().SetMpSlider();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
