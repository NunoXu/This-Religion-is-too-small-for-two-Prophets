using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        //public fields, seen by Unity in Editor
        public GameObject PlayerOneObject;
        public GameObject PlayerTwoObject;

        public Player PlayerOne;
        public Player PlayerTwo;

        public GameObject SpawnOne;
        public GameObject SpawnTwo;

        public GameObject horseModel;
        public GameObject kittenModel;
        public GameObject goatModel;
        public GameObject roosterModel;
        public GameObject sheepModel;
        public GameObject unicornModel;

        //private fields
        private List<GameObject> Sacrifices;
        private int MaxNumberOfObjectsPerPlayer = 6;

        public void Start()
        {
            
            for (int i = 0; i < MaxNumberOfObjectsPerPlayer; i++)
            {
                float angle = i * Mathf.PI * 2 / MaxNumberOfObjectsPerPlayer;
                Vector3 pos = SpawnOne.transform.position;
                 pos.x +=  Properties.ANIMAL_SPAWN_RADIUS * Mathf.Cos(angle);
                 pos.z -= Properties.ANIMAL_SPAWN_RADIUS * Mathf.Sin(angle);
                GenerateAnimal(pos);
            }
            for (int i = 0; i < MaxNumberOfObjectsPerPlayer; i++)
            {
                float angle = i * Mathf.PI * 2 / MaxNumberOfObjectsPerPlayer;
                Vector3 pos = SpawnTwo.transform.position;
                pos.x += Properties.ANIMAL_SPAWN_RADIUS * Mathf.Cos(angle);
                pos.z += Properties.ANIMAL_SPAWN_RADIUS * Mathf.Sin(angle);
                GenerateAnimal(pos);
            }

            this.PlayerOne = PlayerOneObject.GetComponent<Player>();
            this.PlayerTwo = PlayerTwoObject.GetComponent<Player>();

            //change to different animals later ; maybe split into two different lists?
            // this.Sacrifices = GameObject.FindGameObjectsWithTag("Sacrifice").ToList();

        }

        public void Update()
        {


            //Get players movements, update the world

            //Wander sacrifices

            //Respawn more if needed


        }

        //refer to IAJ-Lab9 GameManager for additional functions

        private bool CheckRange(GameObject a, GameObject b, float maximumSqrDistance)
        {
            return (a.transform.position - b.transform.position).sqrMagnitude <= maximumSqrDistance;
        }

        private Object GenerateAnimal(Vector3 pos)
        {
            int type = Random.Range(0, Properties.ANIMAL_TYPES);
            return GenerateAnimal(type, pos);
        }

        private Object GenerateAnimal(int type, Vector3 pos)
        {
            if(type == Properties.HORSE)
            {
                return Instantiate(horseModel, pos, Quaternion.identity);
            }
            else if(type == Properties.CAT)
            {
                return Instantiate(kittenModel, pos, Quaternion.identity);
            }
            else if (type == Properties.GOAT)
            {
                return Instantiate(goatModel, pos, Quaternion.identity);
            }
            else if (type == Properties.UNICORN)
            {
                return Instantiate(unicornModel, pos, Quaternion.identity);
            }
            else if (type == Properties.CHICKEN)
            {
                return Instantiate(roosterModel, pos, Quaternion.identity);
            }
            else if (type == Properties.SHEEP)
            {
                return Instantiate(sheepModel, pos, Quaternion.identity);
            }

            return Instantiate(kittenModel, pos, Quaternion.identity);
        }

        public void GrabAnimal(Player player, Animal animal)
        {

        }

        public void DropAnimal(Player player)
        {
            
        }

        public void KillAnimal(Player player, int keyModifier)
        {

        }
    }
}
