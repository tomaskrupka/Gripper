﻿using Gripper.ChromeDevTools.Page;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    /// <summary>
    /// Facilitates 1-1-1 mapping between iFrame-Execution context-IContext.
    /// </summary>
    public interface IContextFactory
    {

        /// <summary>
        /// Tries to find the DOM execution context of an iFrame and create an <see cref="IContext"/> representation of it.
        /// If an iFrame has more than one execution contexts, matches the one with access to the DOM.
        /// If an iFrame has no execution contexts, returns null.
        /// </summary>
        /// <param name="frame">Frame to find the execution context for.</param>
        /// <returns>Resulting <see cref="IContext"/> object, or null if no context was matched.</returns>
        public Task<IContext?> CreateContextAsync(Frame frame);
    }
}
