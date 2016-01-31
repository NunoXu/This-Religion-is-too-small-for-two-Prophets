using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class Hazard : MonoBehaviour
    {
        private GameManager gameManager;
         
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        void OnTriggerEnter(Collider col)
        {
            var tag = col.gameObject.tag;
            if (tag != "Player1" && tag != "Player2")
            {
                col.gameObject.GetComponent<Animal>().spawn.hasAnimal = false;
                gameManager.TriggerQueue(col.gameObject.GetComponent<Animal>().side, Properties.NATURAL_KILL);
                Destroy(col.gameObject);
            } else if (tag == "Player1" || tag == "Player2")
            {
                Player player = col.gameObject.GetComponent<Player>();
                gameManager.KillPlayer(player);
            }

            
        }
    }
}
