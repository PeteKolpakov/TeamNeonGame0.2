using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class BGMPlayer : MonoBehaviour
    {
        [FMODUnity.EventRef] public string _gameBGM;

        public float full = 1f;
        public float filtered = 0f;
        public float quiet = 0.5f;

        private void Start()
        {
            
        }

        public void PlayBGM()
        {

        }

        public void SetBGMXFade(float set)
        {
            
        }
    }
}
