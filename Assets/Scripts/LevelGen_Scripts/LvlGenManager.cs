using Assets.Scripts.CameraBehaviour;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.LevelGen_Scripts
{
    public class LvlGenManager : MonoBehaviour
    {
        private const float DistanceNeededToSpawn = 20f;

        [SerializeField] private Transform _lvlStartSection;
        [SerializeField] private Chaser _chaser;
        [SerializeField] private List<Chunk> _level;

        //private List<LevelSection> _generatedSectionsCache = new List<LevelSection>(); // used for tracking generated sections in editor

        private Vector3 _lastEndPos;

        [SerializeField][ReadOnly] private int _chunkIndex = 0;
        [SerializeField][ReadOnly] private int _sectionIndex = 0;


        private void Awake()
        {
            Setup();
        }
        private void Update()
        {
           Generate();
           //ChaserSpeedRefresh();
        }

        private void Setup()
        {
            SceneReset();
            _lastEndPos = _lvlStartSection.Find("EndPos").position;
            ShuffleChunks();
            Generate();
        }
        private void Generate()
        {                       
            if (ShouldSpawn())
            {
                for (int i = _chunkIndex; i < _level.Count; i++)
                {
                    if (ShouldSpawn())
                    {
                        for (int n = _sectionIndex; n < _level[_chunkIndex].sections.Count; n++)
                        {
                            GenerateSectionAtIndex(_chunkIndex, _sectionIndex);
                            _sectionIndex++;                   
                        }
                        _sectionIndex = 0;
                        _chunkIndex++;
                    }                    
                    Debug.Log("All Level Sections Generated");
                }
            }            
        }
        private void GenerateSectionAtIndex(int chunk, int section)
        {
            Transform selectedSectionPrefab = _level[chunk].sections[section].transform;                                     // Select next Section from list
            LevelSection lastSpawnedSectionTransform = GenerateSection(selectedSectionPrefab, _lastEndPos); // Spawn Selected Section at lastEndPos, Store Spawned Section Transform
            _lastEndPos = lastSpawnedSectionTransform.EndPosition.position;                              // Update last used EndPos
        }
        private LevelSection GenerateSection(Transform levelPrefab, Vector3 spawnPosition)   
            
        {
            Transform sectionTransform = Instantiate(levelPrefab, spawnPosition, Quaternion.identity, transform);  // Spawn Lvl Section, make child of gen manager
            LevelSection levelSection = sectionTransform.GetComponent<LevelSection>();
            levelSection.Setup(_chaser.transform);
            return levelSection;                                                                            // return position it spawned at
        }

        //private void ChaserSpeedRefresh()
        //{
        //    _chaser.SetSpeed(_level[_chunkIndex].chaserSpeed);
        //}

        private void CheckCache()
        {
            if (transform.childCount > 0)   
            {
                SceneReset();                    
            }
        }
        private bool ShouldSpawn()
        {
            if (Application.isPlaying)
            {
                return Vector3.Distance(_chaser.transform.position, _lastEndPos) < DistanceNeededToSpawn;
            }
            else return true; 
        }
        private void IndexReset()
        {
            _chunkIndex = 0;
            _sectionIndex = 0;
        }
        private void ShuffleChunks()
        {
            foreach (Chunk chunk in _level)
            {
                chunk.Shuffle();
            }
        }

        // // // EDITOR UTILITIES // // //

        [Button("Generate", EButtonEnableMode.Editor)]
        private void GenerateInEditor()
        {
            IndexReset();
            _lastEndPos = _lvlStartSection.Find("EndPos").position;
            CheckCache();
            ShuffleChunks();
            Generate();
        }

        [Button("Reset", EButtonEnableMode.Editor)]
        private void SceneReset() // if section log is not empty, destroys logged sections and clears list
        {
            IndexReset();
            ClearSectionsCache();
        }
        private void ClearSectionsCache()
        {
            for (int i = transform.childCount -1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}