using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    int index = 0;
    public void PlayerFootSount()
    {
        index++;
        index %= 2;
        AudioManager._instance.PlayAudio(index==1?"跑步2":"跑步");
    }
   
}
