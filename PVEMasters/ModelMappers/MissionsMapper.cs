using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ModelMappers
{
    public class MissionsMapper
    {
        public static ApiMission convertToApiModel(Mission Model)
        {
            return new ApiMission {Id = Model.Id, Name = Model.Name, AverageLvlRequired = (int)Model.AverageLvlRequired, AccountLvlRequired = (int)Model.AccountLvlRequired, Duration = Model.Duration, MissionRewards = CreateMissionRewards(Model.MissionRwards)  };
        }

        public static Mission converToDbModel(ApiMission ApiModel)
        {
            return new Mission {Id = ApiModel.Id, Name = ApiModel.Name, AccountLvlRequired = ApiModel.AccountLvlRequired, AverageLvlRequired = ApiModel.AverageLvlRequired, Duration = ApiModel.Duration, MissionRwards = CreateMissionRewardsDBObject(ApiModel.MissionRewards) };
        }

        private static List<ApiMissionRewards> CreateMissionRewards(ICollection<MissionRwards> missionRwards)
        {
            List<ApiMissionRewards> missionRewards = new List<ApiMissionRewards>();

            foreach(var missionReward in missionRwards)
            {
                missionRewards.Add(MissionRewardMapper.convertToApiModel(missionReward));
            }
            return missionRewards;
        }

        private static List<MissionRwards> CreateMissionRewardsDBObject(ICollection<ApiMissionRewards> missionRwards)
        {
            List<MissionRwards> missionRewards = new List<MissionRwards>();

            foreach (var missionReward in missionRwards)
            {
                missionRewards.Add(MissionRewardMapper.converToDbModel(missionReward));
            }
            return missionRewards;
        }
    }
}
