using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class AudioSettings : MonoBehaviour
    {
        FMOD.Studio.Bus BGM;
        FMOD.Studio.Bus SFX;
        private float BGMVol = 1f; // initializes volume values to default fader value in FMOD build
        private float SFXVol = 1f;

        private void Awake()
        {
            BGM = FMODUnity.RuntimeManager.GetBus("bus:/BGM"); // if busses have parent busses they need to be referenced in the path
            SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX"); // eg. "bus:/Master/SFX"
        }

        /*
        private void Update()
        {
            BGM.setVolume(BGMVol);
            SFX.setVolume(SFXVol); 
        }
        */
        public void SetBGMVol(float newBGMVol)
        {
            BGMVol = newBGMVol;
        }

        public void SetSFXVol(float newSFXVol)
        {
            SFXVol = newSFXVol;
        }

    }
}
