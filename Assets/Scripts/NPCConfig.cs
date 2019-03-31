using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NPCConfig")]
public class NPCConfig : ScriptableObject
{
   
    public List<NPC> npcs = new List<NPC>();
}
