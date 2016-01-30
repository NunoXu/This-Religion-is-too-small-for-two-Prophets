﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class MeteorSpell: Spell
    {
        public float speed = 1;

        public Transform sphereTransform;
        public float startTime;
        public float journeyTime;
        public Transform startPosition;
        public Transform endPosition;
        public float direction;

        void Start()
        {
            startTime = Time.time;
            
        }
        void Update()
        {
            
        }

        void FixedUpdate()
        {
            float fracComplete = (Time.time - startTime) / journeyTime;
            Vector3 oldPosition = sphereTransform.position;
            Vector3 newPosition = Vector3.Lerp(startPosition.position, endPosition.position, fracComplete);
            newPosition.y = 0;
            sphereTransform.position = newPosition + new Vector3(0.0f, sphereTransform.transform.position.y, 0.0f);

            float distance = Vector3.Distance(oldPosition, sphereTransform.position);
            sphereTransform.Rotate(Vector3.forward, distance*30f*direction);
        }
    }
}