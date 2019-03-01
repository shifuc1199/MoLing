using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour,IAttackable {
    public float _attack;
    public float Attack
    {
        get
        {
            return _attack;
        }
        set
        {
            _attack = value;
        }
    }

    // Use this for initialization
    void Start () {
       
	}
    
    private void OnEnable()
    {
        AudioManager._instance.PlayAudio("挥舞小刀");
        Timer.Register(0.2f, () => { gameObject.SetActive(false); });
    }
    // Update is called once per frame
    void Update () {
		
	}
}
