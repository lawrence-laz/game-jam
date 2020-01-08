public class BounceShotUpgradeItem : BaseUpgradeItem
{
    public void OnClickItem()
    {
        BuyLogic((stats) => GiveBouncy(stats));
    }

    private void OnEnable()
    {
        Price = 110;
    }

    private void GiveBouncy(HeroStats stats)
    {
        stats.BounceArrows = true;
    }
}
