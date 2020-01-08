using UnityEngine;

namespace Libs.Base.Input
{
    /// MouseLook rotates the transform based on the mouse delta.
    /// Minimum and Maximum values can be used to constrain the possible rotation

    /// To make an FPS style character:
    /// - Create a capsule.
    /// - Add a rigid body to the capsule
    /// - Add the MouseLook script to the capsule.
    ///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
    /// - Add FPSWalker script to the capsule

    /// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
    /// - Add a MouseLook script to the camera.
    ///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
    [AddComponentMenu("Camera-Control/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        public float RotationSpeed;
        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -360F;
        public float maximumX = 360F;

        public float minimumY = -60F;
        public float maximumY = 60F;

        float rotationX = 0F;
        float rotationY = 0F;

        Quaternion originalRotation;

        public ShipControls Ship { get; private set; }

        void Update()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (axes == RotationAxes.MouseXAndY)
            {
                // Read the mouse input axis
                rotationX += UnityEngine.Input.GetAxis("Mouse X") * sensitivityX * PlayerPrefs.GetInt("invert x", 1);
                rotationY += UnityEngine.Input.GetAxis("Mouse Y") * sensitivityY * PlayerPrefs.GetInt("invert y", 1);

                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
                Quaternion zQuaternion = Ship != null
                    ? Ship.Rotation
                    : Quaternion.identity;

                transform.localRotation = originalRotation * xQuaternion * yQuaternion;
                if (Ship != null)
                {
                    transform.localRotation *= zQuaternion;
                }
            }
            else if (axes == RotationAxes.MouseX)
            {
                rotationX += UnityEngine.Input.GetAxis("Mouse X") * sensitivityX;
                rotationX = ClampAngle(rotationX, minimumX, maximumX);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation = originalRotation * xQuaternion;
            }
            else
            {
                rotationY += UnityEngine.Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
                transform.localRotation = originalRotation * yQuaternion;
            }
        }

        void Start()
        {
            // Make the rigid body not change rotation
            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().freezeRotation = true;
            originalRotation = transform.localRotation;

            Ship = GetComponent<ShipControls>();
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}