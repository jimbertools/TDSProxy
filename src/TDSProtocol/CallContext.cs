﻿using System.Collections.Concurrent;
using System.Threading;

namespace TDSProtocol
{

    /// <summary>
    /// Provides a way to set contextual data that flows with the call and 
    /// async context of a test or invocation.
    /// </summary>
    internal static class CallContext
    {
        private static readonly ConcurrentDictionary<string, AsyncLocal<object>> State = new();

        /// <summary>
        /// Stores a given object and associates it with the specified name.
        /// </summary>
        /// <param name="name">The name with which to associate the new item in the call context.</param>
        /// <param name="data">The object to store in the call context.</param>
        public static void LogicalSetData(string name, object data) =>
            State.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        /// <summary>
        /// Retrieves an object with the specified name from the <see cref="CallContext"/>.
        /// </summary>
        /// <param name="name">The name of the item in the call context.</param>
        /// <returns>The object in the call context associated with the specified name, or <see langword="null"/> if not found.</returns>
        public static object LogicalGetData(string name) =>
            State.TryGetValue(name, out var data) ? data.Value : null;
    }
}
