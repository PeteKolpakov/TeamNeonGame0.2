using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGen_Scripts
{
    class LvlGenManager : MonoBehaviour
    {
        private const float MIN_DISTANCE_TO_SPAWN_TRIGGER = 20f;

        [SerializeField]
        private Transform _levelPart_Start;
        [SerializeField]
        private List<Transform> _levelPartList;
        [SerializeField]
        private Transform _chaser;
        [SerializeField]
        private int _preSpawnLevelParts = 4;

        private Vector3 _lastEndPos;

        private void Awake()
        {
            // anchor LastEndPos to Lvl_Start Section (Section must be active in scene)
            _lastEndPos = _levelPart_Start.Find("EndPos").position;         

            // prespwn initial lvl parts
            for (int i = 0; i < _preSpawnLevelParts; i++)
            {
                SpawnLevelPart();
            }

        }

        private void Update()
        {
            if (Vector3.Distance(_chaser.position, _lastEndPos) < MIN_DISTANCE_TO_SPAWN_TRIGGER)   // Spawn new Section when Chaser is close enough
            {
                SpawnLevelPart();
            }
        }

        private void SpawnLevelPart()
        {
            Transform selectedLevelSection = _levelPartList[Random.Range(0, _levelPartList.Count)];             // Select next Section from list 
            Transform lastSpawnedSectionTransform = SpawnSectionGetPosition(selectedLevelSection, _lastEndPos); // Spawn Selected Section at lastEndPos, Store Spawned Section Transform
            _lastEndPos = lastSpawnedSectionTransform.Find("EndPos").position;                                  // Update last used EndPos
        }
        private Transform SpawnSectionGetPosition(Transform levelSection, Vector3 spawnPosition)                
        {
            Transform sectionTransform = Instantiate(levelSection, spawnPosition, Quaternion.identity);         // Spawn Lvl Section
            return sectionTransform;                                                                            // return position it spawned at
        }
    }
}
