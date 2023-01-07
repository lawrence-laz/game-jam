using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopulationControl : MonoBehaviour
{
    public string[] OtherLabels;
    public int MaxCount = int.MaxValue;

    private string _label;
    private bool _isBeingDestroyed;

    private void Start()
    {
        _label = GetComponent<Label>().Text;

        var count = GameObject.FindObjectsOfType<PopulationControl>()
            .Count(obj => !obj._isBeingDestroyed && obj.GetComponent<Label>().Text == _label);

        if (OtherLabels.Length > 0)
        {
            var otherLabelsCount = GameObject.FindObjectsOfType<Label>()
                .Count(label => OtherLabels.Any(otherLabel => otherLabel == label.Text));
            if (otherLabelsCount <= count)
            {
                DestroyThis();
            }
        }
        else if (count > MaxCount)
        {
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
        _isBeingDestroyed = true;
    }
}
