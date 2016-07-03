// BoundingBox.cs - 07/03/2016
// Eric Policaro

using UnityEngine;

namespace FishBowl
{
    /// <summary>
    /// Simple bounding box for constraining movement
    /// </summary>
    public class BoundingBox : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("Center point of the box relative to the transform's position")]
        private Vector3 _center = Vector3.zero;

        [SerializeField]
        [Tooltip("Size of the bounding box")]
        private Vector3 _size = Vector3.one;

        /// <summary>
        /// Gets the extents of the bounding box (half the size)
        /// </summary>
        public Vector3 Extents { get { return _size * 0.5f; } }

        /// <summary>
        /// Gets the world center position of the bounding box
        /// </summary>
        public Vector3 Center
        {
            get { return transform.position + _center; }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Center, _size);
            Gizmos.color = Color.white;
        }
    }
}