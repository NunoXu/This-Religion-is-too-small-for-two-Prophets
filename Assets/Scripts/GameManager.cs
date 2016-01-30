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
        
        public GameObject AltarOne;
        public GameObject AltarTwo;

        public GameObject horseModel;
        public GameObject kittenModel;

        //private fields
        private List<GameObject> Sacrifices;
        private int MaxNumberOfObjectsPerPlayer = 6;

        public void Start()
        {
            /*
            for (int i = 0; i < MaxNumberOfObjectsPerPlayer; i++)
            {
                float angle = i * Mathf.PI * 2 / MaxNumberOfObjectsPerPlayer;
                Vector3 pos = AltarOne.transform.localPosition;
                pos.x += (1f * Mathf.Cos(angle));
                pos.z += (1f * Mathf.Sin(angle));
                GenerateAnimal(pos);
            }
            */

           /* for (int i = 0; i < MaxNumberOfObjectsPerPlayer; i++)
            {
                float angle = i * Mathf.PI * 2 / MaxNumberOfObjectsPerPlayer;
                Vector3 pos = new Vector3(Mathf.Cos(angle), AltarTwo.transform.position.y, Mathf.Sin(angle)) * 5f;
                GenerateAnimal(pos);
            }*/

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
                return Instantiate(kittenModel, pos, Quaternion.identity);
            }
            else if(type == Properties.CAT)
            {
                return Instantiate(horseModel, pos, Quaternion.identity);
            }
            return null;
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
