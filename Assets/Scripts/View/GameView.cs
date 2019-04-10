using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameView : View
{
    public GameObject dash;
    public GameObject singalsword;
    public GameObject multisword;
    public Text MedichineAmountText;
    public float MedichineAmount=3;
    public float MedichineCool_Time;
    public bool Medichine_Cool;
    public Image Medichine_CoolImage;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    float Medichine_Time;
 
    // Update is called once per frame
    void Update()
    {
        if (Medichine_Cool)
        {
            Medichine_CoolImage.fillAmount = MedichineCool_Time - Medichine_Time;
            Medichine_Time += Time.deltaTime;
            
            if (Medichine_Time >= MedichineCool_Time)
            {
                Medichine_Time = 0;
                Medichine_Cool = false;
            }
          
        }

        MedichineAmountText.text = MedichineAmount.ToString();
        dash.SetActive(PlayerInfo.info.SkillDic["dash"]);
        singalsword.SetActive(PlayerInfo.info.ItemDic["sword"]);
        multisword.SetActive(PlayerInfo.info.ItemDic["sword"]);
    }
}
