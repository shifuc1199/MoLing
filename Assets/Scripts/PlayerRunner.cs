using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerRunner : MonoBehaviour
{
    public RunManager manager;
    public float _up_down_speed;
    public float _speed;
    public int Health = 3;
    public GameObject[] HealthImages;
    bool isdie = false;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="rock")
        {
            Destroy(collision.gameObject);
            Camera.main.transform.parent.DOShakePosition(0.5f, 0.5f);
            Health--;
            HealthImages[Health].SetActive(false);
            if(Health<=0)
            {
                isdie = true;
                manager.Mask.SetActive(true);
                Timer.Register(1.5f, () => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
               
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isdie)
            return;
        
        transform.Translate(new Vector3(0,1,0)*ETCInput.GetAxis("Vertical") * _up_down_speed * Time.deltaTime);
        transform.Translate(transform.right * _speed * Time.deltaTime);
    }
}
