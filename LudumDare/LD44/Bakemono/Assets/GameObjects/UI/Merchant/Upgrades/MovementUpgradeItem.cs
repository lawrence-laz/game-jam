public class MovementUpgradeItem : BaseUpgradeItem
{
    private void OnEnable()
    {
        Price = 50;
    }

    public void OnClickMovement()
    {
        BuyLogic((stats) => GiveMovementBoost(stats));
    }

    private void GiveMovementBoost(HeroStats stats)
    {
        stats.MovementSpeed *= 1.656f;
    }
}
