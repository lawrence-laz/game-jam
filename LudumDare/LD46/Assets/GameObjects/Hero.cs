using DG.Tweening;
using Libs.Base.GameLogic.AudioSource;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public List<Hostage> Hostages;

    public TurnManager TurnManager { get; set; }
    public TileObject TileObject { get; set; }
    public PlayRandomAudioClipBehaviour FeetAaudio { get; set; }
    public PlayRandomAudioClipBehaviour VoiceAudio { get; set; }
    public Map Map { get; set; }
    public Move Move { get; set; }

    private Sequence _animation;
    private float _animationStrength = 0.03f;

    private void Start()
    {
        TurnManager = FindObjectOfType<TurnManager>();
        Map = FindObjectOfType<Map>();
        TurnManager.OnTurnEnded.AddListener(OnTurnEnded);
        TileObject = GetComponent<TileObject>();
        FeetAaudio = transform.Find("hero-legs").GetComponentInChildren<PlayRandomAudioClipBehaviour>();
        VoiceAudio = transform.Find("hero-voice").GetComponentInChildren<PlayRandomAudioClipBehaviour>();

        Move = GetComponent<Move>();

        Move.OnMove.AddListener(() => FeetAaudio.PlayRandomAudioClip());

        _animation = DOTween.Sequence()
            .Append(transform.DOBlendableScaleBy(Vector3.down * _animationStrength, .4f))
            .Join(transform.DOBlendableLocalMoveBy(Vector3.down * _animationStrength, .4f))
            .Append(transform.DOBlendableScaleBy(Vector3.up * _animationStrength, .4f))
            .Join(transform.DOBlendableLocalMoveBy(Vector3.up * _animationStrength, .4f))
            .SetLoops(-1);

        AskHostagesToFollow();
    }

    private void OnDestroy()
    {
        _animation?.Kill();
    }

    private void OnTurnEnded()
    {
        AskHostagesToFollow();
    }

    private void AskHostagesToFollow()
    {
        var newHostages = false;
        foreach (var position in TileObject.Position.Around())
        {
            var targetGameObject = Map.Get(position);
            if (targetGameObject.TryGetComponentSafe<Hostage>(out var hostage) && !Hostages.Contains(hostage))
            {
                newHostages = true;
                hostage.Follow(Hostages.Count == 0 ? TileObject : Hostages.Last().GetComponent<TileObject>());
                Hostages.Add(hostage);
            }
        }

        if (newHostages)
        {
            VoiceAudio.PlayRandomAudioClip();
        }
    }
}
