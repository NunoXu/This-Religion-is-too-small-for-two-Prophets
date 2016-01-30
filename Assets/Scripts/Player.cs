using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        //public fields to be set in Unity Editor
        public GameManager.GameManager GameManager;
        public GameObject GameCharacter;
        
        //private fields
        private List<GameObject> PlayerSacrifices;
        private GameObject CurrentSacrifice;

        public void Start()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                //stuff
            }

           
        }

      
    }
}
