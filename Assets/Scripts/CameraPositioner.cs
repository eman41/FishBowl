// CameraBounds1.cs - 07/03/2016
// Eric Policaro

using System;
using UnityEngine;

namespace FishBowl
{
    /// <summary>
    /// Valid camera positions for a camera viewing rig. 
    /// Foward is positive z, right is positive x.
    /// </summary>
    [RequireComponent(typeof(BoundingBox))]
    public class CameraPositioner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Vertical camera position")]
        private CameraHeight _height = CameraHeight.Above;

        [SerializeField]
        [Tooltip("Camera placement on the rig bounds.")]
        private CameraPosition _position = CameraPosition.FrontRight;

        /// <summary>
        /// Cycle to the next vertical (Y) camera position.
        /// </summary>
        public void CycleHeight()
        {
            _height = _heights[_heightIdx];

            int next = (_heightIdx + _cycleDir);
            if (next == _heights.Length)
            {
                _cycleDir = -1;
            }
            else if (next < 0)
            {
                _cycleDir = 1;
            }

            _heightIdx += _cycleDir;
        }
        private int _heightIdx = 1;
        private int _cycleDir = 1;
        private CameraHeight[] _heights;

        /// <summary>
        /// Sets the active camera rig position.
        /// </summary>
        /// <param name="pos">Position to set</param>
        public void SetPosition(CameraPosition pos)
        {
            _position = pos;
        }

        /// <summary>
        /// Gets the active rig world position.
        /// </summary>
        /// <returns>World position vector</returns>
        public Vector3 GetCurrentPosition()
        {
            return GetPosition(_position);
        }

        /// <summary>
        /// Gets the world position of a given rig position.
        /// </summary>
        /// <param name="pos">Camera position</param>
        /// <returns>World position vector</returns>
        public Vector3 GetPosition(CameraPosition pos)
        {
            switch (pos)
            {
                case CameraPosition.FrontRight:
                    return FrontRight;

                case CameraPosition.FrontLeft:
                    return FrontLeft;

                case CameraPosition.BackRight:
                    return BackRight;

                default:
                    return BackLeft;
            }
        }

        void OnEnable()
        {
            _heights = (CameraHeight[])Enum.GetValues(typeof(CameraHeight));
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Center, Extents * 2f);
            Gizmos.DrawSphere(FrontLeft, 0.1f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(FrontRight, 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(BackLeft, 0.1f);

            Gizmos.color = Color.white;
            Gizmos.DrawSphere(BackRight, 0.1f);
        }

        private Vector3 FrontRight
        {
            get
            {
                var flip = Vector3.Scale(Extents, new Vector3(1f, 0f, 1f));
                return Center + flip + Height();
            }
        }

        private Vector3 FrontLeft
        {
            get
            {
                var flip = Vector3.Scale(Extents, new Vector3(-1f, 0f, 1f));
                return Center + flip + Height();
            }
        }

        private Vector3 BackLeft
        {
            get
            {
                var flip = Vector3.Scale(Extents, new Vector3(1f, 0f, -1f));
                return Center + flip + Height();
            }
        }

        private Vector3 BackRight
        {
            get
            {
                var flip = Vector3.Scale(Extents, new Vector3(-1f, 0f, -1f));
                return Center + flip + Height();
            }
        }

        private Vector3 Extents { get { return Bounds.Extents; } }

        private Vector3 Center { get { return Bounds.Center; } }

        private Vector3 Height()
        {
            switch (_height)
            {
                case CameraHeight.Above:
                    return new Vector3 { y = Extents.y };

                case CameraHeight.Level:
                    return Vector3.zero;

                default:
                    return new Vector3 { y = -Extents.y };
            }
        }

        private BoundingBox _boundingBox;
        private BoundingBox Bounds
        {
            get
            {
                if (_boundingBox == null)
                    _boundingBox = GetComponent<BoundingBox>();

                return _boundingBox;
            } 
        }
    }
}