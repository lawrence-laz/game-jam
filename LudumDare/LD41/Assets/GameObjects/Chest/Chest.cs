using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Chest : Singleton<Chest>
{
    private int _gold;

    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            if (value == _gold)
                return;

            int cappedValue = Mathf.Max(0, value);

            if (value > _gold && started == true)
                OnGoldRecieved.Invoke(cappedValue.ToString());
            else if (started == true)
                OnGoldLost.Invoke(cappedValue.ToString());

            _gold = cappedValue;

            if (_gold == 0 && started == true)
                OnOutOfGold.Invoke();

            if (Gold >= 1000)
                AchivementBadge.Achieved("1000");
        }
    }

    public UnityEvent OnOutOfGold;
    public UnityEventString OnGoldRecieved;
    public UnityEventString OnGoldLost;

    private bool started = false;

    [ContextMenu("Add 10 gold")]
    public void Add10Gold()
    {
        Gold += 10;
    }

    [ContextMenu("Add 500 gold")]
    public void Add500Gold()
    {
        Gold += 500;
    }

    [ContextMenu("Remove 1000 gold")]
    public void Remove1000Gold()
    {
        Gold -= 1000;
    }

    [ContextMenu("Set to 1 gold")]
    public void SetTo1Gold()
    {
        Gold = 1;
    }

    private void Start()
    {
        Gold = 100;
        started = true;
    }

    private void Update()
    {
        if (!HasItems() && Gold < 50)
        {
            OnOutOfGold.Invoke();
        }
    }

    private bool HasItems()
    {
        return FindObjectsOfType<Item>().Any(item => item.GetComponent<ForSaleBehaviour>() == null);
    }
}
