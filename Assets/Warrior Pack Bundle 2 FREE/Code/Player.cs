using UnityEngine;
using System.Collections;
using Assets.Scripts.Spells;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {

        public Animator animator;
        public Invoker[] invokers;

        public float rotationSpeed = 30;
        public Vector3 inputVec;
        public Vector3 targetDirection;

        public int PlayerNumber;
        public GameObject CurrentSacrifice;
        public bool hasSacrifice;

        //Warrior types
        public enum Warrior { Mage };

        public Warrior warrior;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
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
