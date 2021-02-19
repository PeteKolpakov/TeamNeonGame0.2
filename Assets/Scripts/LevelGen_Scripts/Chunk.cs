using System;
using System.Collections.Generic;
using Assets.Scripts.Misc;
using UnityEngine;

namespace Assets.Scripts.LevelGen_Scripts
{
    /// <summary>
    /// A Collection of Lvl Sections
    /// </summary>
    [Serializable]
    struct Chunk
    {
        public string name;
        //[Range(0, 5f)]
        //public float chaserSpeed; // TODO : implement chaser speed changes to lvl gen manager
        public List<LevelSection> sections;       

        public void Shuffle()
        {
            int listCount = sections.Count;
            while (listCount > 1)
            {
                int k = FlexUtils.RandInt(0 , listCount - 1);
                listCount--;
                LevelSection temp = sections[listCount];
                sections[listCount] = sections[k];
                sections[k] = temp;
            }
        }
    }
}
