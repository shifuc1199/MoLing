using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BagView : View
{
    public Text MoneyText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnCloseClick()
    {
        GetComponent<Animator>().SetTrigger("close");
        Timer.Register(0.7f, () => { gameObject.SetActive(false); });
    }
    // Update is called once per frame
    void Update()
    {
        MoneyText.text = PlayerInfo.info.Money.ToString();


    }
}
