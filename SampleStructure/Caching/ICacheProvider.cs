using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    /// <summary>
    /// Interface ICacheProvider
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        object Get(string key);
        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        void Set(string key, object data);
        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        /// <param name="minutes">The minutes.</param>
        void Set(string key, object data, int minutes);
        /// <summary>
        /// Determines whether the specified key is set.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the specified key is set; otherwise, <c>false</c>.</returns>
        bool IsSet(string key);
        /// <summary>
        /// Invalidates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Invalidate(string key);
    }
}
