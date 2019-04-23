using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrow : MonoBehaviour
{
    public float speed;


    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            GameObjectPool.GetInstance().ReleaseGameObject(gameObject.name.Split('(')[0], gameObject, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime,Space.World);
    }
}
