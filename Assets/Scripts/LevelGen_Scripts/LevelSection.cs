using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGen_Scripts
{
    class LevelSection : MonoBehaviour
    {
        [SerializeField]
        private Transform _chaser;

        [SerializeField]
        private float _despawnDistance = 35f; // set this to be relevant to the height of the section

        [SerializeField]
        private Transform _endPosition;

        public Transform EndPosition
        {
            get { return _endPosition; }
        }


        private void Update()
        {
            if (_chaser != null)
            {
                if (_chaser.position.y - this.transform.position.y > _despawnDistance)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("LVL GEN ERROR: SECTION CHASER REFERENCE IS NULL");
                }
            }
        }

        // sets up dependency injection for the chacer when section is pawned
        public void Setup(Transform chaser)
        {
            _chaser = chaser;
        }



    }
}
