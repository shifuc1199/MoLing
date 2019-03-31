using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGameView : View
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnCloseClick()
    {
        base.OnCloseClick();
        GameCtr.gamectr.ResetGame();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
