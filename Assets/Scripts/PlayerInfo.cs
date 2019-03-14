using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo info;
    private float max_mp;
    private float mp;
    public Dictionary<string, bool> ItemDic = new Dictionary<string, bool>();
    public  Dictionary<string, bool> SkillDic = new Dictionary<string, bool>();
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
            SkillDic.Add("doublejump", false);
            SkillDic.Add("dash", false);
            SkillDic.Add("singlesword", false);
            SkillDic.Add("multisword", false);
        }
 
    }
     
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
