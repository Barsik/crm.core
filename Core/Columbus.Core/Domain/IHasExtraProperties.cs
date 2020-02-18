using System.Collections.Generic;

namespace Columbus.InnerSource.Core.Domain
{
    /// <summary>
    /// Определение дополнительных параметров
    /// </summary>
    public interface IHasExtraProperties
    {
        Dictionary<string, object> ExtraProperties { get; }
    }
}
