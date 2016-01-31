using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;

namespace Sitecore.Feature.Events.Model
{

    //Event is a reserved keyword ... :P 
    public class SitecoreEvent : BaseSitecoreEvent
    {

     

        #region Event Info
        public string Description { get; set; }

        public string Summary { get; set; }
        public ImageField Image { get; set; }



        #endregion

        #region Event Time
       
        #endregion
        #region Location
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public LinkField LocationWebsite { get; set; }
        #endregion

        #region Organizer
        public string OrganizerName { get; set; }

        public string OrganizerPhone { get; set; }
        public LinkField OrganizerWebsiteUrl { get; set; }

        public LinkField Email { get; set; }

        public LinkField EventWebsite { get; set; }
        #endregion

        #region Cost
        public string CurrencySymbols { get; set; }
        public string Cost { get; set; }
        #endregion

        #region Social
        public LinkField FacebookUrl { get; set; }
        public LinkField TwitterUrl { get; set; }
        public LinkField GoogleUrl { get; set; }

        #endregion

        #region Registration
        public ReferenceField ListName { get; set; }

       
        public CheckboxField AnonymousCanRegister { get; set; }
        #endregion



    }
}