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
            Debug.Log("PlayerTracker has awakened");
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void CashPlayerReference(PlayerMarker player){
            
            Player = player;
        }

        
    }
}
