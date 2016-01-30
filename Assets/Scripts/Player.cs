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
        public int PlayerNumber { get; private set; }
        private List<GameObject> PlayerSacrifices;
        private GameObject CurrentSacrifice;

        public void Start()
        {
            //if gameCharacter.pos IS IN first half?
            //  this.PlayerNumber = FIRST_PLAYER;
            //else
            //  this.PlayerNumber = SECOND_PLAYER;
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
