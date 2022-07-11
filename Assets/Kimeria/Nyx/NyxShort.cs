using System;
using System.Collections.Generic;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools;

namespace Kimeria.Nyx
{
    /// <summary>
    /// static class to have shortcut in accessing main managers of Nyx
    /// </summary>
    public static class NyxShort
    {
        /// <summary>
        /// Access the global and static EventManager
        /// </summary>
        public static EventManager GlobalEvents
        {
            get => GlobalEventManger.EventsManager;
        }

        public static Tools.Localisation.LocalizationManager Localisation
        {
            get => Tools.Localisation.LocalizationManager.Instance;
        }
        public static string Localize(string textId)
        {
            return Localisation.GetLocalizedValue(textId);
        }
    }
}
