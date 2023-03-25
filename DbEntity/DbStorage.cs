using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Goods_for_Animals.DbEntity
{
    public static class DbStorage
    {
        public static TradeEntities DB_s { get; set; } = new TradeEntities();
    }
}
