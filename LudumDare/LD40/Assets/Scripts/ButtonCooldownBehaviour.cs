using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonCooldownBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text timeLeft;
    [SerializeField]
    private bool startOnCooldown = true;
    [SerializeField]
    private float duration = 5;
    [SerializeField]
    private float nextTimeAvailable;
    [SerializeField]
    private float initialCooldown;

    private Button button;
    
    public void StartCooldown()
    {
        nextTimeAvailable = Time.time + duration;
        button.interactable = false;
    }

    private void OnEnable()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        if (startOnCooldown)
        {
            nextTimeAvailable = Time.time + initialCooldown;
            button.interactable = false;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F9))
        {
            nextTimeAvailable = 0;
        }
#endif

        UpdateTimeLeft();
    }

    private void UpdateTimeLeft()
    {
        if (button.interactable)
            return;

        if (Time.time >= nextTimeAvailable)
        {
            timeLeft.enabled = false;
            button.interactable = true;

            return;
        }

        timeLeft.enabled = true;
        timeLeft.text = (nextTimeAvailable - Time.time).ToString("0");
    }
}
