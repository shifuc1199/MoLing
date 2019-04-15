using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class Trigger : MonoBehaviour
{
    public Vector3 pos;
    public GameObject  Boss;
    public GameObject[] Walls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetTrigger()
    {
        foreach (var item in Walls)
        {
            item.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Boss.SetActive(true);
            foreach (var item in Walls)
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
