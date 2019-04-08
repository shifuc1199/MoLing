using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagView : View
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnCloseClick()
    {
        GetComponent<Animator>().SetTrigger("close");
        Timer.Register(0.7f, () => { gameObject.SetActive(false); });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
