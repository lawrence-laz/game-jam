using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientRulesManager : Singleton<ClientRulesManager>
{
    private class Rule
    {
        public Func<bool> Evaluate;
        public Type ClientToAdd;
    }

    private List<Rule> rules;

    public void AddJoinRule<T>(Func<bool> rule) where T : ClientBehaviour
    {
        rules.Add(new Rule()
        {
            Evaluate = rule,
            ClientToAdd = typeof(T)
        });
    }

    public void EvaluateRules()
    {
        for (int i = rules.Count - 1; i >= 0; --i)
        {
            Rule rule = rules[i];
            if (!rule.Evaluate())
                continue;

            Debug.Log("RULE MET, adding client: " + rule.ClientToAdd.Name);

            ClientsManager.Instance.AddExistingClient(rule.ClientToAdd);
            rules.RemoveAt(i);
        }
    }

    private void Start()
    {
        rules = new List<Rule>();
    }
}
