using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
    private void OnEnable()
    {
        Timer.Register(0.5f, () => { gameObject.SetActive(false); });
    }
    // Update is called once per frame
    void Update () {
		
	}
}
