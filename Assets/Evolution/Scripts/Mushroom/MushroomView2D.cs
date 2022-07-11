using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public class MushroomView2D : MushroomView
    {
        [Header("refs")]
        public SpriteRenderer[] body;
        public SpriteRenderer hat;
        public SpriteRenderer points;
        public SpriteRenderer feet;
        public SpriteRenderer inside;
        public SpriteRenderer background;

        public override void Refresh(MushroomTraits traits, MushroomData data)
        {
            SetColor(hat, traits.colorHat);
            SetColor(points, traits.colorPoints);
            SetColor(feet, traits.colorFeet);
            SetColor(inside, traits.colorInside);

            if (background != null)
            {
                background.sprite = CreateSprite(traits.colorBackgroundA, traits.colorBackgroundB, traits.colorBackgroundC, 1024, 1024, 0.5f);
            }
        }

        void SetColor(SpriteRenderer sr, Color c)
        {
            if (sr == null) return;
            sr.color = c;
        }
        void SetColor(SpriteRenderer[] sr, Color c)
        {
            foreach (var item in sr)
            {
                item.color = c;
            }
        }

        public static Sprite CreateSprite(Color cA, Color cB, Color cC, int width = 512, int height = 512, float ratio = 0.5f, float margin = 0.1f)
        {
            Gradient g = new Gradient();
            var keyA = new GradientColorKey(cA, -margin);
            var keyB = new GradientColorKey(cB, ratio);
            var keyC = new GradientColorKey(cC, 1f + margin);
            g.colorKeys = new GradientColorKey[] { keyA, keyB, keyC };
            //g.alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(0f, 0f) };
            g.mode = GradientMode.Blend;
            var texture = Create(g, width, height);
            var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            return sprite;
        }

        public static Texture2D Create(Gradient grad, int width = 32, int height = 1)
        {
            var gradTex = new Texture2D(width, height, TextureFormat.ARGB32, false);
            gradTex.filterMode = FilterMode.Bilinear;
            float inv = 1f / (width - 1);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var t = x * inv;
                    Color col = grad.Evaluate(t);
                    gradTex.SetPixel(x, y, col);
                }
            }
            gradTex.Apply();
            return gradTex;
        }
    }
}

