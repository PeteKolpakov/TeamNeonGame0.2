﻿using NaughtyAttributes;
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

<<<<<<< HEAD
<<<<<<< HEAD
=======
        // TODO : hook this shit up
        public bool HasSpawned = false;
=======
        [SerializeField][ReadOnly] // TODO : hook this shit up
        private bool HasSpawned = false;
>>>>>>> Pete'sBranch

        private void Awake()
        {
            HasSpawned = false;
        }

>>>>>>> Production
        private void Update()
        {
            if (_chaser != null)
            {
                if (_chaser.position.y - this.transform.position.y > _despawnDistance)
                {
                    Destroy(gameObject);
                }
            }
        }

        // sets up dependency injection for the chacer when section is pawned, ALSO sets HasSpawned to True
        public void Setup(Transform chaser)
        {
            _chaser = chaser;
            HasSpawned = true;
        }



    }
}
