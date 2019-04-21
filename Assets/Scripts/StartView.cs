using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartView : View
{
    public GameObject title;
    public Text titile_text;
    // Start is called before the first frame update
    void Start()
    {
        Timer.Register(5, () => { title.SetActive(false); });
    }
    public void SetTitle(string title)
    {
        this.title.SetActive(true);
        titile_text.text = title;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
