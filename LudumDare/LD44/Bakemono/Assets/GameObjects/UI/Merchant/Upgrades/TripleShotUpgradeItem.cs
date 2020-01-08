public class TripleShotUpgradeItem : BaseUpgradeItem
{
    public void OnClickItem()
    {
        BuyLogic((stats) => GiveMultiShot(stats));
    }

    private void OnEnable()
    {
        Price = 95;
    }

    private void GiveMultiShot(HeroStats stats)
    {
        stats.MultiShot = true;
    }
}
