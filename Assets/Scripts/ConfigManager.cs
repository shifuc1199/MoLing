using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ConfigManager 
{
   public static NPCConfig npc_config
    {
        get
        {
            if (_npcconfig == null)
            {
                return _npcconfig = Resources.Load<NPCConfig>("Config/NPCConfig");
            }
            else
            {
                return _npcconfig;
            }
        }
        set { _npcconfig = value; }
    }

    private static  NPCConfig _npcconfig;
    private static Effect _effect_config;
    private static ItemConfig _item_config;
    public static ItemConfig item_config
    {
        get
        {
            if (_item_config == null)
            {
                return _item_config = Resources.Load<ItemConfig>("Config/ItemConfig");
            }
            else
            {
                return _item_config;
            }
        }
        set { _item_config = value; }
    }
    public static Effect effect_config
    {
        get
        {
            if (_effect_config == null)
            {
                return _effect_config = Resources.Load<Effect>("Config/EffectConfig");
            }
            else
            {
                return _effect_config;
            }
        }
        set { _effect_config = value; }
    }
}
