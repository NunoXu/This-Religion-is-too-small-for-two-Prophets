using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public abstract class Invoker : MonoBehaviour
    {
        public GameObject SpellPrefab;


        void Start()
        {
            Init();
        }

        protected virtual void Init()
        {

        }

        public abstract void Invoke();
    }
}
