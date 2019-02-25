using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager _instance;
    private AudioSource Audio;
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


 
   

    public void PlayAudio(string name)
    {
       
            Audio.PlayOneShot(audioclips[FindIndex(name)]);
           
      
    }
    
    // Update is called once per frame
    void Update () {
		
	}
}
