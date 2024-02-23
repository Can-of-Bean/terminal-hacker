using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScreenShader
{
    public class ScreenShaderController : MonoBehaviour
    {
        private static readonly int s_property = Shader.PropertyToID("_DistortionStrength");

        [SerializeField]
        private MeshRenderer m_screenMesh = null!;

        /// <summary>
        /// Should the static effect occur.
        /// </summary>
        [field: SerializeField]
        public bool UseDistortionStatic { get; set; }

        /// <summary>
        /// The minimum and maximum interval between bouts of distortion
        /// </summary>
        [field: SerializeField]
        public Vector2 DistortionIntervalSeconds { get; set; }

        /// <summary>
        /// The minimum and maximum scale that can be applied to the length of the distortion
        /// </summary>
        [field: SerializeField]
        public Vector2 DistortionLengthSeconds { get; set; }

        /// <summary>
        /// A curve scaling from 0 to 1 the distortion amount. Will be multiplied by <see cref="DistortionScale"/>.
        /// </summary>
        [field: SerializeField]
        public AnimationCurve DistortionCurve { get; set; } = null!;

        /// <summary>
        /// Scales the distortion amount from <see cref="DistortionCurve"/>.
        /// </summary>
        [field: SerializeField]
        public float DistortionScale { get; set; }

        private void Start()
        {
            StartCoroutine(DistortionWait());
        }

        private IEnumerator DistortionWait()
        {
            while (Application.isPlaying)
            {
                float secondsToWait = Random.Range(DistortionIntervalSeconds.x, DistortionIntervalSeconds.y);
                yield return new WaitForSeconds(secondsToWait);

                if (UseDistortionStatic)
                    yield return DistortionInvoke();
            }
        }

        private IEnumerator DistortionInvoke()
        {
            float lengthMultiplier = Random.Range(DistortionLengthSeconds.x, DistortionLengthSeconds.y);

            Keyframe lastKey = DistortionCurve.keys[DistortionCurve.length - 1];
            float startTime = Time.time;
            float endTime = Time.time + lastKey.time;

            // apply length multiplier to length and re-create end time including it
            float length = (endTime - startTime) * lengthMultiplier;
            endTime = startTime + length;

            while (Time.time < endTime)
            {
                // set distortion
                float curveValue = DistortionCurve.Evaluate(Time.time - startTime);
                float desiredDistortion = curveValue * DistortionScale;
                SetDistortionAmount(desiredDistortion);

                // wait a frame
                yield return null;
            }

            SetDistortionAmount(0);
        }

        private void SetDistortionAmount(float amount)
        {
            m_screenMesh.material.SetFloat(s_property, amount);
        }
    }
}
