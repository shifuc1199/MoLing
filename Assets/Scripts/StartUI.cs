using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class StartUI : MonoBehaviour
{
    private AudioSource audio;
    public GameObject Mask;
    public GameObject continoueButton;
    public GameObject leaves;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        continoueButton.SetActive(SaveData.isHaveData());
    }
    public void ContinoueGame()
    {
        leaves.SetActive(false);
        Mask.gameObject.SetActive(true);
        DOTween.To(() => audio.volume, x => audio.volume = x, 0, 2);
        Timer.Register(3, () =>
        {
            SaveData.Load();
            SceneManager.LoadScene("LoadingScene");
        });
         
    }
    public void NewGame()
    {
        if(SaveData.isHaveData())
        {
            SaveData.NewGame();
        }
        leaves.SetActive(false);
        Mask.gameObject.SetActive(true);
        Timer.Register(3, () =>
        {
            SceneManager.LoadScene("StartAnim");
        });
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
