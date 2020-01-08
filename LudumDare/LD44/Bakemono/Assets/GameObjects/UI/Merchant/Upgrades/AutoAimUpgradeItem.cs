public class AutoAimUpgradeItem : BaseUpgradeItem
{
    public void OnItemClick()
    {
        BuyLogic((stats) => GiveAutoAim(stats));
    }

    private void OnEnable()
    {
        Price = 95;
    }

    private void GiveAutoAim(HeroStats stats)
    {
        stats.AutoAim = true;
    }
}
