using System.Collections;
using UnityEngine;

namespace PenduAnimation
{
    

    public class AnimatePenduParts : MonoBehaviour        
    {
        [SerializeField] float duration = 0.3f;
        public void AnimatePenduPart (GameObject part)
        {
            part.SetActive(true);
            StartCoroutine(AnimatePartCoroutine(part.transform));
        }

        IEnumerator AnimatePartCoroutine(Transform t)
        {
            float elapsed = 0f;

            Vector3 initialScale = Vector3.zero;
            Vector3 finalScale = Vector3.one;

            //float maxAngle = 30f;
            //float damping = 4f;
            //float frequency = 8f;

            //t.localScale = initialScale;
            //while(elapsed < duration)
            //{
            //    float t01 = elapsed / duration;
            //    t.localScale = Vector3.Lerp(initialScale, finalScale, Mathf.SmoothStep(0, 1, t01));
            //    float angle = maxAngle * Mathf.Exp(-damping * t01 ) * Mathf.Cos(frequency * t01 * Mathf.PI * 2 );
            //    t.localRotation = Quaternion.Euler( 0, 0, angle );
            //    elapsed += Time.deltaTime;
            //    yield return null;  
            //}
            //t.localScale = finalScale;
            //t.localRotation = Quaternion.Euler(0, 0, 0f);

            //elapsed = 0f;

            float initialRotation = 30f;
            float finalRotation = 0f;

            t.localScale = initialScale;
            t.localRotation = Quaternion.Euler(0, 0, initialRotation);

            while (elapsed < duration)
            {
                float t01 = elapsed / duration;
                t.localScale = Vector3.Lerp(initialScale, finalScale, Mathf.SmoothStep(0, 1, t01));
                float zRot = Mathf.Lerp(initialRotation, finalRotation, Mathf.SmoothStep(0, 1, t01));
                t.localRotation = Quaternion.Euler(0, 0, zRot);

                elapsed += Time.deltaTime;
                yield return null;
            }
            t.localScale = finalScale;
            t.localRotation = Quaternion.Euler(0, 0, finalRotation);
        }        
    }
}
