using UnityEngine;

public class KeepPosition : MonoBehaviour
{
    public Transform Other;
    public bool KeepZRotation;
    private float defaultFov;

    public ShipControls Ship { get; private set; }

    private void Start()
    {
        defaultFov = Camera.main.fieldOfView;
        Ship = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipControls>();
    }

    private void Update()
    {
        transform.position = Other.position;

        if (KeepZRotation)
        {
            var rotation = transform.localRotation.eulerAngles;
            rotation.z = Other.localRotation.eulerAngles.z;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotation), 0.8f);

            Camera.main.fieldOfView = defaultFov 
                + Ship.Velocity.magnitude * 0.6f * Mathf.Sign(Vector3.Dot(transform.forward, Ship.Velocity)); 

            //Debug.LogFormat("{0} / {1}", transform.localEulerAngles, Other.localEulerAngles);
        }
    }
}
