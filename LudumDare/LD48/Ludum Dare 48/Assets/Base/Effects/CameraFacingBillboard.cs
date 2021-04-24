using UnityEngine;

namespace Libs.Base.Effects
{
    public class CameraFacingBillboard : MonoBehaviour
    {
        public Camera m_Camera;

        private void Start()
        {
            m_Camera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);
        }
    }
}