// FishController.cs - 07/03/2016
// Eric Policaro

using UnityEngine;

namespace FishBowl
{
    /// <summary>
    /// Fish movement controller. 
    /// If a follow target is assigned, the fish will attempt to look
    /// at and catch the target as it swims.
    /// </summary>
    public class FishController : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("Target to follow")]
        private Transform _follow;

        [SerializeField]
        [Tooltip("Acceleration when following a target")]
        private float _catchupAccel = 0.1f;

        [SerializeField]
        [Range(0f, 1f)]
        [Tooltip("Modifies the acceleration to slow down the fish as it reaches the target")]
        private float _brakeFactor = 0.25f;

        [SerializeField]
        [Tooltip("Distance from the target before the fish begins swimming")]
        private float _catchupDistance = 0.5f;

        private float _currentSpeed;
        private float _uprightVel;
        private BobMotion _bobbing;

        void Start()
        {
            _bobbing = GetComponentInChildren<BobMotion>();
        }

        void OnDrawGizmos()
        {
            if (_follow != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, _follow.position);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, _catchupDistance);

                Gizmos.color = Color.white;
            }
        }

        void Update()
        {
            if (_follow == null)
            {
                _bobbing.Bobbing = true;
                return;
            }
            
            if (AtGoal)
            {
                if (Moving)
                {
                    SlowDown();
                }
                else
                {
                    RotateUpright();
                    _bobbing.Bobbing = true;
                }
            }
            else
            {
                if (Moving)
                {
                    LookAtGoal();
                    _bobbing.Bobbing = false;
                }

                if (DistanceToGoal >= _catchupDistance)
                {
                    _currentSpeed += _catchupAccel;
                }

                transform.position = Vector3.MoveTowards(
                    transform.position, _follow.position, _currentSpeed * Time.deltaTime);
            }
        }

        private void LookAtGoal()
        {
            Vector3 dir = (_follow.position - transform.position).normalized;
            var look = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, look, 5f);
        }

        private void SlowDown()
        {
            _currentSpeed -= _catchupAccel * _brakeFactor;
            _currentSpeed = Mathf.Max(0f, _currentSpeed);
        }

        private void RotateUpright()
        {
            float toward = transform.eulerAngles.x > 180 ? 360f : 0f;
            float x = Mathf.SmoothDamp(
                transform.localEulerAngles.x, toward, ref _uprightVel, 0.5f);

            Vector3 euler = transform.localEulerAngles;
            euler.x = x;
            transform.localRotation = Quaternion.Euler(euler);
        }

        private bool Moving
        {
            get { return _currentSpeed > 0f; }
        }

        private bool AtGoal
        {
            get { return Mathf.Approximately(DistanceToGoal, 0f); }
        }

        private float DistanceToGoal
        {
            get { return Vector3.Distance(transform.position, _follow.position); }
        }
    }
}