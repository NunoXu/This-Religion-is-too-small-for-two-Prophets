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

        bool carried;
        GameObject carrier;

        Vector3 defaultY;

        public int type;

        //private fields

        public void Start()
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
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

        public Player Player(int number)
        {
            if(number == Properties.FIRST_PLAYER)
            return player1.GetComponent<Player>();
            else
                return player2.GetComponent<Player>();
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
                player1.GetComponent<Player>().hasSacrifice = false;
                o.transform.position = new Vector3(nposition.x, defaultY.y, nposition.z);
            }
            if ((carrier == player2) && (Input.GetKeyDown("[.]")))
            {
                carried = false;
                player2.GetComponent<Player>().hasSacrifice = false;
                o.transform.position = new Vector3(nposition.x, defaultY.y, nposition.z);
            }
        }

        void pickUp()
        {
            if (((player1.transform.position - this.gameObject.transform.position).sqrMagnitude < 1.5) &&
                (Input.GetKeyDown(KeyCode.LeftControl)))
            {
                if (player1.GetComponent<Player>().hasSacrifice == false)
                {

                    player1.GetComponent<Player>().hasSacrifice = true;
                    carrier = player1;
                    carried = true;
                }
            }

            if (((player2.transform.position - this.gameObject.transform.position).sqrMagnitude < 1.5) &&
                (Input.GetKeyDown("[.]")))
            {
                if (player2.GetComponent<Player>().hasSacrifice == false)
                {
                    carrier = player2;
                    player2.GetComponent<Player>().hasSacrifice = true;
                    carried = true;
                }
            }

        }
    }
}
