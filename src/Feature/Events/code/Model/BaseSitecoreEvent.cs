using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Feature.Events.Model
{
    public class BaseSitecoreEvent
    {
        public Item Item { get; set; }

        public string EventUrl
        {
            get
            {
                return LinkManager.GetItemUrl(Item);
            }
        }

        public string Title { get; set; }
        public ReferenceField EventType { get; set; }

        public CheckboxField AllDayEvent { get; set; }
        public DateField StartDate { get; set; }
        public DateField EndDate { get; set; }


    }
}