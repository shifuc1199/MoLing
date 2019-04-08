using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameView : View
{
    public GameObject dash;
    public GameObject singalsword;
    public GameObject multisword;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        dash.SetActive(PlayerInfo.info.SkillDic["dash"]);
        singalsword.SetActive(PlayerInfo.info.ItemDic["sword"]);
        multisword.SetActive(PlayerInfo.info.ItemDic["sword"]);
    }
}
