namespace Sitecore.Feature.Events.Repositories
{
    using System.Collections.Generic;
    using Model;
    public interface IEventsRepository
    {
        IEnumerable<SitecoreEvent> Get();

        SitecoreEvent Get(string itemName);
        IEnumerable<BaseSitecoreEvent> GetMinified();
    }
}