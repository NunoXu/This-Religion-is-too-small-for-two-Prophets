using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class Spawn : MonoBehaviour
    {
        //public fields to be set in Unity Editor

        //private fields
        public GameObject CurrentAnimalObject;
        public bool hasAnimal;

        public void Start()
        {
            this.hasAnimal = false;
        }

        public void Update()
        {
            
        }
        
        public Animal CurrentAnimal()
        {
                return CurrentAnimalObject.GetComponent<Animal>();
        }
    }
}
