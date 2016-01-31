using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells.Invokers
{
    class MeteorInvoker : Invoker
    {
        public Transform SpawnPoint;

        public override void Invoke()
        {
            Instantiate(SpellPrefab, SpawnPoint.position, Quaternion.identity);
        }
    }
}
