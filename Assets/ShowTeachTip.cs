using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowTeachTip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetText(string text)
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = text;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
