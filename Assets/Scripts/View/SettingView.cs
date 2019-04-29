using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingView : View
{
    public Behaviour[] behaviours;

    public GameObject Effect;
    public GameObject background;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void On_CameraPost_ValueChange(int index)
    {
        if (index == 0)
        {
            foreach (var item in behaviours)
            {
                item.enabled = true;
            }
        }
        else
        {
            foreach (var item in behaviours)
            {
                item.enabled = false;
            }
        }
    }
    public void BackToMenu()
    {
        SaveData.Save(false);
        SceneManager.LoadScene("StartScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void On_Effect_ValueChange(int index)
    {
        if (index == 0)
        {
            Effect.SetActive(true);
        }
        else
        {
            Effect.SetActive(false);
        }
    }
    public void On_BacnGround_ValueChange(int index)
    {
        if (index == 0)
        {
            background.SetActive(true);
        }
        else
        {
            background.SetActive(false);
        }
    }
}
