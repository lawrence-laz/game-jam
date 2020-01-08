using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFeed : MonoBehaviour
{
    [SerializeField]
    private PostImage postImage;
    [SerializeField]
    private PostText postText;

    private Feed feed;

    private void OnEnable()
    {
        feed = GetComponent<Feed>();
    }

    private void Update()
    {
        if (postImage != null)
        {
            feed.Post(postImage);
            postImage = null;
        }

        if (postText != null)
        {
            feed.Post(postText);
            postText = null;
        }
    }
}
