using Sitecore.Links;
using Sitecore.Data.Items;

namespace Sitecore.Feature.Events.SitecoreCustomisation
{
    public class EventsLinkProvider : LinkProvider
    {

        public override string GetItemUrl(Item item, UrlOptions options)
        {
            if (!item.TemplateID.Equals(Templates.Event.ID))
            {
                return base.GetItemUrl(item, options);
            }
            else
            {
                return $"{Sitecore.Context.Site.HostName}/Events/{item.Name}";
            }
        }
    }
}