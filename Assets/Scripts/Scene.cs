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
        public GameObject[] SaveDoors;
        public Dictionary<int, bool> DoorDic = new Dictionary<int, bool>();
        public Transform ForestPoint;
        public GameObject Fish;
        public GameObject Boss;
        public GameObject TripleSword;
        private void Awake()
        {
            _instance = this;
          
            VirtualCamera = VirtualCameras[0];

            if (!SaveData.isHaveData())
            {
           
                  
              

                Timer.Register(3, () =>
                {
                    NPC npc = ConfigManager.npc_config.npcs.Find((a) => { return a.ID == 100; });
                    DialogView view = UIManager._instance.OpenView<DialogView>();
                    view.SetContenct(npc._callback_name, npc.talks.ToArray());
                });
                for (int i = 0; i < SaveDoors.Length; i++)
                {
                    DoorDic.Add(SaveDoors[i].GetComponent<SaveDoor>().id, false);
                }
            }
            else
                DoorDic = SaveData.data.Doors;
        }
     
        public void SceneJump(string name)
        {
            UIManager._instance.OpenView<MaskView>();
            Timer.Register(1,() => { UnityEngine.SceneManagement.SceneManager.LoadScene(name); });
          
        }
        public void AutoSaveRunPos()
        {
            SaveData.Save();
        }
        private void OnApplicationQuit()
        {
            SaveData.Save(false);
        }
        public void ResetGameByFinalBoss()
        {
            ChangeCamera(0);
            Boss.GetComponent<Boss_Controller>().ResetBoss();

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
            player.transform.position = SaveData.isHaveData()?SaveData.data._playerpos.ToVector3():player.ResetPoint;
            if(Fish!=null&&Fish.activeSelf)
            ResetGameByBoss();
            if (Boss != null && Boss.activeSelf)
                ResetGameByFinalBoss();
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
            AudioManager._instance.PlayBgm("森林");
            player.Inputable = false;
            UIManager._instance.OpenView<MaskView>();
            Timer.Register(1,() => { UIManager._instance.OpenView<StartView>().SetTitle("神秘之森"); player.Inputable = true; player.transform.position = ForestPoint.position; });
          
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
            if(!SaveData.isHaveData())
            {
                SaveData.Save();
            }
        }
       
        // Update is called once per frame
        void Update()
        {
            TripleSword.SetActive(PlayerInfoController._instance.pi.EquipItemDic.Contains("sword"));
        }
    }
}
