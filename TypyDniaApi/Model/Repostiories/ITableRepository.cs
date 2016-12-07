using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypyDniaApi.Model.ForumObjects;

namespace TypyDniaApi.Model.Repostiories
{
    public interface ITableRepository
    {
        Table GetTable(DateTime date);
    }
}
