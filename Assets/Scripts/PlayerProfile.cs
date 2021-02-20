using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Serialization
{
    [System.Serializable]
    public struct PlayerProfile
    {
        public string playerName;
        public int currency;
        public int health;
        public int charges;
        public float damage;
        public Item item;
    }
}
