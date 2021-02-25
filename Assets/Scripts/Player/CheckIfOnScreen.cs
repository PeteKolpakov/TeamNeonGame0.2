using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    class CheckIfOnScreen : MonoBehaviour
    {
        [SerializeField]
        private Transform _chaser;
        [SerializeField]
        private float _reloadSceneDistance;

        private void Start()
        {
            
        }
        private void Update()
        {
            ReloadSceneIfPlayerOffScreen();
        }
        public void ReloadSceneIfPlayerOffScreen()
        {
            if (_chaser != null)
            {
                if (_chaser.position.y - this.transform.position.y > _reloadSceneDistance)
                {
                    Debug.Log("Reloaded mf");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}
