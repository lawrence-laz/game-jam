using ARFC;
using DG.Tweening;
using UnityEngine;
using UnityGoodies;

public class PlayerDeathHandler : MonoBehaviour
{
    public Collider DeathCollider;
    public GameObject GameOverPrefab;

    private float startingTimeScale = 1;
    private float startingFixedDeltaTime = 0.02f;
    private HealthBehaviour _hp;

    private void OnEnable()
    {
        DOTween.KillAll();
    }

    private void Start()
    {
        _hp = GetComponent<HealthBehaviour>();
        Debug.Log("<color=green>FIXING TIME SCALE</color>");
        Time.fixedDeltaTime = startingFixedDeltaTime;
        Time.timeScale = startingTimeScale;
    }

    public void HandleDeath()
    {
        this.ForAllComponentsInRootsChildren<FPController>(controller => {
            controller.Look.Modifiers.Sensitivity = 0;
            controller.enabled = false;
        });

        transform.GetRoot().gameObject.EnableComponentsInChildren(false,
            typeof(AimBehaviour));

        var targetSlowMotion = 0.1f;
        var durationTillSlowMotion = 2;
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, targetSlowMotion, durationTillSlowMotion);
        DOTween.To(() => Time.fixedDeltaTime, x => Time.fixedDeltaTime = x, targetSlowMotion * 0.02f, durationTillSlowMotion);

        var animation = DOTween.Sequence()
            .Append(Camera.main.transform.DOLocalRotate(Vector3.right * 70, .6f).SetEase(Ease.InBack))
            .AppendCallback(() => {
                Instantiate(GameOverPrefab);

                var mainhand = Camera.main.transform.Find("Mainhand");
                mainhand.SetParent(null);
                mainhand.gameObject.AddComponent<Rigidbody>();
                mainhand.gameObject.EnableCollidersInChildren(true);

                var offhand = Camera.main.transform.Find("Offhand");
                offhand.SetParent(null);
                offhand.gameObject.AddComponent<Rigidbody>();
                offhand.gameObject.EnableCollidersInChildren(true);

                FindObjectOfType<CursorHelper>().ShowCursor = true;

                gameObject.EnableCollidersInChildren(false);
                DeathCollider.GetComponent<Collider>().enabled = true;

                var body = GetComponent<Rigidbody>();
                body.constraints = new RigidbodyConstraints();
                if (_hp.LastDamager)
                {
                    Debug.LogFormat("Looking at {0} after death", _hp.LastDamager.gameObject.name);
                    Camera.main.transform.DOLookAt(_hp.LastDamager.position + Vector3.up * 2, 2.2f);
                }

                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.EnableComponentsInChildren(false,
                        typeof(SwordGuyBehaviour),
                        typeof(BowGuyBehaviour));
                }
            });
    }
}
