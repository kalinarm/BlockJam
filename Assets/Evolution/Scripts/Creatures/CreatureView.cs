using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class CreatureView : MonoBehaviour
    {
        public Animator animator;
        public Transform root;
        public SpriteRenderer[] toColor;
        
        public SpriteRenderer body;
        public SpriteRenderer mouth;
        public SpriteRenderer border;
        public SpriteRenderer[] eyes;
        public SpriteRenderer[] ears;
        public SpriteRenderer[] pupil;

        List<SpriteRenderer> toColorGenerated = new List<SpriteRenderer>();

        private void Start()
        {
            if( animator != null)
            {
                animator.speed = Random.Range(0.95f, 1.05f);
            }
        }
        public void Create(CreatureTraits traits, CreatureData data)
        {
            toColorGenerated.Clear();

            root.DeleteAllChilds();

            CreateObj(data.objBody, traits.bodyShape, true);
            CreateObj(data.objBorder, traits.tieShape, true);
            CreateObj(data.objHands, traits.handShape, true);
            CreateObj(data.objMouth, traits.mouthShape);
            CreateObj(data.objEyes, traits.eyeShape);
            CreateObj(data.objEars, traits.earsShape, true);

        }
        public void Refresh(CreatureTraits traits, CreatureData data)
        {
            Create(traits, data);
            /*body.sprite = PickSprite(data.spriteBody, traits.bodyShape);
            mouth.sprite = PickSprite(data.spriteMouth, traits.mouthShape);
            border.sprite = PickSprite(data.spriteBorder, traits.borderShape);

            var s = PickSprite(data.spriteEyes, traits.eyeShape);
            foreach (var item in eyes)
            {
                item.sprite = s;
            }

            s = PickSprite(data.spriteEars, traits.earsShape);
            foreach (var item in ears)
            {
                item.sprite = s;
            }*/

            foreach (var item in pupil)
            {
                item.color = traits.colorEye;
            }

            foreach (var item in toColor)
            {
                item.color = traits.colorAll;
            }
            foreach (var item in toColorGenerated)
            {
                item.color = traits.colorAll;
            }
        }

        public static Sprite PickSprite(Sprite[] array, int index)
        {
            if (array.Length == 0) return null;
            return array[index%array.Length];
        }
        public static GameObject PickObj(GameObject[] array, int index)
        {
            if (array.Length == 0) return null;
            return array[index % array.Length];
        }
        public GameObject CreateObj(GameObject[] array, int index, bool colorize = false)
        {
            GameObject prefab = PickObj(array, index);
            if (prefab == null) return null;
            var obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(root);
            obj.transform.localPosition = prefab.transform.localPosition;

            if (colorize)
            {
                CreaturePart p = obj.GetComponent<CreaturePart>();
                if (p != null)
                {
                    foreach (var item in p.toColorize)
                    {
                        toColorGenerated.Add(item);
                    }
                }
                else
                {
                    SpriteRenderer[] sr = obj.GetComponentsInChildren<SpriteRenderer>();
                    foreach (var item in sr)
                    {
                        toColorGenerated.Add(item);
                    }
                }
            }

            return obj;
        }
    }
}

