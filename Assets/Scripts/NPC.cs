using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TalkCallBack();
[System.Serializable]
public class NPC 
{
    public int ID;
    public List<Talk> talks = new List<Talk>();
     
}
[System.Serializable]
public class Talk
{
    public string talker_name;
    public string contenct;
    public AudioClip talk_clip;
    public string _callback_name;
}
