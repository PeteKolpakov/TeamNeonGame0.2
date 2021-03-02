using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class PlayerAudio : MonoBehaviour
    {
        [FMODUnity.EventRef] public string _dashSFX;
        [FMODUnity.EventRef] public string _fallSFX;
        [FMODUnity.EventRef] public string _takeDamageSFX;
        [FMODUnity.EventRef] public string _dieSFX;
        [FMODUnity.EventRef] public string _pauseEnterSFX;
        [FMODUnity.EventRef] public string _pauseExitSFX;

        public void PlaySFX(string path)
        {
            FMODUnity.RuntimeManager.PlayOneShot(path);
        }
    }
}
