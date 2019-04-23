using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo

{

    public int Money;
    public float health = 4;
    public float maxhelath = 4;
    public float max_mp = 100;
    public float mp = 100;
    public Dictionary<string, bool> Teach = new Dictionary<string, bool>();
    public Dictionary<string, int> ItemDic = new Dictionary<string, int>();
    public Dictionary<string, bool> SkillDic = new Dictionary<string, bool>();
    public List<string> EquipItemDic = new List<string>();
    public List<string> BagItemDic = new List<string>();


}
