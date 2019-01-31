using PVEMasters.ApiModels;
using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.ModelMappers
{
    public class MissionRewardMapper
    {
        public static ApiMissionRewards convertToApiModel(MissionRwards Model)
        {
            return new ApiMissionRewards { RewardName = Model.RewardName, Amount = Model.Amount, MissionId = Model.MissionId };
        }

        public static MissionRwards converToDbModel(ApiMissionRewards ApiModel)
        {
            //Needs implementation
            return new MissionRwards { Amount = ApiModel.Amount, RewardName = ApiModel.RewardName };
        }
    }
}
