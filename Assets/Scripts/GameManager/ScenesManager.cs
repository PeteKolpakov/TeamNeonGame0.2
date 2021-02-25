using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager
{
    public class ScenesManager : MonoBehaviour
    {
        public int GetCurrentScene()
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            return index;
        }
        public void GoToScene()
        {
            SceneManager.LoadScene(0);
        }
        public void GoToRestStage()
        {
            SceneManager.LoadScene(6);
        }
        public void GoToNextLevel()
        {
            //After Rest Stage
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    }
}
