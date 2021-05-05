using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossyCore.Search
{
    /// <summary>
    /// Search engine
    /// </summary>
    public interface IEngine<T>
    {
        /// <summary>
        /// Search context for entrys that match the query
        /// </summary>
        /// <param name="query">search term</param>
        /// <returns>list of objects that match the query</returns>
        IList<T> Search(string query);
    }
}
