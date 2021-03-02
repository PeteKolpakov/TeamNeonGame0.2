using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GameManager;

namespace Assets.Scripts.Player
{
    public class PlayerMarker : MonoBehaviour
    {
        private PlayerTracker _tracker;
        private void Awake() {
            _tracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerTracker>();
            _tracker.CashPlayerReference(this);
        }
    }
}
