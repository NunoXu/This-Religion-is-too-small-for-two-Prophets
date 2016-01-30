using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells.Invokers
{
    class HeatWaveInvoker : Invoker
    {
        public GameObject[] Trees;
        
        public override void Invoke()
        {
            foreach (GameObject tree in Trees)
            {
                HeatWaveSpell spell = tree.GetComponent<HeatWaveSpell>();
                spell.Invoke();
            }    
        }
    }
}
