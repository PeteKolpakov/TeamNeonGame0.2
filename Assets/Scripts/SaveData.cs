using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Serialization
{
    [System.Serializable]
    public class SaveData
    {
        private static SaveData _current;
        public static SaveData current
        {
            get
            {
                if(_current == null)
                {
                    _current = new SaveData();
                }
                return _current;
            }
        }

        public PlayerProfile profile;
        public int healingCharges;
        public int bullets;
       
    }
}
