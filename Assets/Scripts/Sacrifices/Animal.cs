using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class Animal : MonoBehaviour
    {
        //public fields to be set in Unity Editor
        public GameManager.GameManager GameManager;
        public GameObject GameAnimal;
        public Player Player;
        public int Type;

        //private fields

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
