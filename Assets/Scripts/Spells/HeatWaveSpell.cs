using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class HeatWaveSpell: Spell
    {
        public ParticleSystem ps;
        public Collider targetCollider;
        public float startTime;
        private float fireTime = 15f;

        private bool done = true;

        public void Invoke()
        {
            startTime = Time.time;
            ps.Play();
            targetCollider.enabled = true;
            done = false;
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        void Update()
        {
            if (!done && Time.time - startTime >= fireTime) {
                ps.Stop();
                targetCollider.enabled = false;
                done = true;
            }
        }


    }
}
