using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx
{
    public static class RectTransformExtensions
    {
        public static void SetFullSize(this RectTransform self)
        {
            self.ResetLocal();
            self.anchorMin = Vector2.zero;
            self.anchorMax = Vector2.one;
            self.offsetMin = Vector2.zero;
            self.offsetMax = Vector2.zero;
        }
    }
}
