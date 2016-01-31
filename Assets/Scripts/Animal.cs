using Assets.Scripts.Movement;
using Assets.Scripts.Movement.DynamicMovement;
using Assets.Scripts.Spells;
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
        GameObject[] P1tree;
        GameObject[] P2tree;
        GameObject gm;

        public Spawn spawn;
        bool carried;
        GameObject carrier;

        Vector3 defaultY;
        Vector3 height = new Vector3(0.0f, 1.0f, 0.0f);

        public int type;
        public int side;

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
        public NavMeshAgent nav { get; set; }

        public void Start()
        {
            player1 = GameObject.FindWithTag("Player1");
            player2 = GameObject.FindWithTag("Player2");
            altar1 = GameObject.FindWithTag("Altar1");
            altar2 = GameObject.FindWithTag("Altar2");
            gm = GameObject.FindWithTag("GameManager");
            P1tree = GameObject.FindGameObjectsWithTag("P1 Tree");
            P2tree = GameObject.FindGameObjectsWithTag("P2 Tree");
            defaultY = this.transform.position;

            this.KinematicData = new KinematicData(new StaticData(gameObject.transform.position));
            this.Drag = 1;
            this.MaxSpeed = 1.0f - this.Weight();

            /*  this.Movement = new DynamicWander(0.5f, 0.05f, this.MaxSpeed)
              {
                  Character = this.KinematicData,
                  Target = new KinematicData(new StaticData(this.spawn.gameObject.transform.position))
              };
  */
            nav = GetComponent<NavMeshAgent>();
        }

        public void Update()
        {
            
            if (carried)
            {
                carry(this.gameObject);
               

            }
            else
            {
                Vector3 dest = this.spawn.transform.position + UnityEngine.Random.insideUnitSphere * 20.0f;
                dest.y = 0;
                GotoPosition(dest, this.MaxSpeed);
                /*
                if (this.Movement != null)
            {
                MovementOutput steering = this.Movement.GetMovement();

                //Debug.DrawRay(this.GameObject.transform.position, steering.linear,Color.blue);

                this.KinematicData.Integrate(steering, this.Drag, Time.deltaTime);
                this.KinematicData.SetOrientationFromVelocity();
                this.KinematicData.TrimMaxSpeed(this.MaxSpeed);

                this.gameObject.transform.position = this.KinematicData.position;
                this.gameObject.transform.rotation = Quaternion.AngleAxis(this.KinematicData.orientation * MathConstants.MATH_180_PI, Vector3.up);
            }*/
                pickUp();
            }
        }
        private void GotoPosition(Vector3 pos, float speed)
        {
            nav.speed = speed;
            nav.SetDestination(pos);
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
            Vector3 nposition = height + carrier.transform.position + carrier.transform.forward * 1.25f;
            o.transform.position = nposition;
            if ((carrier == player1) && (Input.GetButtonDown("Fire1")))
            {
                carried = false;
                player1.GetComponent<Player1>().hasSacrifice = false;

                gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                if (Vector3.Distance(this.gameObject.transform.position, altar1.transform.position) < 1.25f)

                {

                    GameObject ps = (GameObject)Instantiate(gm.GetComponent<GameManager>().altarSystem, altar1.transform.position, Quaternion.identity);

                   if(UnityEngine.Random.Range(0.0f,1.0f)> 0.75f)
                   {
                    altar1.GetComponentsInChildren<AudioSource>()[UnityEngine.Random.Range(0,3)].Play();
                   }


                    if ( type == Properties.HORSE)
                    {                    }
                    else if (type == Properties.CAT)
                    {
                        foreach(GameObject ob in P1tree)
                        {
                            ob.GetComponent<HeatWaveSpell>().Invoke();
                        }
                    }
                    else if (type == Properties.CHICKEN)
                    {
                        CalculateThunderP1();

                        }
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
                    return;
                }
                else
                {
                    o.transform.position = new Vector3(nposition.x, defaultY.y, nposition.z);
                }

                nav.enabled = true;
                this.GetComponent<Rigidbody>().detectCollisions = true;
            }
            if ((carrier == player2) && (Input.GetButtonDown("Fire12")))
            {
                carried = false;
                player2.GetComponent<Player2>().hasSacrifice = false;
                gameObject.GetComponentInChildren<ParticleSystem>().Stop();
                if (Vector3.Distance(this.gameObject.transform.position, altar2.transform.position) < 1.25f)
                {
                    if (UnityEngine.Random.Range(0.0f, 1.0f) > 0.75f)
                    {
                        altar1.GetComponentsInChildren<AudioSource>()[UnityEngine.Random.Range(0, 3)].Play();
                    }

                    GameObject ps = (GameObject)Instantiate(gm.GetComponent<GameManager>().altarSystem, altar2.transform.position, Quaternion.identity);
                    if (type == Properties.HORSE)
                    { }
                    else if (type == Properties.CAT)
                    {
                        foreach (GameObject ob in P2tree)
                        {
                            ob.GetComponent<HeatWaveSpell>().Invoke();
                        }
                    }
                    else if (type == Properties.CHICKEN)
                    {
                        CalculateThunderP2();
                    }
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
                    

                    this.spawn.hasAnimal = false;
                    Destroy(this.gameObject);
                    gm.GetComponent<GameManager>().TriggerQueue(Properties.SECOND_PLAYER, Properties.SACRIFICE_KILL);
                    return;
                }
                else
                {
                    o.transform.position = new Vector3(nposition.x, defaultY.y, nposition.z);
                }

                nav.enabled = true;
                this.GetComponent<Rigidbody>().detectCollisions = true;
            }
        }

        void pickUp()
        {
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
            if ((Vector3.Distance(player1.transform.position,this.gameObject.transform.position) < 1.5) &&
                (Input.GetButtonDown("Fire1")))
            {
                if (player1.GetComponent<Player1>().hasSacrifice == false)
                {
                    
                    carrier = player1;
                    carried = true;
                    nav.enabled = false;
                    player1.GetComponent<Player1>().setSacrifice(this.gameObject);
                    this.GetComponent<Rigidbody>().detectCollisions = false;
                }
            }

            if ((Vector3.Distance(player2.transform.position,this.gameObject.transform.position) < 1.5) &&
                (Input.GetButtonDown("Fire12")))
            {
                if (player2.GetComponent<Player2>().hasSacrifice == false)
                {
                    carrier = player2;
                    carried = true;
                    nav.enabled = false;
                    player2.GetComponent<Player2>().setSacrifice(this.gameObject);
                    this.GetComponent<Rigidbody>().detectCollisions = false;
                }
            }

        }

        void CalculateThunderP2()
        {
            Vector2 thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += 3.572f;
            thunderPos.y += -2.714f;
            GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += 3.572f;
            thunderPos.y += -2.714f;
            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += 3.572f;
            thunderPos.y += -2.714f;
            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += 3.572f;
            thunderPos.y += -2.714f;
            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(3.572f, 0.15f, -2.714f), Quaternion.identity);

        }

        void CalculateThunderP1()
        {

            Vector2 thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += -17.082f;
            thunderPos.y += -2.749f;
            GameObject pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += -17.082f;
            thunderPos.y += -2.749f;
            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += -17.082f;
            thunderPos.y += -2.749f;
            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            thunderPos = UnityEngine.Random.insideUnitCircle;
            thunderPos *= 9.15f;
            thunderPos.x += -17.082f;
            thunderPos.y += -2.749f;
            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(thunderPos.x, 0.15f, thunderPos.y), Quaternion.identity);

            pse = (GameObject)Instantiate(gm.GetComponent<GameManager>().thunder, new Vector3(-17.082f, 0.15f, -2.749f), Quaternion.identity);
        }

    }
}
