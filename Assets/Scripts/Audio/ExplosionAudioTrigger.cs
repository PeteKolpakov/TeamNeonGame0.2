using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class ExplosionAudioTrigger : MonoBehaviour
    {
        [FMODUnity.EventRef] [SerializeField] public string Sound;

        public void OnDestroy()
        {
            FMODUnity.RuntimeManager.PlayOneShot(Sound);
        }

    }
} 
