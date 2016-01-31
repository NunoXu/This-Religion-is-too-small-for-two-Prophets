using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class Meteor : MonoBehaviour
    {
        public MeteorSpell spell;

        void OnTriggerEnter(Collider col)
        {
            var tag = col.gameObject.tag;
            if (tag != "Player1" && tag != "Player2")
            {
                col.gameObject.GetComponent<Animal>().spawn.hasAnimal = false;
                spell.gameManager.TriggerQueue(spell.Player, Properties.NATURAL_KILL);
                Destroy(col.gameObject);
            }
        }
    }
}
