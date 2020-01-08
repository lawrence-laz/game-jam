using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CursorBehaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite cursor;
    [SerializeField]
    private Sprite crosshair;

    [Header("Animation")]
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float scaleDelta;
    [SerializeField]
    private float scaleSpeed;

    private Image image;

    public static Vector3 GetWorldPosition()
    {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;

        return position;
    }

    public void SetImageToCursor()
    {
        image.sprite = cursor;
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetImageToCrosshair()
    {
        image.sprite = crosshair;
    }

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        FollowMouse();
        Animate();
    }

    private void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }

    private void Animate()
    {
        if (image.sprite == crosshair)
        {
            transform.localScale = Vector3.one - (Vector3.one * Mathf.PingPong(Time.time * scaleSpeed, scaleDelta));
            Quaternion rotation = transform.rotation;
            Vector3 euler = rotation.eulerAngles;
            euler.z += rotationSpeed;
            transform.rotation = Quaternion.Euler(euler);
        }
    }
}
