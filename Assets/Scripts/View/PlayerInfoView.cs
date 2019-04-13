using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class PlayerInfoView : View
{
    public Text AddMonetText;
    public Text MoneyText;
    public GameObject[] lifehead;
    public Image mpsli;
    // Start is called before the first frame update
    void Start()
    {
        SetLifeHead();
        SetMpSlider();
    }
    public void SetLifeHead()
    {
        for (int i = (int)PlayerInfo.info.maxhelath; i <lifehead.Length ; i++)
        {
            lifehead[i].SetActive(false);
        }
        for (int i = 0; i < (int)PlayerInfo.info.maxhelath; i++)
        {
 
            lifehead[i].SetActive(true);
        }
        for (int i = (int)PlayerInfo.info.health; i < lifehead.Length; i++)
            {
               if (i < 0)
                 return;

                lifehead[i].transform.GetChild(1).gameObject.SetActive(false);
            }

        for (int i = 0; i < (int)PlayerInfo.info.health; i++)
        {
            if (i >= lifehead.Length)
                return;

            lifehead[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void SetAddMoney(int addmonet)
    {
        AddMonetText.text = "+"+addmonet.ToString();
        if(addmonet>0)
        Timer.Register(0.5f, () => { PlayerInfo.info.Money += addmonet; });
        else
            PlayerInfo.info.Money += addmonet;
        AddMonetText.gameObject.SetActive(true);
        Timer.Register(1, () => { AddMonetText.gameObject.SetActive(false); });
    }
    public void SetMpSlider()
    {
        mpsli.DOFillAmount(PlayerInfo.info.mp / PlayerInfo.info.max_mp, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = PlayerInfo.info.Money.ToString();
    }
}
