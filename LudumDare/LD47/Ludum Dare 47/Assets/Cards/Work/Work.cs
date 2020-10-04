using UnityEngine;

public static class Work
{
    public static bool IsWorkTime()
    {
        return (!GameObject.FindObjectOfType<Clock>().IsWorkHours || GameObject.FindObjectOfType<Calendar>().IsWeekend);
    }
}
