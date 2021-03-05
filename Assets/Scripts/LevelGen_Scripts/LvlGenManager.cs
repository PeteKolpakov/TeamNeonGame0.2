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

        private List<LevelSection> _generatedSectionsLog = new List<LevelSection>(); // used for tracking generated sections in editor

        private Vector3 _lastEndPos;

        [SerializeField] [ReadOnly] private int _chunkIndex = 0;
        [SerializeField] [ReadOnly] private int _sectionIndex = 0;


        private void Awake()
        {
            Setup();
        }
        private void Update()
        {
            Generate();
        }

        private void Setup()
        {
            IndexReset();
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
                }
            }
        }
        private void GenerateSectionAtIndex(int chunk, int section)
        {
            Transform selectedSectionPrefab = _level[chunk].sections[section].transform;                                     // Select next Section from list
            LevelSection lastSpawnedSectionTransform = GenerateSection(selectedSectionPrefab, _lastEndPos); // Spawn Selected Section at lastEndPos, Store Spawned Section Transform
            _lastEndPos = lastSpawnedSectionTransform.EndPosition.position;                              // Update last used EndPos
            if (Application.isEditor)                                                       // if using the button to spawn in editor, add spawned sections to log
            {
                _generatedSectionsLog.Add(lastSpawnedSectionTransform);                     // log generated sections for later removal
            }
        }
        private LevelSection GenerateSection(Transform levelPrefab, Vector3 spawnPosition)

        {
            Transform sectionTransform = Instantiate(levelPrefab, spawnPosition, Quaternion.identity);         // Spawn Lvl Section
            LevelSection levelSection = sectionTransform.GetComponent<LevelSection>();
            levelSection.Setup(_chaser.transform);
            return levelSection;                                                                            // return position it spawned at
        }

        //private void ChaserSpeedRefresh()
        //{
        //    _chaser.SetSpeed(_level[_chunkIndex].chaserSpeed);
        //}

        private void LogCheck()
        {
            if (_generatedSectionsLog.Count > 0)
            {
                SceneReset();
            }
        } // TODO : check if section log exists on Generate Call
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
            LogCheck();
            ShuffleChunks();
            Generate();

        }

        [Button("Reset", EButtonEnableMode.Editor)]
        private void SceneReset() // if section log is not empty, destroys logged sections and clears list
        {
            IndexReset();
            InitializeLog();
            _generatedSectionsLog.Clear();
        }
        private void InitializeLog()
        {
            if (_generatedSectionsLog.Count > 0)
            {
                for (int i = 0; i < _generatedSectionsLog.Count; i++)
                {
                    DestroyImmediate(_generatedSectionsLog[i].gameObject);
                }
            }
        }
    }
}