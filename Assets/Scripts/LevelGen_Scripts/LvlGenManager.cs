using Assets.Scripts.CameraBehaviour;
using Assets.Scripts.Misc;  // im working on a util library, at the moment theres not a lot but you can call them thru FlexUtils.Foo()
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.LevelGen_Scripts
{
    public class LvlGenManager : MonoBehaviour
    {
        private const float MIN_DISTANCE_TO_SPAWN_TRIGGER = 20f;

        [SerializeField]
        private Transform _lvlStartSection;
        [SerializeField]
        private Chaser _chaser;
        [SerializeField]
        private int _preSpawnLevelParts = 4;

        private List<LevelSection> _generatedSectionsLog = new List<LevelSection>();

        [SerializeField]
        private List<Chunk> _level;

        private Vector3 _lastEndPos;

        [SerializeField]
        private int _chunkIndex = 0;
        [SerializeField]
        private int _sectionIndex = 0;


        private void Awake()
        {
            Setup();
        }
        private void Update()
        {
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
                            GenerateSectionAt(_chunkIndex, _sectionIndex);
                            _sectionIndex++;                   
                        }
                        _sectionIndex = 0;
                        _chunkIndex++;
                    }
                    _chaser.SetCanMove(false);
                    Debug.Log("All Level Sections Generated");
                }
            }
            
        }
        private void GenerateSectionAt(int chunk, int section)
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
        //private void Prespawn(int amount)
        //{
        //    for (int i = 0; i < amount; i++)
        //    {
        //        GenerateSectionAt(RandomIndex(_levelPartList));
        //    }
        //}  

        private void LogCheck()
        {
            if (Application.isEditor)                                               // if using the button to spawn in editor, add spawned sections to log
            {
                if (_generatedSectionsLog.Count > 0)                                // if parts already generated, regenrate parts
                {
                    EditorReset();                    
                }
            }
        } // check if section log exists on Generate Call

        private bool ShouldSpawn()
        {
            return Vector3.Distance(_chaser.transform.position, _lastEndPos) < MIN_DISTANCE_TO_SPAWN_TRIGGER;
        }

        private int RandomIndex(List<Transform> list)
        {
            return FlexUtils.RandInt(0, list.Count);
        }

        // // // EDITOR UTILITIES // // //
        
        [Button("Generate", EButtonEnableMode.Editor)]
        private void Setup() // gets lvl start position and spawns an amount of sections (can be called in editor with button)
        {
            _chunkIndex = 0;
            _sectionIndex = 0;
            _lastEndPos = _lvlStartSection.Find("EndPos").position; // anchor LastEndPos to Lvl_Start Section (Section must be active in scene)
            LogCheck();                                             // only called in editor
            ShuffleSections();                                      // should be called before any generation
            //Prespawn(_preSpawnLevelParts);                          // prespwn initial lvl parts
        }
        /// <summary>
        /// Re - shuffles the order of level sections foreach chunk in _level
        /// </summary>
        private void ShuffleSections()
        {
            foreach (Chunk chunk in _level)
            {
                chunk.Shuffle();
            }
        } 

        [Button("Reset", EButtonEnableMode.Editor)]
        private void EditorReset() // if section log is not empty, destroys logged sections and clears list
        {
            if(_generatedSectionsLog.Count > 0)                         
            {
                for (int i = 0; i < _generatedSectionsLog.Count; i++)
                {
                    DestroyImmediate(_generatedSectionsLog[i].gameObject);
                }
            }
            _generatedSectionsLog.Clear();
        }
    }
}