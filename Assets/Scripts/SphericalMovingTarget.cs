// SphericalMovingTarget.cs - 07/03/2016
// Eric Policaro

using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FishBowl
{
    /// <summary>
    /// Moves a target a random fashion inside a sphereical bounding volume.
    /// </summary>
    public class SphericalMovingTarget : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("Spherical radius of the available random area")]
        private float _radius = 1f;

        [SerializeField]
        [Tooltip("Target object to move")]
        private Transform _mover;

        [SerializeField]
        [Tooltip("Speed the target object moves to its next position")]
        private float _speed = 1f;

        [SerializeField]
        [Tooltip("Maximum time before assigning the next position (secs)")]
        private float _maxWait = 3f;

        [SerializeField]
        [Tooltip("Minimum time before assigning the next position (secs)")]
        private float _minWait = 2f;

        private Vector3 _nextSpot;
        private bool _waiting;

        void Start()
        {
            if (_mover == null)
            {
                Debug.LogError("No mover assigned to act as the target, creating one");
                var go = new GameObject("Mover");
                go.transform.SetParent(transform);
                go.transform.localPosition = Vector3.zero;
                _mover = go.transform;
            }

            _nextSpot = PickRandomSpot();
        }

        void OnValidate()
        {
            if(_mover == null)
                Debug.LogWarning("Assign a child GameObject to Mover");
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
            if (_mover != null)
                Gizmos.DrawSphere(_mover.position, 0.02f);

            if (Application.isPlaying)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(transform.position + _nextSpot, 0.02f);
            }

            Gizmos.color = Color.white;
        }

        void Update()
        {
            _mover.transform.localPosition = Vector3.MoveTowards(
                _mover.transform.localPosition, _nextSpot, _speed * Time.deltaTime);

            if (!_waiting && CloseToGoal())
            {
                _waiting = true;
                StartCoroutine(WaitForNext());
            }
        }

        IEnumerator WaitForNext()
        {
            float wait = Random.Range(_minWait, _maxWait);
            Vector3 next = PickRandomSpot();
            
            yield return new WaitForSeconds(wait);
            
            _nextSpot = next;
            _waiting = false;
        }

        private bool CloseToGoal()
        {
            float distance = Vector3.Distance(_mover.localPosition, _nextSpot);
            return distance <= 0.01f;
        }

        private Vector3 PickRandomSpot()
        {
            Vector3 random = Random.insideUnitSphere;
            return (random * _radius);
        }
    }
}