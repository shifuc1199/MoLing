using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class Scene : MonoBehaviour
    {
        public static Scene _instance;
        public GameObject VirtualCamera;
        public PlayerCtr player;
        private void Awake()
        {
            _instance = this;
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
