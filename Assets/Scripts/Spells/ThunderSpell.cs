using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class ThunderSpell : Spell
    {

        public Collider targetCollider;
        public ParticleSystem particles;
        public float startTime;
        public float timeForThunder = 1.5f;

        private bool done = false;

        void Start()
        {
            startTime = Time.time;
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        void FixedUpdate()
        {
            if (done && particles.isPlaying && particles.particleCount <=0)
            {
                GameObject.Destroy(this.gameObject);
            }

            if (!done && Time.time - startTime >= timeForThunder)
            {
                particles.Play();
                targetCollider.enabled = true;
                done = true;
            }
            
        }

    }
}
