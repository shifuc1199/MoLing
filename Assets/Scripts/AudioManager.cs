using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using DG.Tweening;
public class AudioManager : MonoBehaviour {
    public static AudioManager _instance;
    private AudioSource Audio;
    public AudioSource BGMSource;
    public AudioClip[] audioclips;
 
    private void Awake()
    {
        _instance = this;

        Audio = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
	}
  
    int FindIndex(string name)
    {
        for (int i = 0; i < audioclips.Length; i++)
        {
            if(audioclips[i].name==name)
            {
                return i;
            }
        }
        return -1;
    }
    
    public void PlayBgm(string name)
    {
        DOTween.To(() => BGMSource.volume, x =>BGMSource.volume = x, 0, 1);
        Timer.Register(1, () =>
        {
            DOTween.To(() => BGMSource.volume, x => BGMSource.volume = x, 1, 1);
            BGMSource.clip = audioclips[FindIndex(name)];
            BGMSource.Play();
        });
      
    }
    public void PlayCV(AudioClip clip)
    {
        Audio.clip = clip;
        Audio.Play();
    }



    public void PlayAudio(string name)
    {
       
            Audio.PlayOneShot(audioclips[FindIndex(name)]);
           
      
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
