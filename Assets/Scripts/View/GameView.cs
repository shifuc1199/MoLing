using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameView : View
{
    public GameObject mechine;
    public GameObject sitdown;
    public GameObject dash;
    public GameObject singalsword;
    public GameObject multisword;
    public Text MedichineAmountText;
   
    public float MedichineCool_Time;
    public bool Medichine_Cool;
    public Image Medichine_CoolImage;
    public Text Run_away_text;
    public float Run_away_Time;
    float Run_away_timer;
    bool Run_away;
    // Start is called before the first frame update
    void Start()
    {
        Run_away_timer = Run_away_Time;
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    float Medichine_Time;
   public void Start_Run_Away()
    {
        Run_away = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Run_away)
        {
            Run_away_timer -= Time.deltaTime;
            Run_away_text.text = (int)Run_away_timer + "秒后坍塌";

        }
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
        mechine.SetActive(PlayerInfoController._instance.pi.ItemDic["drug"] !=0);
        sitdown.SetActive(PlayerInfoController._instance.pi.SkillDic["sitdown"]);
        MedichineAmountText.text = PlayerInfoController._instance.pi.ItemDic["drug"].ToString();
        dash.SetActive(PlayerInfoController._instance.pi.SkillDic["dash"]);
        singalsword.SetActive(PlayerInfoController._instance.pi.EquipItemDic.Contains("sword"));
        multisword.SetActive(PlayerInfoController._instance.pi.EquipItemDic.Contains("sword"));
    }
}
