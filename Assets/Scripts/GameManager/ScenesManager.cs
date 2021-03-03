using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField]
        private Animator transition;
        private PlayerTracker _playerTracker;

        private void Start() {
            _playerTracker = GetComponent<PlayerTracker>();
        }
        public void Update()
        {
            //Testing purposes
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
            }
        }
        public int GetCurrentScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            return index;
        }
        public static void GoToScene(int levelindex)
        {
            SceneManager.LoadScene(levelindex);
        }
        public static void GoToRestStage()
        {
            SceneManager.LoadScene(6);
        }
        public void GoToNextLevel()
        {
            //After Rest Stage
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }
        public enum SceneIndex
        {
            //Can also be done with .ToString, this is just a place holder for illustration
            MainMenu,
            LevelOne,
            LevelTwo,
            LevelThree,
            LevelFour,
            LevelFive,
            RestStage
        }
        public IEnumerator LoadLevel(int lvlIndex)
        {
            if(transition != null){
                transition.SetTrigger("Start");

            }

            yield return new WaitForSeconds(1);

            GoToScene(lvlIndex);
        }

        
        
    }
}
