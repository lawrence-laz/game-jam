public class DaggerUpgradeItem : BaseUpgradeItem
{
    public void OnClickItem()
    {
        BuyLogic((stats) => GiveDagger(stats));
    }

    private void OnEnable()
    {
        Price = 110;
    }

    private void GiveDagger(HeroStats stats)
    {
        stats.Dagger = true;
        stats.transform.Find("Dagger_Object").gameObject.SetActive(true);
    }
}
