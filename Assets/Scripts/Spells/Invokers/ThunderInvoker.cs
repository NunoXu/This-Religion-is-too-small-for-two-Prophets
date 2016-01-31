
using UnityEngine;

namespace Assets.Scripts.Spells.Invokers
{
    public class ThunderInvoker : Invoker
    {
        public Transform[] Spawnpoints;
        public Transform Altar;

        public int Thunders = 5;

        public override void Invoke()
        {
            Instantiate(SpellPrefab, Altar.position, Quaternion.identity);

            int i = 0;
            foreach (Transform trans in Spawnpoints)
            {
                Instantiate(SpellPrefab, trans.position, Quaternion.identity);

                if (i >= Thunders)
                {
                    break;
                }
            }
                   
        }
    }
}
