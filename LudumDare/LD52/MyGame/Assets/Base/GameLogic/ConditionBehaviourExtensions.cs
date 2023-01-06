namespace Libs.Base.GameLogic
{
    public static class ConditionBehaviourExtensions
    {
        public static bool Value(this ConditionBehaviour[] conditions)
        {
            foreach (ConditionBehaviour condition in conditions)
            {
                if (!condition.Value)
                    return false;
            }

            return true;
        }
    }
}
