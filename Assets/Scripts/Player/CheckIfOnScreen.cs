using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    class CheckIfOnScreen : MonoBehaviour
    {
        private Transform _chaser;
        [SerializeField]
        private float _reloadSceneDistance;

        private void Start() {
            _chaser = GameObject.FindGameObjectWithTag("Chaser").transform;
        }

        private void Update()
        {
            ReloadSceneIfPlayerOffScreen();
        }
        public void ReloadSceneIfPlayerOffScreen()
        {
                if (_chaser.position.y - this.transform.position.y > _reloadSceneDistance)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
        }
    }
}
