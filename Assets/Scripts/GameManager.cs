using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        //public fields, seen by Unity in Editor
        public Player PlayerOne;
        public Player PlayerTwo;

        //private fields
        private List<GameObject> Sacrifices;
        
        public void Start()
        {            
            //change to different animals later ; maybe split into two different lists?
            this.Sacrifices = GameObject.FindGameObjectsWithTag("Sacrifice").ToList();
            
        }

        public void Update()
        {
           

                //Get players movements, update the world

                //Wander sacrifices

                //Respawn more if needed

            
        }

        //refer to IAJ-Lab9 GameManager for additional functions

    }
}
