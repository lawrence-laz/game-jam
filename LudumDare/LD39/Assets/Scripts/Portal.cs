using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Portal : MonoBehaviour
{
    [SerializeField]
    public Transform targetPosition;
    [SerializeField]
    float showMessageDelay = 0.5f;
    [SerializeField]
    AudioClip openSound;
    [SerializeField]
    AudioClip lockedSound;
    [SerializeField]
    public string[] lockedMessage = null;
    int lockedMessageIndex = 0;
    new AudioSource audio;
    TextManager textManager;

    Transform player;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        textManager = TextManager.Instance;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Action.Instance.action && other.gameObject.tag == "Player" && textManager.AllowMovement)
        {
            Action.Instance.action = false;
            TeleportToTargetPosition(other.transform);
        }
    }

    public void TryOpen()
    {
        //GameObject.FindWithTag("Player").transform.LookAt(transform);
        TeleportToTargetPosition(GameObject.FindWithTag("Player").transform);
    }

    private void TeleportToTargetPosition(Transform player)
    {
        if (targetPosition == null)
        {
            CancelInvoke();
            Invoke("ShowLockedMessage", showMessageDelay);
            if (lockedSound == null)
                audio.Play();
            else
                audio.PlayOneShot(lockedSound);
            return;
        }

        if (openSound == null)
            audio.Play();
        else
            audio.PlayOneShot(openSound);
        player.position = targetPosition.transform.position;
    }

    private void ShowLockedMessage()
    {
        if (!TextManager.Instance.AllowMovement)
            return;

        if (lockedMessage.Length == 0)
        {
            //TextManager.Instance.ShowTextTyped("It's locked..." , 1, 0.05f);
            TextManager.Instance.ShowTextTyped(new string[] { "It's locked..." }, 0.1f, gameObject);
        }
        //else if (lockedMessage.Length == 1)
        //{
        //    TextManager.Instance.ShowTextTyped(lockedMessage[0] == "" ? "It's locked..." : lockedMessage[0], 1, 0.05f);
        //}
        else
        {
            TextManager.Instance.ShowTextTyped(lockedMessage, 0.1f, gameObject);
        }
    }

    float timeStartedTurning;
    Quaternion targetRotation;
    void LookAtPortal()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeStartedTurning = Time.time;
        InvokeRepeating("TurnStep", 0, 1 / 30f);
        targetRotation = Quaternion.LookRotation(transform.position - player.position);
    }

    void TurnStep()
    {
        // Smoothly rotate towards the target point.
        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, 5 * Time.deltaTime);
        if (Time.time - timeStartedTurning > 3)
            CancelInvoke("TurnStep");
    }

    private void PlayMelody1()
    {
        SoundPlayer.Instance.PlayMelody1();
    }
}
