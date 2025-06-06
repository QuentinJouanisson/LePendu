using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
namespace LayerAnimation
{
    public class ParallaxLayerAnimator : MonoBehaviour
    {
        [SerializeField] private Transform[] layers;
        [SerializeField] private float animationDuration = 2f;
        [SerializeField] private Transform endTarget;
        //[SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        private Vector3 basePos, endPos;
        [SerializeField] private float delayFactor = 1f;        

        void Awake()
        {
            basePos = transform.position;
            endPos = endTarget.transform.position;

            layers = new Transform[transform.childCount];
            for (int i = 0; i < layers.Length; i++)
                layers[i] = transform.GetChild(i);            
        }
        private void Start()
        {            
            AnimateToStart(); //test to rmove
        }
        private void InitLayers(Vector3 target)
        {
            foreach (Transform layer in layers)
            layer.position = target;            
        }
        public void AnimateToEnd()
        {
            InitLayers(basePos);
            StartCoroutine(AnimatePosition(basePos, endPos));
        }
        public void AnimateToStart()
        {
            InitLayers(endPos);
            StartCoroutine(AnimatePosition(endPos, basePos));
        }

        private IEnumerator AnimatePosition(Vector3 startPos, Vector3 target)
        {                        
            float maxDelay = animationDuration / layers.Length;
            float delay = maxDelay * delayFactor;

            Transform[] orderedLayers = new Transform[layers.Length];
            System.Array.Copy(layers, orderedLayers, layers.Length);
            if(startPos != endPos)
            {
                System.Array.Reverse(orderedLayers);
            }
                

            foreach (Transform layer in orderedLayers)
            {                
                StartCoroutine(MoveLayer(layer, startPos, target, delay));
                //yield return null;
                yield return new WaitForSeconds(maxDelay);
            }
        }

        private IEnumerator MoveLayer(Transform layer, Vector3 start, Vector3 end, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                layer.position = Vector3.Lerp(start, end, EaseInOut(elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        float EaseInOut(float t)
        {
            return t < 0.5 ? 4 * t * t * t : (t - 1) * (2 * t - 2) * (2 * t - 2) + 1;
        }
    }
}
