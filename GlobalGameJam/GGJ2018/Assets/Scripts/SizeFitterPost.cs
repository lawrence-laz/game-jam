using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeFitterPost : MonoBehaviour
{
    private RectTransform message;

    private void OnEnable()
    {
        message = (RectTransform)transform.Find("Message").transform;
    }

    private void Update()
    {

        Rect current = ((RectTransform)transform).rect;
        float height = current.height;

        Vector3[] thisCorners = new Vector3[4];
        Vector3[] messageCorners = new Vector3[4];
        ((RectTransform)transform).GetWorldCorners(thisCorners);
        message.GetWorldCorners(messageCorners);

        while (messageCorners[0].y - 10 < thisCorners[0].y)
        {
            ((RectTransform)transform).sizeDelta = new Vector2(current.width, height);
            height += 10;
            ((RectTransform)transform).GetWorldCorners(thisCorners);
            Debug.Log(messageCorners[0].y + " " + thisCorners[0].y);
        }
    }
}
