using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnMouseDown()
    {
        if (!UIManager._instance.IsOpening<NumberGameView>())
        {
            UIManager._instance.OpenView<NumberGameView>();
            GameCtr.gamectr.CreateMap();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
