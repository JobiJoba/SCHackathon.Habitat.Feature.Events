using Newtonsoft.Json;
using Sitecore.Feature.Events.Model;
using System;

namespace Sitecore.Feature.Events.ViewModels
{
    public class EventMinifiedViewModel
    {
        [JsonIgnore]
        public BaseSitecoreEvent Event { get; set; }

        public string EventUrl
        {
            get
            {

                return Event.EventUrl;
            }
        }

        #region Event Info
        public string Title
        {
            get
            {

                return Event.Title;
            }
        }
        #endregion

        #region Event Time
        public bool AllDay
        {
            get
            {
                return Event.AllDayEvent.Checked;
            }
        }

        public DateTime Start
        {
            get
            {
                return Event.StartDate.DateTime;
            }
        }

        public DateTime End
        {
            get
            {
                return Event.EndDate.DateTime;
            }
        }

        public string EventTypeName
        {
            get
            {
                var eventType = Event.EventType.TargetItem;
                if (eventType != null)
                {
                    return eventType[Templates.Event.Fields.EventTypeName];
                }
                return "";
            }
        }

        public string EventTypeColor
        {
            get
            {
                var eventType = Event.EventType.TargetItem;
                if (eventType != null)
                {
                    return eventType[Templates.Event.Fields.EventTypeColor];
                }
                return "";
            }
        }



        #endregion
    }
}