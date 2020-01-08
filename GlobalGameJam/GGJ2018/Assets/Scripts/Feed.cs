using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feed : Singleton<Feed>
{
    [SerializeField]
    private GameObject textPostPototype;
    [SerializeField]
    private GameObject imagePostPrototype;

    private Transform content;

    public void Post(PostText post)
    {
        GameObject postInstance = SpawnPost(textPostPototype);
        postInstance.transform.SetSiblingIndex(0);
        postInstance.transform.Find("ProfilePic").GetComponent<Image>().sprite = post.Author.ProfilePic;
        postInstance.transform.Find("Name").GetComponent<Text>().text = post.Author.name;
        postInstance.transform.Find("Message").GetComponent<Text>().text = post.Text;
    }

    public void Post(PostImage post)
    {
        GameObject postInstance = SpawnPost(imagePostPrototype);
        postInstance.transform.SetSiblingIndex(0);
        postInstance.transform.Find("ProfilePic").GetComponent<Image>().sprite = post.Author.ProfilePic;
        postInstance.transform.Find("Name").GetComponent<Text>().text = post.Author.name;
        postInstance.transform.Find("Message").GetComponent<Text>().text = post.Text;
        postInstance.transform.Find("Image").GetComponent<Image>().sprite = post.Image;
    }

    private GameObject SpawnPost(GameObject post)
    {
        return Instantiate(post, content);
    }

    private void OnEnable()
    {
        content = GameObject.Find("Panel_Content").transform;
    }
}
