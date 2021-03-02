using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class PlayerAudio : MonoBehaviour
    {
        [FMODUnity.EventRef] public string _dashSFX;
        [FMODUnity.EventRef] public string _hurtSFX;
        [FMODUnity.EventRef] public string _deathSFX;
        [FMODUnity.EventRef] public string _fallSFX;
        [FMODUnity.EventRef] public string _gunshotBasicSFX;
        //[FMODUnity.EventRef] public string _healSFX;

        public void TriggerAudio(string SoundEvent)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(SoundEvent, gameObject);
        }

        public void PlayerShoot()
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(_gunshotBasicSFX, gameObject);
        }
    }
}
