using System;
using TypyDniaApi.Model.ForumObjects;

namespace TypyDniaApi.Model.DataSource.Contexts
{
    public interface IContext
    {
        Day GetDay(DateTime date);
    }
}
