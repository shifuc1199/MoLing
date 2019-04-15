using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{
    public GameObject continoueButton;
    // Start is called before the first frame update
    void Start()
    {
        continoueButton.SetActive(SaveData.isHaveData());
    }
    public void ContinoueGame()
    {
        SaveData.Load();
        SceneManager.LoadScene("LoadingScene");
    }
    public void NewGame()
    {
        if(SaveData.isHaveData())
        {
            SaveData.NewGame();
        }
         
        SceneManager.LoadScene("LoadingScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
