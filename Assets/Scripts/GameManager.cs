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

        public GameObject horseModel;
        public GameObject kittenModel;
        public GameObject goatModel;
        public GameObject roosterModel;
        public GameObject sheepModel;
        public GameObject unicornModel;

        public GameObject[] SacrificesPlayerOne;
        public GameObject[] SacrificesPlayerTwo;

        //private fields

        private int MaxNumberOfObjectsPerPlayer = 6;

        private Player PlayerOne;
        private Player PlayerTwo;

        private int PlayerOneSpawns;
        private int PlayerTwoSpawns;

        static readonly System.Random _random = new System.Random();

        public void Start()
        {
            /*
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
            */
            this.PlayerOne = PlayerOneObject.GetComponent<Player>();
            this.PlayerTwo = PlayerTwoObject.GetComponent<Player>();

            this.PlayerOneSpawns = 0;
            this.PlayerTwoSpawns = 0;

            while (this.PlayerOneSpawns < Properties.MAX_ANIMALS)
                CalculateAndGenerateAnimal(Properties.FIRST_PLAYER);
            while (this.PlayerTwoSpawns < Properties.MAX_ANIMALS)
                CalculateAndGenerateAnimal(Properties.SECOND_PLAYER);
    
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

        private void CalculateAndGenerateAnimal(int player)
        {
            int spawnedNumber = (player == Properties.FIRST_PLAYER) ? this.PlayerOneSpawns : this.PlayerTwoSpawns;

            if (spawnedNumber >= Properties.MAX_ANIMALS)
                return;

            while (true)
            {
                int spot = Random.Range(0, Properties.ANIMAL_SPAWNS);
                GameObject spawnObj = (player == Properties.FIRST_PLAYER) ? this.SacrificesPlayerOne[spot] : this.SacrificesPlayerTwo[spot];
                Spawn spawn = spawnObj.GetComponent<Spawn>();
                if (spawn.hasAnimal)
                    continue;
                else
                {
                    spawn.CurrentAnimalObject = (GameObject)GenerateRandomAnimal(CalculatePoint(spawn.transform.position));
                    spawn.hasAnimal = true;
                    if (player == Properties.FIRST_PLAYER)
                        PlayerOneSpawns++;
                    else
                        PlayerTwoSpawns++;
                    return;
                }
            }
        }

        private Vector3 CalculatePoint(Vector3 _origin)
        {
            var angle = _random.NextDouble() * System.Math.PI * 2;
            var radius = System.Math.Sqrt(_random.NextDouble()) * Properties.ANIMAL_SPAWN_RADIUS;
            var x = _origin.x + radius * System.Math.Cos(angle);
            var z = _origin.z + radius * System.Math.Sin(angle);
            return new Vector3((float)x, _origin.y, (float)z);
        }

        private Object GenerateRandomAnimal(Vector3 pos)
        {
            int type = Random.Range(0, Properties.ANIMAL_TYPES);
            return GenerateAnimal(type, pos);
        }

        private Object GenerateAnimal(int type, Vector3 pos)
        {
            if (type == Properties.HORSE)
            {
                return Instantiate(horseModel, pos, Quaternion.identity);
            }
            else if (type == Properties.CAT)
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
