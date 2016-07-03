// BobMotion.cs - 07/03/2016
// Eric Policaro

using System;
using UnityEngine;

namespace FishBowl
{
    /// <summary>
    /// Simple y movement based on a provided curve.
    /// </summary>
    public class BobMotion : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("Bobbing animation")]
        private AnimationCurve _curve;

        [SerializeField]
        [Tooltip("Speed of the bob from top to bottom")]
        private float _speed = 1f;

        [SerializeField]
        [Tooltip("Maximum height above neutral Y")]
        private float _amplitude = 0.1f;

        [SerializeField]
        [Tooltip("Start bobbing with application start")]
        private bool _bobOnStart = false;

        /// <summary>
        /// Gets or Sets if this object is currently bobbing.
        /// </summary>
        public bool Bobbing { get; set; }

        void Start()
        {
            if(_curve == null)
                throw new Exception("No bob animation curve assigned (Curve)");

            Bobbing = _bobOnStart;
        }

        void OnValidate()
        {
            if (_curve == null)
                Debug.LogWarning("Assign an animation curve to control the bobbing motion.");
        }

        void Update()
        {
            if (Bobbing)
            {
                float sample = _curve.Evaluate(Time.time) * _speed;
                float y = Mathf.Lerp(-1, 1, sample) * _amplitude;
                SetLocalY(y);
                _lastY = y;
            }
            else
            {
                SetLocalY(_lastY);
            }
        }
        private float _lastY;

        private void SetLocalY(float y)
        {
            Vector3 localPos = transform.localPosition;
            localPos.y = y;
            transform.localPosition = localPos;
        }
    }
}