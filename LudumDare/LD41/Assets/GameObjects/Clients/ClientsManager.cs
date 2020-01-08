using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Assets.External.DreamBit.Extension;
using DreamBit.Extensions;
using System;

public class ClientsManager : Singleton<ClientsManager>
{
    [Header("Positions")]
    public Vector3 ExitPosition;
    public Vector3 ActivePosition;

    [Header("Clients")]
    [SerializeField]
    GameObject[] startClients;
    [SerializeField]
    GameObject[] allClients;

    public List<GameObject> Clients { get; set; }
    public GameObject ActiveClient { get; set; }
    public int ClientsVisitedCount { get; set; }

    private GameObject lastClient = null;

    public void AddExistingClient(Type clientType)
    {
        if (!clientType.IsSubclassOf(typeof(ClientBehaviour)))
        {
            Debug.LogError("Shouldn't happen");
            return;
        }

        GameObject client = (FindObjectOfType(clientType) as MonoBehaviour)?.gameObject;
        if (client == null)
        {
            Debug.LogError("Probably shouldn't happen");

            return;
        }

        if (!Clients.Contains(client))
            Clients.Add(client);
    }

    public void AddExistingClient<T>() where T : ClientBehaviour
    {
        AddExistingClient(typeof(T));
    }

    private void Start()
    {
        Clients = new List<GameObject>();
        SpawnStartClients();
    }

    private void Update()
    {
        if (ActiveClient == null)
            CallRandomClient();
    }

    private void SpawnStartClients()
    {
        foreach (GameObject prefab in allClients)
        {
            GameObject client = Instantiate(prefab);
            client.transform.parent = transform;
            client.transform.position = ExitPosition;
            if (startClients.Contains(prefab))
                Clients.Add(client);
        }
    }

    IEnumerator callCoroutine;
    private void CallRandomClient()
    {
        this.StartCoroutineIfNotStarted(ref callCoroutine, CallRandomClientCoroutine());
    }

    private IEnumerator CallRandomClientCoroutine()
    {
        ClientRulesManager.Instance.EvaluateRules();

        if (Clients.Count == 0)
        {
            Debug.Log("No more clients");
            yield break;
        }

        GameObject randomClient = null;
        do randomClient = Clients.GetRandom();
        while ((
                randomClient == lastClient
                || (ClientsVisitedCount == 0 && randomClient.GetComponent<ThiefBehaviour>() != null)
                )
               && Clients.Count > 1);

        lastClient = randomClient;
        ClientBehaviour client = randomClient.GetComponent<ClientBehaviour>();
        client.TargetPosition = ExitPosition;
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 4f));
        client.Paused = true;

        ActiveClient = randomClient;
        client.TargetPosition = ActivePosition;
        client.Paused = false;

        callCoroutine = null;
        ++ClientsVisitedCount;
    }
}
