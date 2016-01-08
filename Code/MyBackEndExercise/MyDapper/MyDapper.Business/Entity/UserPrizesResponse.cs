using System;
using System.Collections.Generic;

namespace MyDapper.Business.Entity
{
    public class UserPrizeCollection
    {
        public int UserPrizesTotalCount { get; set; }

        public List<UserPrize> UserPrizeList { get; set; }
    }

    public class UserPrize
    {
        public int UserId { get; set; }

        public string ActivityName { get; set; }

        public string PrizeName { get; set; }

        public DateTime LotteryTime { get; set; }

        public int PrizeCount { get; set; }
    }
}
