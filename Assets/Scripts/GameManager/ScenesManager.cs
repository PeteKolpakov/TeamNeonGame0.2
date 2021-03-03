﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField]
        private Animator _transition;
        [SerializeField]
        private float _transitionDuration = 1f;

        private PlayerTracker _playerTracker;

        private void Start() {
            _playerTracker = GetComponent<PlayerTracker>();
        }
        public void Update()
        {
            //Testing purposes
            if (Input.GetKeyDown(KeyCode.T))
            {
                GoToNextLevel();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
            }
        }
        public static void GoToScene(int levelindex)
        {
            SceneManager.LoadScene(levelindex);
        }
        public int GetCurrentScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            return index;
        }
        public void ResetScene()
        {
            StartCoroutine(LoadLevel(GetCurrentScene())); //lol
        }
        public void GoToMainMenu()
        {
            StartCoroutine(LoadLevel(0));
        }
        public void BossScene()
        {
            StartCoroutine(LoadLevel(2));
        }
        public void GoToNextLevel()
        {
            //After Rest Stage
            StartCoroutine(LoadLevel(GetCurrentScene() + 1));
        }

        
        public IEnumerator LoadLevel(int lvlIndex)
        {
            if(_transition != null){
                _transition.SetTrigger("Start");
            }

            yield return new WaitForSeconds(_transitionDuration);

            GoToScene(lvlIndex);
            if(_transition != null)
            {
                _transition.SetTrigger("End");
            }
        }        
    }
}
