using Assets.Scripts.GameManager;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMarker : MonoBehaviour
    {
        private PlayerTracker _tracker;
        private void Awake()
        {
            _tracker = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerTracker>();
            _tracker.CashPlayerReference(this);
        }
    }
}
