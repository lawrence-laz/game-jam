using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    private SkillBehaviour selectedSkill;

    public void SelectSkill(SkillBehaviour skill)
    {
        if (selectedSkill != null)
        {
            selectedSkill.OnDeselect();
        }

        selectedSkill = skill;
        skill.OnSelected();
    }

    // Called from Canvas/SkillManager EventTrigger
    public void ShootSkill()
    {
        if (selectedSkill == null)
            return;

        selectedSkill.Shoot(CursorBehaviour.GetWorldPosition());
        selectedSkill.OnDeselect();
        selectedSkill = null;
    }

    private void Update()
    {
        if (selectedSkill != null && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)))
        {
            selectedSkill.OnDeselect();
            selectedSkill = null;
        }
    }
}
