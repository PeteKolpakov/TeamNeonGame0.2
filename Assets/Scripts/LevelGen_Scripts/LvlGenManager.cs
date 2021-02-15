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
        private List<Transform> _levelPartList;
        [SerializeField]
        private Transform _chaser;
        [SerializeField]
        private int _preSpawnLevelParts = 4;

        private List<LevelSection> _generatedSectionsLog = new List<LevelSection>();

        [SerializeField]
        private List<Chunk> _chunks;

        [SerializeField]
        private Chunk ElChunk;

        private Vector3 _lastEndPos;



        private void Awake()
        {
            Setup();
        }
        private void Update()
        {
            if (Vector3.Distance(_chaser.position, _lastEndPos) < MIN_DISTANCE_TO_SPAWN_TRIGGER)   // Spawn new Section when Chaser is close enough
            {
                GenerateSectionAt(RandomIndex(_levelPartList));
            }            
        }

        private void GenerateNextSection()
        {
            
        }

        private void GenerateSectionAt(int index)
        {
            Transform selectedSectionPrefab = _levelPartList[index];                                     // Select next Section from list
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
            levelSection.Setup(_chaser);
            return levelSection;                                                                            // return position it spawned at
        }
        private void Prespawn(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GenerateSectionAt(RandomIndex(_levelPartList));
            }
        }  

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

        private int RandomIndex(List<Transform> list)
        {
            return FlexUtils.RandInt(0, list.Count);
        }

        // // // EDITOR UTILITIES // // //
        
        [Button("Generate", EButtonEnableMode.Editor)]
        private void Setup() // gets lvl start position and spawns an amount of sections (can be called in editor with button)
        {
            _lastEndPos = _lvlStartSection.Find("EndPos").position;                 // anchor LastEndPos to Lvl_Start Section (Section must be active in scene)
            LogCheck();
            Prespawn(_preSpawnLevelParts);                                          // prespwn initial lvl parts
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

        [Button("Shuffle Chunk", EButtonEnableMode.Editor)]
        private void ShuffleChunk()
        {
            ElChunk.Shuffle();
        }
    }
}