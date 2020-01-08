namespace UnityGoodies
{
    public static class IntExtensions
    {
        public static bool IsBetween(this int x, int startIncluding, int endExcluding)
        {
            return x >= startIncluding && x < endExcluding;
        }
    }
}
