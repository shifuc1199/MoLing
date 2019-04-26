using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum TrapType
{
    射箭,
    掉落
}
public class TrapTrigger : MonoBehaviour
{
    public TrapType type;
    public Transform[] Arrow;
    public DOTweenAnimation[] Doors;
    GameObject player;
    bool ispress=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int index = 0;
    public void ResetArrow()
    {
        CancelInvoke();
        foreach (var item in Doors)
        {
            item.DOPlayBackwards();
        }
        Destroy(gameObject);
    }
    public void ProduceArrow()
    {
       
        GameObject temp = GameObjectPool.GetInstance().GetGameObject("Arrow", Arrow[index].position, Quaternion.identity);
        GameObjectPool.GetInstance().ReleaseGameObject("Arrow", temp, 2f);
        temp.transform.right = player.transform.position - temp.transform.position;
        index++;
        index %= Arrow.Length;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ispress)
            return;
        if (collision.gameObject.tag == "Player")
        {
            ispress = true;
            player = collision.gameObject;
            switch (type)
            {
                case TrapType.射箭:
                    foreach (var item in Doors)
                    {
                        item.DOPlay();
                    }
                    Timer.Register(5, () => { ResetArrow(); });
                    InvokeRepeating("ProduceArrow", 1, 0.5f);
                    break;
                case TrapType.掉落:
                    gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
