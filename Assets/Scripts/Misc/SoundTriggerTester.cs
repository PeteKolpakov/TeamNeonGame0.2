using Assets.Scripts.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    class SoundTriggerTester : MonoBehaviour
    {
        private AudioEventTrigger _trigger;

        private void Awake()
        {
            _trigger = GetComponent<AudioEventTrigger>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _trigger.PlayOneShot();
            }
        }

    }
}
