using Assets.Scripts.GameManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Enemies
{
    class FinalEnemy : MonoBehaviour
    {
        private ScenesManager sceneManager;

        private void Awake()
        {
            sceneManager = GameObject.Find("GameManager").GetComponent<ScenesManager>();
        }
        private void OnDestroy()
        {
            sceneManager.GoToNextLevel();
        }

    }
}
