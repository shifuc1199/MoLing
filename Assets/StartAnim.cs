using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class StartAnim : MonoBehaviour
{
    public Text text;
    public TextAsset ta;
    string[] talksplit;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        Split();
        InvokeRepeating("ChangeNext", 1f, 3);
        
    }
    void Split()
    {
        string talks = ta.text;
         talksplit = talks.Split('\n');
    }
    public void ChangeNext()
    {
        text.DOFade(0, 0.25f);
        Timer.Register(0.25f, () => { text.DOFade(1, 0.25f); });
        Timer.Register(0.25f, () => {
            text.text = talksplit[index];
            index++;
            if (index >= talksplit.Length)
            {
                JumpAnim();
            }
        });
        
    }
    public void JumpAnim()
    {
        SceneManager.LoadScene("开场跑酷"); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
