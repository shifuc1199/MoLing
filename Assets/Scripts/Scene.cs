using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Scene : MonoBehaviour
    {
        public static Scene _instance;
        public GameObject[] VirtualCameras;
        public GameObject VirtualCamera;
        public PlayerCtr player;
       
        public Transform ForestPoint;
        public GameObject Fish;
        private void Awake()
        {
            _instance = this;
          
            VirtualCamera = VirtualCameras[0];
             
        }
        public void SavePos(GameObject trans)
        {
            if (trans.transform.GetChild(0).gameObject.activeSelf)
                return;

            SaveData.Save();
          
            trans.transform.GetChild(0).gameObject.SetActive(true);
            trans.transform.GetChild(1).gameObject.SetActive(true);
        }
        public void ResetGameByBoss()
        {
            ChangeCamera(0);
            Fish.GetComponent<FishBoss_Controller>().ResetBoss();
          
        }
        public void ResetGame()
        {
            player.GetComponent<PlayerHurtTrigger>()._hurtcontroller.isdie = false;
            player.GetComponent<PlayerHurtTrigger>()._hurtcontroller.isInvincible = false;
            player.GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health = player.GetComponent<PlayerHurtTrigger>()._hurtcontroller.MaxHealth;
            PlayerInfoController._instance.pi.health = player.GetComponent<PlayerHurtTrigger>()._hurtcontroller.Health;
            player.GetComponentInChildren<Animator>().SetTrigger("reset");
            player.Inputable = true;
            player.transform.position = SaveData.data._playerpos.ToVector3();
            if(Fish!=null&Fish.activeSelf)
            ResetGameByBoss();
        }
        public void ChangeCamera(GameObject gam)
        {
            VirtualCamera = gam;
            for (int i = 0; i < VirtualCameras.Length; i++)
            {
                if (!VirtualCameras[i].Equals( gam))
                VirtualCameras[i].SetActive(false);
                else
                VirtualCameras[i].SetActive(true);
            }
        }
        public void ChangeScene()
        {
            player.Inputable = false;
            UIManager._instance.OpenView<MaskView>();
            Timer.Register(1,() => { player.Inputable = true; player.transform.position = ForestPoint.position; });
          
        }
        public void ChangeCamera(int index)
        {
            VirtualCamera = VirtualCameras[ index];
            for (int i = 0; i < VirtualCameras.Length; i++)
            {
                if (i!= index)
                    VirtualCameras[i].SetActive(false);
                else
                    VirtualCameras[i].SetActive(true);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
