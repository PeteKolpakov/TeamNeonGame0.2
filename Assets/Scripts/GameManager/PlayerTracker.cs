using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameManager{
    public class PlayerTracker : MonoBehaviour
    {
        public static PlayerTracker Instance;
        public PlayerMarker Player;

        private void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMarker>();
            Debug.Log("I have awakened!");
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
