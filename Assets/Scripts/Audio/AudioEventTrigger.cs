using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class AudioEventTrigger : MonoBehaviour
    {
        [FMODUnity.EventRef] [SerializeField] public string Sound;

        public void PlaySound()
        {
            FMODUnity.RuntimeManager.PlayOneShot(Sound);
        }
    }
}
