using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchDownloader.Model
{
    public interface ISaver
    {
        void Save(string content);
    }
}
