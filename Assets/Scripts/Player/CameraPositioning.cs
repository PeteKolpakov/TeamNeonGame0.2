using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Player
{
    class CameraPositioning : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private PlayerMovement _playerMovement;
        private float _halfWidht;
        private float _halfHeight;
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _halfHeight = _camera.orthographicSize;
            _halfWidht = -_halfHeight * ((float)Screen.width / Screen.height);

            outOfBounds(_playerMovement.transform);
        }
        public void outOfBounds(Transform transform)
        {
            if(transform.position.y < -_halfHeight)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Debug.Log("Scene reloaded");
            }
        }

    }
}
