﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Misc;

namespace Assets.Scripts.LevelGen_Scripts
{
    /// <summary>
    /// A Collection of Lvl Sections
    /// </summary>
    [Serializable]
    struct Chunk
    {
        public string name;
        public int GenerationRounds;
        public List<LevelSection> sections;

        private List<LevelSection> _spawnOrder;

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

            //foreach (LevelSection c in sections)
            //{
            //    _spawnOrder.Add(c);
            //}
        }
    }
}
