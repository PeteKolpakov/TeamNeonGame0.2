using System;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    static class FlexUtils
    {
        /// <summary>
        /// Will only call the method when in Editor Mode,
        /// Function Must return Void
        /// </summary>
        /// <param Method="f"></param>
        public static void OnlyRunInEditor(Action method)
        {
            if (Application.isEditor)
            {
                method();
            }
        }

        /// <summary>
        /// Gives random int from and to the set range inclusively
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static int RandInt(int from, int to)
        {
            return UnityEngine.Random.Range(from, to - 1);
        }
        /// <summary>
        /// 50/50 true or false
        /// </summary>
        /// <returns></returns>
        public static bool Coinflip()
        {
            int counter = UnityEngine.Random.Range(0, 2);
            if (counter == 0)
            {
                return false;
            }
            else if(counter == 1)
            {
                return true;
            }
            else
            {
                Debug.LogError("ERROR: Coinflip has registered non bianary values, returning default value");
                return false;
            }

        }
    }

}
