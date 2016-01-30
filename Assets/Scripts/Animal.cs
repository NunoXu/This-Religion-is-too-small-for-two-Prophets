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

        float defaultY;

        public int Type;

        //private fields

        public void Start()
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            defaultY = this.transform.position.y;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (carried && (Carrier().PlayerNumber == Properties.FIRST_PLAYER))
                {
                    Carrier().hasSacrifice = false;
                    resetY();
                }
                else
                {
                    if ((player1.transform.position - this.gameObject.transform.position).sqrMagnitude < 1.5)
                    {
                        if (!Player(Properties.FIRST_PLAYER).hasSacrifice)
                        {
                            Player(Properties.FIRST_PLAYER).setSacrifice(this.gameObject);
                            carrier = player1;
                            carried = true;
                        }
                    }
                }
            }

            if (Input.GetKeyDown("[.]"))
            {
                if (carried && (Carrier().PlayerNumber == Properties.SECOND_PLAYER))
                {
                    Carrier().hasSacrifice = false;
                    resetY();
                }
                else
                {
                    if ((player2.transform.position - this.gameObject.transform.position).sqrMagnitude < 1.5)
                    {
                        if (!Player(Properties.SECOND_PLAYER).hasSacrifice)
                        {
                            Player(Properties.SECOND_PLAYER).setSacrifice(this.gameObject);
                            carrier = player2;
                            carried = true;
                        }
                    }
                }
            }

            if (carried)
            {
                Vector3 nposition = carrier.transform.position + carrier.transform.forward * 1.25f;
                this.gameObject.transform.position = nposition;
            }
        }

        public void resetY()
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, defaultY, this.gameObject.transform.position.z);
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
    }
}
