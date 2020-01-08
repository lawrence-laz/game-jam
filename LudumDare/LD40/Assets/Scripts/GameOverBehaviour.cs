using UnityEngine;
using UnityEngine.UI;

public class GameOverBehaviour : MonoBehaviour
{
    private static string[] tips = {
        "TIP: Use the lightning spell to keep balance in numbers.",
        "TIP: Bunny zombie spell resurrects bunnies after death as zombies!",
        "TIP: It's good to keep atleast two separate bunny groups alive.",
        "TIP: Lightning destroys plants as well.",
        "TIP: Use the sun spell as often as you can, it has no downside!",
        "TIP: The sleeping spell stops animals from starving, but not aging.",
        "TIP: The bunny zombie can only be killed by lightning.",
        "TIP: The egg hatches a bunny more often than a wolf.",
    };

    [SerializeField]
    private GameObject gameOverPanel;

    private Transform herbivoreContainer;
    private Transform carnivoreContainer;
    private bool isGameOver = false;

    private int TipIndex
    {
        get
        {
            int index = PlayerPrefs.GetInt("TIP_ID", 0);
            if (index >= tips.Length)
                index = Random.Range(0, tips.Length);
            else
                PlayerPrefs.SetInt("TIP_ID", index + 1);

            return index;
        }
    }

    private void OnEnable()
    {
        herbivoreContainer = GameObject.FindGameObjectWithTag("HerbivoreContainer").transform;
        carnivoreContainer = GameObject.FindGameObjectWithTag("CarnivoreContainer").transform;
    }

    private void Start()
    {
        InvokeRepeating("CheckForGameOver", 2, 1);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerPrefs.DeleteAll();
        }
#endif
    }

    private void CheckForGameOver()
    {
        if (isGameOver)
            return;

        bool herbivoreExists = false;
        foreach (Transform animal in herbivoreContainer)
        {
            herbivoreExists = true;
            break;
        }

        bool carnivoreExists = false;
        foreach (Transform animal in carnivoreContainer)
        {
            carnivoreExists = true;
            break;
        }

        if (herbivoreExists && carnivoreExists)
            return;

        isGameOver = true;
        InitGameOver();
    }

    private void InitGameOver()
    {
        DisableAllSkills();
        ShowGameOverPanel();
    }

    private void DisableAllSkills()
    {
        GameObject.FindGameObjectWithTag("SkillsBar").SetActive(false);
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Text tipText = gameOverPanel.transform.Find("Tip_Text").GetComponent<Text>();
        tipText.text = Time.timeSinceLevelLoad < 20 ? "TIP: Keep both wolves and bunnies alive!" : tips[TipIndex];
    }
}
