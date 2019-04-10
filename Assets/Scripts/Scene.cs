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
        private void Awake()
        {
            _instance = this;
            VirtualCamera = VirtualCameras[0];
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
