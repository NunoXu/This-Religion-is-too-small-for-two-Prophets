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
        public GameObject altarSystem;
        public GameObject rockslide;
        public GameObject meteorr;
        public GameObject meteorl;

        public GameObject[] SacrificesPlayerOne;
        public GameObject[] SacrificesPlayerTwo;

        //private fields

        private int MaxNumberOfObjectsPerPlayer = 6;

        private Player PlayerOne;
        private Player PlayerTwo;

        private int PlayerOneSpawns;
        private int PlayerTwoSpawns;

        private Queue<int> RespawnQueuePlayerOne;
        private float currentRespawnStartTimePlayerOne;

        private Queue<int> RespawnQueuePlayerTwo;
        private float currentRespawnStartTimePlayerTwo;

        static readonly System.Random _random = new System.Random();

        public void Start()
        {
            this.RespawnQueuePlayerOne = new Queue<int>();
            this.RespawnQueuePlayerTwo = new Queue<int>();

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


            if (RespawnQueuePlayerOne.Count > 0)
                CheckForRespawn(Properties.FIRST_PLAYER);
            if (RespawnQueuePlayerTwo.Count > 0)
                CheckForRespawn(Properties.SECOND_PLAYER);
            

            //Wander sacrifices 
            


        }

        private void CheckForRespawn(int player)
        {
            if (player == Properties.FIRST_PLAYER)
            {

                int currentQueueTime = RespawnQueuePlayerOne.Peek();

                float timecount = Time.time - currentRespawnStartTimePlayerOne;
                int timecountSec = (int)Mathf.Round(timecount % 60f);

                if (currentQueueTime > timecountSec)
                {
                    RespawnQueuePlayerOne.Dequeue();
                    CalculateAndGenerateAnimal(player);

                    if (RespawnQueuePlayerOne.Count > 0)
                    {
                        currentRespawnStartTimePlayerOne = Time.time;
                    }

                }

            } else
            {
                int currentQueueTime = RespawnQueuePlayerTwo.Peek();

                float timecount = Time.time - currentRespawnStartTimePlayerTwo;
                int timecountSec = (int)Mathf.Round(timecount % 60f);

                if (currentQueueTime > timecountSec)
                {
                    RespawnQueuePlayerTwo.Dequeue();
                    CalculateAndGenerateAnimal(player);

                    if (RespawnQueuePlayerTwo.Count > 0)
                    {
                        currentRespawnStartTimePlayerTwo = Time.time;
                    }

                }
            }
        }

        public void TriggerQueue(int player, int time)
        {
            if (player == Properties.FIRST_PLAYER)
            {
                if (RespawnQueuePlayerOne.Count == 0)
                    this.currentRespawnStartTimePlayerOne = Time.time;
                RespawnQueuePlayerOne.Enqueue(time);
            }
            else
            {
                if (RespawnQueuePlayerTwo.Count == 0)
                    this.currentRespawnStartTimePlayerTwo = Time.time;
                RespawnQueuePlayerTwo.Enqueue(time);
            }
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
                    spawn.CurrentAnimal().spawn = spawn;
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
