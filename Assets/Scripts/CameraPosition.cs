// CameraPosition.cs - 07/03/2016
// Eric Policaro

namespace FishBowl
{
    /// <summary>
    /// Available camera rig positions along the XZ plane.
    /// </summary>
    public enum CameraPosition
    {
        /// <summary>
        /// Place camera on the +X, +Z edge of the bounding volume.
        /// </summary>
        FrontRight,

        /// <summary>
        /// Place camera on the -X, +Z edge of the bounding volume.
        /// </summary>
        FrontLeft,

        /// <summary>
        /// Place camera on the +X, -Z edge of the bounding volume.
        /// </summary>
        BackRight,

        /// <summary>
        /// Place camera on the -X, -Z edge of the bounding volume.
        /// </summary>
        BackLeft,
    }
}