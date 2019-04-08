using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Trigger : MonoBehaviour
{
    public Vector3 pos;
    public GameObject  Boss;
    public GameObject[] Walls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            DOTween.To(() => game.Scene._instance.VirtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView, x => game.Scene._instance.VirtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.FieldOfView = x, 70,1);
          //  Timer.Register(1, () => { game.Scene._instance.VirtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().enabled = false; });
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
