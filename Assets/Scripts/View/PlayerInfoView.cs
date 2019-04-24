using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class PlayerInfoView : View
{
    public Text AddMonetText;
    public Text MoneyText; public Text ShopMoneyText;
    public GameObject[] lifehead;
    public Image mpsli;
    // Start is called before the first frame update
    void Start()
    {
      
        SetMpSlider();
    }
    public void SetLifeHead()
    {
       
        for (int i = (int)PlayerInfoController._instance.pi.maxhelath; i <lifehead.Length ; i++)
        {
            lifehead[i].SetActive(false);
        }
        for (int i = 0; i < (int)PlayerInfoController._instance.pi.maxhelath; i++)
        {
 
            lifehead[i].SetActive(true);
        }
        for (int i = (int)PlayerInfoController._instance.pi.health; i < lifehead.Length; i++)
            {
               if (i < 0)
                 return;

                lifehead[i].transform.GetChild(1).gameObject.SetActive(false);
            }

        for (int i = 0; i < (int)PlayerInfoController._instance.pi.health; i++)
        {
            if (i >= lifehead.Length)
                return;

            lifehead[i].transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void SetAddMoney(int addmonet)
    {
        if (AddMonetText == null)
            return;
        AddMonetText.text = "+"+addmonet.ToString();
        if(addmonet>0)
        Timer.Register(0.5f, () => { PlayerInfoController._instance.pi.Money += addmonet; });
        else
            PlayerInfoController._instance.pi.Money += addmonet;
        AddMonetText.gameObject.SetActive(true);
        Timer.Register(1, () => {
            if (AddMonetText == null)
                return; AddMonetText.gameObject.SetActive(false); });
    }
    public void SetMpSlider()
    {
        mpsli.DOFillAmount(PlayerInfoController._instance.pi.mp / PlayerInfoController._instance.pi.max_mp, 0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        SetLifeHead();
        ShopMoneyText.text = PlayerInfoController._instance.pi.Money.ToString();
        MoneyText.text = PlayerInfoController._instance.pi.Money.ToString();
    }
}
