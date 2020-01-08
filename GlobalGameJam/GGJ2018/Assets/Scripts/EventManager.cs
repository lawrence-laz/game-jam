using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public const float PostsDelay = 6;
    public const float EventFrequency = 20;

    public List<Event> primaryEvents; // Only event that don't require other events to have occured.

    private Dictionary<Event, int> events;
    private Feed feed;
    private Queue<object> postsBuffer;
    private Coroutine postingCoroutine;
    private Coroutine eventCoroutine;

    private float lastTimePosted = 0;

    public void PerformEvent(Event ev)
    {
        AddEventsUnique(ev.EnablesEvents);
        HandleCoinEvents(ev.CoinEffects);
        foreach (PostText post in ev.PostsText)
        {
            postsBuffer.Enqueue(post);
        }
        foreach (PostImage post in ev.PostsImage)
        {
            postsBuffer.Enqueue(post);
        }
        PostDequeued();
    }

    private void OnEnable()
    {
        feed = GameObject.FindWithTag("Feed").GetComponent<Feed>();
        postsBuffer = new Queue<object>();
        events = new Dictionary<Event, int>();

        AddEventsUnique(primaryEvents.ToArray());

        if (postingCoroutine != null)
            StopCoroutine(postingCoroutine);
        postingCoroutine = StartCoroutine(PostFromBuffer());

        if (eventCoroutine != null)
            StopCoroutine(eventCoroutine);
        eventCoroutine = StartCoroutine(SimulateEvent());
    }

    private IEnumerator SimulateEvent()
    {
        yield return new WaitForSeconds(5);

        while (true)
        {
            RemoveDeadEvents();
            if (events.Count > 0)
            {
                Event ev = WeightedRandomizer.From(events).TakeOne();
                events.Remove(ev);
                PerformEvent(ev);
            }

            yield return new WaitForSeconds(EventFrequency);
        }
    }

    public void RemoveDeadEvents()
    {
        List<Event> toRemove = new List<Event>();
        foreach (var ev in events)
        {
            if (!ev.Key.CoinEffects.All(x => Coin.IsWorking(x.CoinName)))
                toRemove.Add(ev.Key);
        }
        foreach (Event ev in toRemove)
        {
            events.Remove(ev);
        }
    }

    private IEnumerator PostFromBuffer()
    {
        while (true)
        {
            if (Time.time - lastTimePosted > PostsDelay)
                PostDequeued();

            yield return new WaitForSeconds(PostsDelay);
        }
    }

    private void PostDequeued()
    {
        if (postsBuffer.Count > 0)
        {
            lastTimePosted = Time.time;
            object post = postsBuffer.Dequeue();
            if (post is PostText)
                feed.Post((PostText)post);
            else if (post is PostImage)
                feed.Post((PostImage)post);
            else
                throw new System.Exception("What do you think you are doing putting thin in ma spahett?");
        }
    }

    private void HandleCoinEvents(CoinChangeValue[] changes)
    {
        foreach (CoinChangeValue change in changes)
        {
            Coin coin = null;
            try
            {
                coin = Coin.GetByName(change.CoinName);
                if (coin == null)
                    Debug.Log(change.CoinName + " not found");
            }
            catch(Exception e)
            {
                Debug.Log(change.CoinName + "not found");
            }
            Simulator sim = coin.GetComponent<Simulator>();
            sim.MemeVelocity = change.VelocityChangeAmount;
            sim.MemeVelocityFadeOutDuration = change.FadeOutSpeed;
        }
    }

    private void AddEventsUnique(Event[] eventsToAdd)
    {
        foreach (Event ev in eventsToAdd)
        {
            if (ev == null || events.ContainsKey(ev))
                continue;

            events.Add(ev, ev.Probability);
        }
    }
}
