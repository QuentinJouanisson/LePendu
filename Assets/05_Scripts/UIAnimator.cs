using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using JetBrains.Annotations;

namespace PenduAnimation
{
    public class UIAnimator : MonoBehaviour
    {
        [System.Serializable]
        public class AnimatedUIElement
        {
            public RectTransform rectTransform;
            public Vector2 offscreenOffset = new Vector2(0, -1000);
            [HideInInspector] public Vector2 basePosition;
            [HideInInspector] public Vector2 newPosition;
        }

        [Header("Animation Settings")]

        [SerializeField] private float animationDuration = 0.5f;
        [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Animated Elements")]

        public List<AnimatedUIElement> uiElements = new();

        private void Awake()
        {
            foreach (var element in uiElements)
            {
                if (element.rectTransform != null)
                {
                    element.basePosition = element.rectTransform.anchoredPosition;
                }
            }
        }
        

        public void DrawInGameObjects()
        {
            foreach(var element in uiElements)
            {
                if (element.rectTransform != null)
                StartCoroutine(AnimateToPosition(element.rectTransform, element.basePosition, deactivateAfter: false));
            }
        }
        public void DrawOutGameObjects()
        {
            foreach(var element in uiElements)
            {
                if (element.rectTransform != null)
                StartCoroutine(AnimateToPosition(element.rectTransform, element.basePosition + element.offscreenOffset, deactivateAfter: true));
            }
        }
        private IEnumerator AnimateToPosition(RectTransform target, Vector2 targetPos, bool deactivateAfter)
        {
            Vector2 startPos = target.anchoredPosition;
            float time = 0f;

            while (time<animationDuration)
            {
                float t = time / animationDuration;
                t = t * t * (3f -2f * t);
                target.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
                time += Time.unscaledDeltaTime;
                yield return null;
            }
            target.anchoredPosition = targetPos;

            if (deactivateAfter)
            {
                target.gameObject.SetActive(false);
            }
        }
    }
}        