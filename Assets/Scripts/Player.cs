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

        //private fields
        public int PlayerNumber { get; private set; }
        public GameObject CurrentSacrifice;
        public bool hasSacrifice;

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


        public Animal CurrentAnimal()
        {
            return CurrentSacrifice.GetComponent<Animal>();
        }
        
        public void setSacrifice(GameObject animal)
        {
            this.hasSacrifice = true;
            this.CurrentSacrifice = animal;
        }
    
    }
}
