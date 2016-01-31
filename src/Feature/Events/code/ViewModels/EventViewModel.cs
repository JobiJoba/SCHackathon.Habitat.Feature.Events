using Sitecore.Collections;
using Sitecore.Feature.Events.Model;
using Sitecore.Foundation.SitecoreExtensions.Repositories;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.Web.UI.WebControls;
using System;
using System.Web;

namespace Sitecore.Feature.Events.ViewModels
{
    public class EventViewModel
    {
        public SitecoreEvent Event { get; set; }



        #region Event Info
        public HtmlString Title
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Title.ToString()));
            }

        }

        public HtmlString Body
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Body.ToString()));
            }

        }

        public HtmlString Summary
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Summary.ToString()));
            }

        }

        public HtmlString Image
        {
            get
            {
                //TODO: Maybe we can put some parameters; depend on the style
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Image.ToString(), "mw=1000&class=img-responsive image"));
            }

        }

        public string ImageUrl
        {
            get
            {
                if(Event.Image.MediaItem == null)
                {
                    return "#";
                }
                return MediaManager.GetMediaUrl(Event.Image.MediaItem);
            }

        }

        #endregion

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



        #region Event Date

        public HtmlString ConcatDateIfNeeded()
        {

            if (Event.AllDayEvent.Checked)
            {
                if (Sitecore.Context.PageMode.IsExperienceEditor)
                {
                    return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StartDate.ToString()) + " - " + FieldRenderer.Render(Event.Item, Templates.Event.Fields.EndDate.ToString()));
                }

                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StartDate.ToString()));
            }

            //Case when somewone forget to check the All day but specify a StartDate but not an EndDate
            if (Event.StartDate.DateTime != DateTime.MinValue && Event.EndDate.DateTime == DateTime.MinValue)
            {
                if (Sitecore.Context.PageMode.IsExperienceEditor)
                {
                    return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StartDate.ToString()) + " - " + FieldRenderer.Render(Event.Item, Templates.Event.Fields.EndDate.ToString()));
                }
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StartDate.ToString()));
            }


            if (Event.StartDate.DateTime != DateTime.MinValue && Event.EndDate.DateTime != DateTime.MinValue)
            {

                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StartDate.ToString()) + " - " + FieldRenderer.Render(Event.Item, Templates.Event.Fields.EndDate.ToString()));
            }



            // In case no date are specified; every day is the default
            return new HtmlString(DictionaryRepository.Get("/Events/Date/Every Day", "Every Day"));

        }

        public HtmlString StartDate
        {
            get
            {

                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StartDate.ToString()) );
            }

        }
        public HtmlString EndDate
        {
            get
            {

                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.EndDate.ToString()));
            }

        }
        #endregion

        #region Location
        public HtmlString LocationName
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Name.ToString()));
            }
        }
        public HtmlString Address
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Address.ToString()));
            }
        }
        public HtmlString City
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.City.ToString()));
            }
        }
        public HtmlString StateOrProvince
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.StateOrProvince.ToString()));
            }
        }
        public HtmlString PostalCode
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.PostalCode.ToString()));
            }
        }
        public HtmlString Phone
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Phone.ToString()));
            }
        }

        public HtmlString LocationUrl
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Website.ToString()));
            }
        }

        #endregion

        #region Organizer
        public HtmlString OrganizerName
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.OrganizerName.ToString()));
            }
        }

        public HtmlString OrganizerPhone
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.OrganizerPhone.ToString()));
            }
        }

        public HtmlString OrganizerWebsite
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.OrganizerWebsite.ToString()));
            }
        }

        public HtmlString OrganizerEmail
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.OrganizerEmail.ToString()));
            }
        }
        
        #endregion

        #region Cost
        public HtmlString CurrencySymbols
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.CurrencySymbols.ToString()));
            }
        }
        public HtmlString Cost
        {
            get
            {
                return new HtmlString(FieldRenderer.Render(Event.Item, Templates.Event.Fields.Cost.ToString()));
            }
        }
        #endregion

    }
}