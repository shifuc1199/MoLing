using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2_Attack_Ctr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
   
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);

                GameObject temp3 = GameObjectPool.GetInstance().GetGameObject("小火", transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                GameObjectPool.GetInstance().ReleaseGameObject("小火", temp3, 1f);
 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
