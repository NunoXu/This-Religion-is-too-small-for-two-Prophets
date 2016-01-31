using Assets.Scripts.Movement;
using Assets.Scripts.Movement.DynamicMovement;
using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class Animal : MonoBehaviour
    {
        //public fields to be set in Unity Editor
        GameObject player1;
        GameObject player2;
        GameObject altar1;
        GameObject altar2;
        GameObject ps1;
        GameObject ps2;
        GameObject ps3;
        GameObject gm;

        public Spawn spawn;
        bool carried;
        GameObject carrier;

        Vector3 defaultY;

        public int type;

        //DynamicCharacter
        public GameObject GameObject { get; protected set; }


        public void Start()
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            altar1 = GameObject.FindWithTag("Altar1");
            altar2 = GameObject.FindWithTag("Altar2");
          /*  ps1 = GameObject.FindWithTag("partS1");
            ps2 = GameObject.FindWithTag("partS2");
            ps3 = GameObject.FindWithTag("partS3");
            */gm = GameObject.FindWithTag("GameManager");
            defaultY = this.transform.position;

        }

        public void Update()
        {
          
            if (carried)
            {
                carry(this.gameObject);
            }
            else
            {
                
                pickUp();
            }
        }

        public Player Carrier()
        {
                return carrier.GetComponent<Player>();
        }

        public float Weight()
        {
            if (type == Properties.HORSE)
            {
                return Properties.HORSE_WEIGHT;
            }
            else if (type == Properties.CAT)
            {
                return Properties.CAT_WEIGHT;
            }
            else if (type == Properties.GOAT)
            {
                return Properties.GOAT_WEIGHT;
            }
            else if (type == Properties.UNICORN)
            {
                return Properties.UNICORN_WEIGHT;
            }
            else if (type == Properties.CHICKEN)
            {
                return Properties.CHICKEN_WEIGHT;
            }
            else if (type == Properties.SHEEP)
            {
                return Properties.SHEEP_WEIGHT;
            }
            return 1.0f;
        }

        void carry(GameObject o)
        {
            Vector3 nposition = carrier.transform.position + carrier.transform.forward * 1.25f;
            o.transform.position = nposition;
            if ((carrier == player1) && (Input.GetKeyDown(KeyCode.LeftControl)))
            {
                carried = false;
                player1.GetComponent<Player1>().hasSacrifice = false;

                if (Vector3.Distance(this.gameObject.transform.position, altar1.transform.position) < 1.25f)
                {/*
                    ps1.GetComponent<ParticleSystem>().Play();
                    ps2.GetComponent<ParticleSystem>().Play();
                    ps3.GetComponent<ParticleSystem>().Play();
                   */ gm.GetComponent<GameManager>().TriggerQueue(Properties.FIRST_PLAYER,Properties.SACRIFICE_KILL);
                    Destroy(this.gameObject);
                }
                else
                {
                    o.transform.position = new Vector3(nposition.x, defaultY.y, nposition.z);
                }
            }
            if ((carrier == player2) && (Input.GetKeyDown("[.]")))
            {
                carried = false;
                player2.GetComponent<Player2>().hasSacrifice = false;
                if (Vector3.Distance(this.gameObject.transform.position, altar2.transform.position) < 1.25f)
                {
                    gm.GetComponent<GameManager>().TriggerQueue(Properties.SECOND_PLAYER, Properties.SACRIFICE_KILL);
                    Destroy(this.gameObject);
                }
                else
                {
                    o.transform.position = new Vector3(nposition.x, defaultY.y, nposition.z);
                }
            }
        }

        void pickUp()
        {
            if ((Vector3.Distance(player1.transform.position,this.gameObject.transform.position) < 1.5) &&
                (Input.GetKeyDown(KeyCode.LeftControl)))
            {
                if (player1.GetComponent<Player1>().hasSacrifice == false)
                {
                    
                    carrier = player1;
                    carried = true;
                    player1.GetComponent<Player1>().setSacrifice(this.gameObject);
                }
            }

            if ((Vector3.Distance(player2.transform.position,this.gameObject.transform.position) < 1.5) &&
                (Input.GetKeyDown("[.]")))
            {
                if (player2.GetComponent<Player2>().hasSacrifice == false)
                {
                    carrier = player2;
                    carried = true;
                    player2.GetComponent<Player2>().setSacrifice(this.gameObject);
                }
            }

        }


    }
}
