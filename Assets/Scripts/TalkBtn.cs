using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  TalkBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    private void OnMouseDown()
    {
        
        GetComponentInParent<IInteractClick>().InteractClick();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
