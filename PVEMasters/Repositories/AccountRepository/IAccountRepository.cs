﻿using PVEMasters.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVEMasters.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        Task AddMissionRewardsToAccount(List<MissionRwards> missionRewards);
        Task<ApplicationUser> getUserByUsername(string userName);
        Task UpdateUser(ApplicationUser usr);
        int CreateAccountStatistic(AccountStatistic accountStatistic);
    }
}
