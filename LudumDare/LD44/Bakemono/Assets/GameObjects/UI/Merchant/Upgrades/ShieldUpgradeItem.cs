public class ShieldUpgradeItem : BaseUpgradeItem
{
    public void OnClickShield()
    {
        BuyLogic((stats) => GiveShield(stats));
    }

    private void OnEnable()
    {
        Price = 50;
    }

    private void GiveShield(HeroStats stats)
    {
        stats.HasShield = true;
        stats.Shield += 1;
    }
}
