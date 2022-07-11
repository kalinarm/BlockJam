using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    [CreateAssetMenu]
    public class CreatureData : ScriptableObject
    {
        public Phenotype phenotype;

        public GameObject[] objBody;
        public GameObject[] objMouth;
        public GameObject[] objBorder;
        public GameObject[] objHands;
        public GameObject[] objEars;
        public GameObject[] objEyes;

        public Sprite[] spriteBody;
        public Sprite[] spriteEyes;
        public Sprite[] spriteEars;
        public Sprite[] spriteMouth;
        public Sprite[] spriteBorder;

        public Color refColor = Color.red;

        public static Sprite PickSprite(Sprite[] array, int index)
        {
            if (array.Length == 0) return null;
            return array[index % array.Length];
        }

        public static GameObject Pick(GameObject[] array, int index)
        {
            if (array.Length == 0) return null;
            return array[index % array.Length];
        }
    }
}

