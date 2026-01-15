using System;
using UnityEngine;

namespace Utils
{
    public class Cooldown
    {
        private float _cooldownTime;
        private float _lastUseTime = -Mathf.Infinity;
        
        public Cooldown(float cooldownTime)
        {
            _cooldownTime = cooldownTime;
        }

        public bool IsReady => Time.time >= _lastUseTime + _cooldownTime;

        public void Use()
        {
            _lastUseTime = Time.time;
        }

        public float GetRemainingTime => Mathf.Max(0f, (_lastUseTime + _cooldownTime) - Time.time);

        public void Reset()
        {
            _lastUseTime = -Mathf.Infinity;
        }
    }
}