using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject[] BackGround;
    // Start is called before the first frame update
    void Start()
    {
    

    }
    public void OnValueChange(int index)
    {
        if(index==0)
        {
            foreach (var item in BackGround)
            {
                item.SetActive(false);
            }
        }
        else
        {
            foreach (var item in BackGround)
            {
                item.SetActive(true);
             }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
