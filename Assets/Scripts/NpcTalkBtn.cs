using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcTalkBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    private void OnMouseDown()
    {
        
        GetComponentInParent<NPCCtr>().TalkBtnOnClick();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
