using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondAirAttackAnimtionEvent : MonoBehaviour
{
    public Transform left_point;
    public Transform right_point;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InstanceEffect_After()
    {
        GameObject temp1 = GameObjectPool.GetInstance().GetGameObject("DashAttack", left_point.position, Quaternion.identity);
        temp1.transform.rotation = Quaternion.identity;
       temp1.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        temp1.GetComponent<DashAttackCtr>().speed = 50;
    GameObjectPool.GetInstance().ReleaseGameObject("DashAttack", temp1, 1);
        temp1.GetComponent<SpriteRenderer>().color = Color.black;
        GameObject temp2 = GameObjectPool.GetInstance().GetGameObject("DashAttack", right_point.position, Quaternion.identity);
        temp2.transform.rotation = Quaternion.Euler(0, 180, 0);
        GameObjectPool.GetInstance().ReleaseGameObject("DashAttack", temp2, 1);
        temp2.GetComponent<SpriteRenderer>().color = Color.black;
       temp2.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        temp2.GetComponent<DashAttackCtr>().speed = 50;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
