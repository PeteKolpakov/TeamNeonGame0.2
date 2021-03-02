using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Audio
{
    class MenuAudio : MonoBehaviour
    {
        [FMODUnity.EventRef] [SerializeField] private string _mouseOverSFX;
        [FMODUnity.EventRef] [SerializeField] private string _buttonPressSFX;

        public void PlayMouseOverSFX()
        {
                FMODUnity.RuntimeManager.PlayOneShot(_mouseOverSFX);
        }

        public void PlayButtonClickSFX()
        {
                FMODUnity.RuntimeManager.PlayOneShot(_buttonPressSFX);
        }

    }
}
