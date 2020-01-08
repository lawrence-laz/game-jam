using Assets.External.DreamBit.Extension;
using DG.Tweening;
using DreamBit.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClientBehaviour : MonoBehaviour
{
    private struct ConditionAndReaction
    {
        public string Name;
        public Func<bool> Condition;
        public Func<IEnumerator> Reaction;
    }

    public const float MoveSpeed = 2.5f;
    public readonly Vector3 ItemSpawnPosition = new Vector3(3, 1.75f, 2.25f);

    public Vector3 TargetPosition { get; set; }
    public bool IsInTargetPosition { get { return TargetPosition == transform.position; } }
    public bool IsActive { get { return ClientsManager.Instance.ActiveClient == gameObject; } }
    public bool Paused { get; set; }
    public bool Bought { get; set; }

    public AudioClip NoAudioClip;
    public AudioClip YesAudioClip;
    public AudioClip[] TalkSounds;

    public Item Item = null;
    public List<Item> OwnedItems = new List<Item>();

    protected IEnumerator ai;
    protected Func<IEnumerator> nextVisit;
    protected int timesVisited;

    private Vector3 lastFramePosition;
    private new AudioSource audio;
    private List<ConditionAndReaction> conditionsAndReactions = new List<ConditionAndReaction>();
    private List<string> activeReactions = new List<string>();

    protected bool IsFirstVisit { get { return timesVisited == 1; } }

    public void Leave()
    {
        TargetPosition = ClientsManager.Instance.ExitPosition;
        Speech.Instance.Hide();
        Item = null;
        ClientsManager.Instance.ActiveClient = null;
        activeReactions = new List<string>();
    }

    public void Take(Item item)
    {
        Item = item;
        OwnedItems.Add(item);
        item.gameObject.SetActive(false);
    }

    protected IEnumerator AiLoop()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            yield return WaitForActive();
            yield return BeforeEnter();
            yield return WaitForBeingInPosition();

            if (nextVisit != null)
            {
                timesVisited++;
                yield return nextVisit();
            }

            Debug.Log("Leaving");
            Leave();
        }
    }

    protected IEnumerator AiReaction()
    {
        yield return new WaitForEndOfFrame();
        if (nextVisit != null)
        {
            yield return nextVisit();
        }

        Debug.Log("Leaving");
        Leave();
        ai = null;
        this.StartCoroutineIfNotStarted(ref ai, AiLoop());
    }

    protected void AddConditionAndReaction(string name, Func<bool> condition, Func<IEnumerator> reaction)
    {
        conditionsAndReactions.Add(new ConditionAndReaction()
        {
            Name = name,
            Condition = condition,
            Reaction = reaction
        });
    }

    protected virtual IEnumerator BeforeEnter()
    {
        Debug.Log("Before enter");
        yield return null;
    }

    private void Start()
    {
        timesVisited = 0;
        audio = GameObject.FindGameObjectWithTag("OneShotAudio").GetComponent<AudioSource>();
        TargetPosition = ClientsManager.Instance.ExitPosition;

        //animationSequence = DOTween.Sequence()
        //.Append(transform.DOLocalRotate(Vector3.forward * 10, 0.2f))
        ////.Append(transform.DOLocalRotate(Vector3.forward * -90, 0.5f))
        //.SetLoops(-1, LoopType.Yoyo)
        //.Play();
        InvokeRepeating("Animate", 0, 1f / 23f);
        Initialize();
    }


    private void Update()
    {
        if (IsActive && IsInTargetPosition)
        {
            CheckConditionsAndStartReactions();
            Think();
        }

        if (!Paused)
            Walk();

        Animate();
    }

    private void LateUpdate()
    {
        lastFramePosition = transform.position;
    }

    private void CheckConditionsAndStartReactions()
    {
        foreach (ConditionAndReaction item in conditionsAndReactions)
        {
            if (activeReactions.Contains(item.Name) || !item.Condition())
                continue;

            activeReactions.Add(item.Name);
            nextVisit = item.Reaction;
            StopAllCoroutines();
            ai = null;
            this.StartOrRestartCoroutine(ref ai, AiReaction());
            Speech.Instance.Hide();

            return;
        }
    }

    Sequence walkAnimation = null;
    Sequence idleAnimation = null;
    private void Animate()
    {
        if (!IsInTargetPosition && !Paused && (walkAnimation == null || !walkAnimation.IsPlaying()))
        {
            //Debug.Log("Walking");
            StopAnimation();
            WalkAnimation();
        }
        else if ((IsInTargetPosition || Paused) && (idleAnimation == null || !idleAnimation.IsPlaying()))
        {
            //Debug.Log("Standing");
            StopAnimation();
            IdleAnimation();
        }
    }

    private void WalkAnimation()
    {
        Vector3 rotation = transform.localRotation.eulerAngles;

        walkAnimation = DOTween.Sequence()
            .Append(transform.DOLocalRotate(Vector3.forward * 10, 0.2f))
            .Append(transform.DOLocalRotate(rotation, 0.2f))
            //.SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false)
            .Play();
        walkAnimation.OnComplete(() => walkAnimation.Restart());
    }

    private void IdleAnimation()
    {
        float scale = transform.localScale.y;

        idleAnimation = DOTween.Sequence()
            .Append(transform.DOScaleY(scale * 1.07f, 1f))
            .Append(transform.DOScaleY(scale, 1f))
            //.SetLoops(-1, LoopType.Yoyo)
            .SetAutoKill(false)
            .Play();
        idleAnimation.OnComplete(() => idleAnimation.Restart());
    }

    private void StopAnimation()
    {
        walkAnimation?.OnComplete(null);
        walkAnimation?.SetAutoKill(true);
        idleAnimation?.OnComplete(null);
        idleAnimation?.SetAutoKill(true);
    }

    protected virtual void Think()
    {
        if (IsInTargetPosition)
            TargetPosition = ClientsManager.Instance.ExitPosition;
        Debug.LogWarning("AUTO BEHAVIOUR EXIT ON ENTER");
    }

    protected virtual void Initialize()
    {

    }

    private void Walk()
    {
        if (IsInTargetPosition)
            return;

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MoveSpeed * Time.deltaTime);
    }

    protected IEnumerator Say(string text, float duration = 0f)
    {
        Speech.Instance.Say(text);
        audio.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audio.clip = TalkSounds.GetRandom();
        audio.Play();
        //audio.PlayOneShot(TalkSounds.GetRandom());

        if (duration == 0f)
        {
            Speech.Instance.Skippable();
            yield return WaitForSpace();
        }
        else
        {
            Speech.Instance.NotSkippable();
            yield return new WaitForSeconds(duration);
        }
    }

    protected IEnumerator WaitForSpace()
    {
        do yield return null;
        while (!Input.GetKeyDown(KeyCode.Space));
        yield return new WaitForEndOfFrame();
    }

    protected IEnumerator WaitForActive()
    {
        while (!IsActive)
            yield return null;
        yield return new WaitForEndOfFrame();
    }

    protected IEnumerator WaitForBeingInPosition()
    {
        while (!IsInTargetPosition)
            yield return null;
        yield return new WaitForEndOfFrame();
    }

    protected IEnumerator WaitForItem()
    {
        while (Item == null)
            yield return null;
        yield return new WaitForEndOfFrame();
    }

    protected void Pay(int gold)
    {
        Chest.Instance.Gold += gold;

        if (Chest.Instance.Gold >= 500)
            AddExistingClient<CowBehaviour>();
    }

    protected IEnumerator SayPayLeave(string text, float duration, int gold)
    {
        yield return Say(text, 2);
        Pay(gold);
        Leave();
    }

    protected IEnumerator SayPayLeave(string text, int gold)
    {
        yield return Say(text, 0);
        Pay(gold);
        Leave();
    }

    protected IEnumerator SayPayLeave(string[] texts, int gold)
    {
        foreach (string text in texts)
        {
            yield return Say(text, 2);
        }
        Pay(gold);
        Leave();
    }

    protected GameObject GiveItem(GameObject prefab)
    {
        GameObject item = Instantiate(prefab);
        item.transform.position = ItemSpawnPosition;

        Item itemComponent = item.GetComponent<Item>();
        //if (itemComponent.Type.ContainsAny("rare", "epic"))
        //    AddExistingClient<WalrusBehaviour>();

        //if (itemComponent.Name.ContainsAny("metal", "orb", "candle"))
        //    AddExistingClient<BlacksmithBehaviour>();

        return item;
    }

    protected void RemoveFromClients()
    {
        RemoveFromClients(gameObject);
    }

    protected void RemoveFromClients<T>() where T : ClientBehaviour
    {
        GameObject client = ClientsManager.Instance.Clients.FirstOrDefault(x => x.GetComponentInChildren<T>() != null);
        RemoveFromClients(client);
    }

    protected void RemoveFromClients(GameObject client)
    {
        if (client == null)
        {
            Debug.LogWarning("Probably shouldn't be null");
            return;
        }

        Debug.Log("Removing: " + client.name);

        ClientsManager.Instance.Clients.Remove(client);
    }

    protected bool HasActiveNotHiddenItem(string name)
    {
        Item[] allObjects = FindObjectsOfType<Item>();

        return allObjects.Any(item => item.gameObject.activeSelf == true && item.Name == name && item.GetComponent<HiddenItemBehaviour>() == null);
    }

    protected IEnumerator Sell(string sellMessage,
                        string soldMessage, float soldMessageDuration,
                        string skipMeesage, float skipMessageDuration,
                        GameObject itemPrefab, int price)
    {
        yield return new WaitForEndOfFrame();
        string message = sellMessage;
        Item item = GiveItem(itemPrefab).GetComponent<Item>();
        ForSaleBehaviour forSale = item.gameObject.AddComponent<ForSaleBehaviour>();
        forSale.Price = price;
        message = message.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name);
        yield return Say(message, 0.05f);
        yield return new WaitForEndOfFrame();
        Speech.Instance.Cancelable();
        bool x = false;
        while (item.GetComponent<ForSaleBehaviour>() != null && !(x = Input.GetKeyDown(KeyCode.X)))
            yield return null;
        yield return new WaitForEndOfFrame();

        if (item.GetComponent<ForSaleBehaviour>() == null)
        {
            Bought = true;
            GetComponent<AudioSource>().PlayOneShot(YesAudioClip);
            yield return Say(soldMessage.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name), soldMessageDuration);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            Bought = false;
            Sequence s = DOTween.Sequence()
                .Append(item.transform.DOMove(ClientsManager.Instance.ActivePosition, 1))
                .Join(item.transform.DOScale(0, 1f))
                .OnComplete(() => Destroy(item.gameObject));
            //Destroy(item.gameObject);
            GetComponent<AudioSource>().PlayOneShot(NoAudioClip);
            yield return Say(skipMeesage.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name), skipMessageDuration);
            yield return new WaitForEndOfFrame();
        }
    }

    protected IEnumerator SellOrReactToItem(string sellMessage,
                    string soldMessage, float soldMessageDuration,
                    string skipMeesage, float skipMessageDuration,
                    GameObject itemPrefab, int price,
                    string reactionToItemMessage, float reactionToItemMessageDuration,
                    bool customReact = false, bool shouldTakeItem = false)// TODO: account this when there's a sell area instead of dropping item.
    {
        string message = sellMessage;
        Item item = GiveItem(itemPrefab).GetComponent<Item>();
        ForSaleBehaviour forSale = item.gameObject.AddComponent<ForSaleBehaviour>();
        forSale.Price = price;
        message = message.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name);
        yield return Say(message, 0.05f);
        Speech.Instance.Cancelable();
        bool x = false;
        while (item.GetComponent<ForSaleBehaviour>() != null && !(x = Input.GetKeyDown(KeyCode.X)) && Item == null)
            yield return null;

        if (Item != null)
        {
            Bought = false;
            Sequence s = DOTween.Sequence()
                .Append(item.transform.DOMove(ClientsManager.Instance.ActivePosition, 1))
                .Join(item.transform.DOScale(0, 1f))
                .OnComplete(() => Destroy(item.gameObject));

            if (!customReact)
                yield return Say(reactionToItemMessage.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name), reactionToItemMessageDuration);
        }
        else if (item.GetComponent<ForSaleBehaviour>() == null)
        {
            Bought = true;
            GetComponent<AudioSource>().PlayOneShot(YesAudioClip);
            yield return Say(soldMessage.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name), soldMessageDuration);
        }
        else
        {
            Bought = false;
            Sequence s = DOTween.Sequence()
                .Append(item.transform.DOMove(ClientsManager.Instance.ActivePosition, 1))
                .Join(item.transform.DOScale(0, 1f))
                .OnComplete(() => Destroy(item.gameObject));
            //Destroy(item.gameObject);
            GetComponent<AudioSource>().PlayOneShot(NoAudioClip);
            yield return Say(skipMeesage.Replace("#price#", forSale.Price.ToString()).Replace("#name#", item.Name), skipMessageDuration);
        }
    }

    protected IEnumerator Ask(string question)
    {
        yield return Say(question, 0.05f);
        Speech.Instance.Cancelable();
        bool x = false;
        while (Item == null && !(x = Input.GetKeyDown(KeyCode.X)))
            yield return null;
        if (x)
        {
            GetComponent<AudioSource>().PlayOneShot(NoAudioClip);
        }
    }

    protected void AddExistingClient<T>() where T : ClientBehaviour
    {
        ClientsManager.Instance.AddExistingClient<T>();
    }

    protected bool HasRareItems()
    {
        return FindObjectsOfType<Item>().Any(item => item.Type == "rare" && item.GetComponent<ForSaleBehaviour>() == null);
    }

    protected bool HasEpicItems()
    {
        return FindObjectsOfType<Item>().Any(item => item.Type == "epic" && item.GetComponent<ForSaleBehaviour>() == null);
    }

    protected bool HasAnyItemNamed(params string[] names)
    {
        return FindObjectsOfType<Item>().Any(item => item.Name.ContainsAny(names) && item.GetComponent<ForSaleBehaviour>() == null);
    }

    protected bool HasAnyItemTagged(params string[] tags)
    {
        return FindObjectsOfType<Item>().Any(item => item.Tags.ContainsAny(tags) && item.GetComponent<ForSaleBehaviour>() == null);
    }
}
