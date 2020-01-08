using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementBadge : MonoBehaviour
{
    [SerializeField] new string name;

    new SpriteRenderer renderer;

    private ParticleSystem particles;
    bool alreadyAchieved = false;

    public static void Achieved(string name)
    {
        PlayerPrefs.SetInt(name, 1);
    }

    [ContextMenu("Achieved")]
    public void AchievedNonStatic()
    {
        PlayerPrefs.SetInt(name, 1);
    }

    private void OnEnable()
    {
        renderer = GetComponent<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        alreadyAchieved = IsAchieved();
    }

    private void Start()
    {
        InvokeRepeating("UpdateStatus", 0, 1);
    }

    private void UpdateStatus()
    {
        if (!IsAchieved())
            renderer.color = Color.black;
        else
        {
            if (!alreadyAchieved)
            {
                alreadyAchieved = true;
                ParticleAnimation();
            }
            renderer.color = Color.white;
        }
    }

    private void ParticleAnimation()
    {
        if (particles == null)
            return;

        particles.transform.position = ClientsManager.Instance.ActivePosition;
        particles.Play();
        Sequence s = DOTween.Sequence()
            .Append(particles.transform.DOMove(transform.position, 5))
            .OnComplete(() => {
                particles.Stop();
            })
            .Play();
    }

    private bool IsAchieved()
    {
        return PlayerPrefs.GetInt(name, 0) == 1;
    }
}
