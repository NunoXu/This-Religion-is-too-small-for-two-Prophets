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
        public float startTime;
        public float fireTime = 5f;

        private bool done = true;

        public void Invoke()
        {
            startTime = Time.time;
            ps.Play();
            done = false;
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        void Update()
        {
            if (!done && Time.time - startTime >= fireTime) {
                ps.Stop();
                done = true;
            }
        }


    }
}
