using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RunManager : MonoBehaviour
{
    public float GameTimer;
    public GameObject Mask;
    // Start is called before the first frame update
    void Start()
    {
        Timer.Register(GameTimer, ChangeToLoading);
    }
    void ChangeToLoading()
    {
        Mask.SetActive(true);
        Timer.Register(2, () => { SceneManager.LoadScene("LoadingScene"); });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
