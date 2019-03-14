using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    public GameObject dash;
    public GameObject singalsword;
    public GameObject multisword;
    
    // Start is called before the first frame update
    void Start()
    {
        dash.SetActive(PlayerInfo.info.SkillDic["dash"]);
        singalsword.SetActive(PlayerInfo.info.SkillDic["singlesword"]);
        multisword.SetActive(PlayerInfo.info.SkillDic["multisword"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
