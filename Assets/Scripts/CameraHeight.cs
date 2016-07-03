// CameraHeight.cs - 07/03/2016
// Eric Policaro

namespace FishBowl
{
    /// <summary>
    /// Available camera rig positions along the Y axis.
    /// </summary>
    public enum CameraHeight
    {
        /// <summary>
        /// Place camera at the top of the bounding volume.
        /// </summary>
        Above,

        /// <summary>
        /// Place camera at the center of the bounding volume.
        /// </summary>
        Level,

        /// <summary>
        /// Place camera at the bottom of the bounding volume.
        /// </summary>
        Below
    }
}