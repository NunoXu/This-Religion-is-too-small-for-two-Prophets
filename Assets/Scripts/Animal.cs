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
        GameObject gm;

        public Spawn spawn;
        bool carried;
        GameObject carrier;

        Vector3 defaultY;

        public int type;

        //DynamicCharacter
        public KinematicData KinematicData { get; protected set; }
        private DynamicMovement movement;
        public DynamicMovement Movement
        {
            get { return this.movement; }
            set
            {
                this.movement = value;
                if (this.movement != null) this.movement.Character = this.KinematicData;
            }
        }
        public float Drag { get; set; }
        public float MaxSpeed { get; set; }


        public void Start()
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            altar1 = GameObject.FindWithTag("Altar1");
            altar2 = GameObject.FindWithTag("Altar2");
            gm = GameObject.FindWithTag("GameManager");
            defaultY = this.transform.position;

            this.KinematicData = new KinematicData(new StaticData(gameObject.transform.position));
            this.Drag = 1;
            this.MaxSpeed = 1.0f - this.Weight();

            this.Movement = new DynamicWander(0.5f, 0.05f, this.MaxSpeed)
            {
                Character = this.KinematicData,
                Target = new KinematicData(new StaticData(this.spawn.gameObject.transform.position))
            };

        }

        public void Update()
        {
            
            if (carried)
            {
                carry(this.gameObject);
            }
            else
            {
                if (this.Movement != null)
            {
                MovementOutput steering = this.Movement.GetMovement();

                //Debug.DrawRay(this.GameObject.transform.position, steering.linear,Color.blue);

                this.KinematicData.Integrate(steering, this.Drag, Time.deltaTime);
                this.KinematicData.SetOrientationFromVelocity();
                this.KinematicData.TrimMaxSpeed(this.MaxSpeed);

                this.gameObject.transform.position = this.KinematicData.position;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(this.KinematicData.orientation * MathConstants.MATH_180_PI, Vector3.up);
            }
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

                {

                    GameObject ps = (GameObject)Instantiate(gm.GetComponent<GameManager>().altarSystem, altar1.transform.position, Quaternion.identity);
                    if ( type == Properties.HORSE)
                    {                    }
                    else if (type == Properties.CAT)
                    {

                    }
                    else if (type == Properties.CHICKEN)
                    {  }
                    else if (type == Properties.SHEEP)
                    {
                        GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().rockslider, new Vector3(10.75f, 0.15f, 6.0f), Quaternion.identity);
                    }
                    else if(type == Properties.UNICORN)
                    {                    }
                    else if (type == Properties.GOAT)
                    {
                        if (gm.GetComponent<GameManager>().meteor)
                        {
                            gm.GetComponent<GameManager>().meteor = false;
                            GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().meteorr, new Vector3(-10.0f, 0.15f, 6.0f), Quaternion.identity);
                        }
                        else
                        {
                            gm.GetComponent<GameManager>().meteor = true;
                            GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().meteorr, new Vector3(-10.0f, 0.15f, -7.0f), Quaternion.identity);
                        }
                    }
                    this.spawn.hasAnimal = false;
                    Destroy(this.gameObject);
                    gm.GetComponent<GameManager>().TriggerQueue(Properties.FIRST_PLAYER, Properties.SACRIFICE_KILL);

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
                    GameObject ps = (GameObject)Instantiate(gm.GetComponent<GameManager>().altarSystem, altar2.transform.position, Quaternion.identity);
                    ps.GetComponent<ParticleSystem>().Play();
                    if (type == Properties.HORSE)
                    { }
                    else if (type == Properties.CAT)
                    {
                       
                    }
                    else if (type == Properties.CHICKEN)
                    { }
                    else if (type == Properties.SHEEP)
                    {
                        GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().rockslidel, new Vector3(10.75f, 0.15f, 6.0f), Quaternion.identity);
                    }
                    else if (type == Properties.UNICORN)
                    { }
                    else if (type == Properties.GOAT)
                    {
                        if (gm.GetComponent<GameManager>().meteor)
                        {
                            gm.GetComponent<GameManager>().meteor = false;
                            GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().meteorl, new Vector3(10.75f, 0.15f, 6.0f), Quaternion.identity);
                        }
                        else
                        {
                            gm.GetComponent<GameManager>().meteor = true;
                            GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().meteorl, new Vector3(10.75f, 0.15f, -7.0f), Quaternion.identity);
                        }
                    }
                    else if (type == Properties.CHICKEN)
                    { }
                    else if (type == Properties.SHEEP)
                    {
                        GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().rockslider, new Vector3(10.75f, 0.15f, 6.0f), Quaternion.identity);
                    }
                    else if (type == Properties.UNICORN)
                    { }
                    else if (type == Properties.GOAT)
                    {
                       
                    }
                    this.spawn.hasAnimal = false;
                    Destroy(this.gameObject);
                    gm.GetComponent<GameManager>().TriggerQueue(Properties.SECOND_PLAYER, Properties.SACRIFICE_KILL);

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
