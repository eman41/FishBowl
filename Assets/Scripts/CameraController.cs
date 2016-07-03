// CameraController.cs - 07/03/2016
// Eric Policaro

using System;
using UnityEngine;

namespace FishBowl
{
    /// <summary>
    /// Control camera movement within a predefined bounding area.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(CameraPositioner))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] 
        [Tooltip("Main Camera transform")]
        private Transform _cameraPivot;

        [SerializeField]
        [Tooltip("Maximum speed to move the camera when repositionoing")]
        private float _maxSpeed = 4f;
        
        [SerializeField]
        [Tooltip("Time to smooth the camera as it approaches its next position")]
        private float _smoothTime = 0.4f;

        [SerializeField]
        [Tooltip("Linear velocity (units/s)")]
        private Vector3 _moveSpeed = Vector3.zero;

        void Start()
        {
            if (_cameraPivot == null)
                throw new Exception("No camera transform attached.");
        }

        void OnValidate()
        {
            if (_cameraPivot == null)
                Debug.LogWarning("Connect the Main Camera to the Camera Pivot field.");
        }

        void Update()
        {
            PollInput();
            UpdateCameraPosition();
        }

        private void PollInput()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                Positioner.CycleHeight();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                Positioner.SetPosition(CameraPosition.FrontRight);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                Positioner.SetPosition(CameraPosition.FrontLeft);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                Positioner.SetPosition(CameraPosition.BackRight);
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
                Positioner.SetPosition(CameraPosition.BackLeft);
        }

        private Vector3 _vel;
        [ContextMenu("Update Camera Position")]
        public void UpdateCameraPosition()
        {
            if(Positioner == null)
                return;

            if (Application.isPlaying)
            {
                _cameraPivot.position = Vector3.SmoothDamp(
                    _cameraPivot.position,
                    Positioner.GetCurrentPosition(), ref _vel, _smoothTime, _maxSpeed);
            }
            else
                _cameraPivot.position = Positioner.GetCurrentPosition();

            if (_moveSpeed.sqrMagnitude > 0f)
                transform.position += _moveSpeed * Time.deltaTime;    
            
            _cameraPivot.LookAt(transform.position);
        }

        private CameraPositioner _positioner;
        private CameraPositioner Positioner
        {
            get
            {
                if (_positioner == null)
                    _positioner = GetComponent<CameraPositioner>();

                return _positioner;
            }
        }
    }
}