using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    /// <summary>
    /// Handles Triggering AudioClips through FMOD
    /// </summary>
    class AudioEventTrigger : MonoBehaviour
    {
        [FMODUnity.EventRef] public string Event;
        [SerializeField]     private bool PlayOnAwake;
        [SerializeField]     private bool PlayOnDestroy;

        /// <summary>
        /// Plays Audio Clip selected in component
        /// </summary>
        public void PlayOneShot()
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
        }

        private void Start()
        {
            if (PlayOnAwake)
                PlayOneShot();            
        }
        private void OnDestroy()
        {
            if (PlayOnDestroy)
                PlayOneShot();
        }
    }
}
