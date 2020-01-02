using PVEMasters.IRewardsManager;

namespace PVEMasters.RewardsManager
{
    public static class RewardsChain
    {
        public static AbstractRewardHandler GetChain()
        {
            AbstractRewardHandler chain = new ExperienceHandler();
            chain.SetNext(new GoldHandler());

            return chain;
        }
    }
}
