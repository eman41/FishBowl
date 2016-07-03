// SwimWiggle.cs - 07/03/2016
// Eric Policaro

using UnityEngine;

namespace FishBowl
{
    /// <summary>
    /// Simple fish swimming animation (y axis rotation).
    /// </summary>
    public class SwimWiggle : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("Curve controlling the back and forth motion.")]
        private AnimationCurve _curve;

        [SerializeField]
        [Tooltip("Back and forth speed of the wiggle")]
        private float _speed = 1.5f;

        [SerializeField]
        [Range(0f, 90f)]
        [Tooltip("Maximum angle off center to rotate")]
        private float _maxAngle = 20f;

        void Update()
        {
            float sample = _curve.Evaluate(Time.time * _speed);
            float angle = _maxAngle * Mathf.Lerp(-1, 1, sample);

            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}