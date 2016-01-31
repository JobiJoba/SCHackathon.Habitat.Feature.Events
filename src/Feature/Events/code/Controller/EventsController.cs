namespace Sitecore.Feature.Events.Controller
{
    using System.Web.Mvc;
    using Repositories;
    using Mvc.Presentation;
    using ViewModels;
    using Model;
    using System.Linq;
    using Data;
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Sites;
    using System.Web;
    using Links;
    using Analytics;
    using Mvc.Controllers;
    using Analytics.Model.Entities;
    using Foundation.SitecoreExtensions.Repositories;
    public class EventsController : SitecoreController
    {
        private IEventsRepository _eventsRepository;
        private IContactListRepository _contactListRepository;

        public EventsController()
        {
            try
            {
                if (!Tracker.IsActive)
                {
                    Tracker.StartTracking();
                }

                this._contactListRepository = new ContactListRepository();

                this._eventsRepository = new EventsRepository(RenderingContext.Current.Rendering.Item);
            }
            catch (Exception)
            {
                // RenderingContext does not like to be called from Javascript :< That's really a dirty things
            }
        }

        public EventsController(IEventsRepository eventsRepository, IContactListRepository contactListRepository)
        {
            this._eventsRepository = eventsRepository;
            this._contactListRepository = contactListRepository;

        }

        public ActionResult EventList()
        {
            // just a protection to do not display the empty one (where at the end) - I could have put Sitecore Validation but i really don't like it :)
            var items = this._eventsRepository.Get().Select(sitecoreEvent => MapToEventViewModel(sitecoreEvent)).Where(o => !string.IsNullOrEmpty(o.Event.Title) && !string.IsNullOrEmpty(o.EventTypeName));
            return this.View("EventList", items);
        }


        public ActionResult EventListCalendar()
        {
            Calendar calendar = new Calendar()
            {
                ItemId = RenderingContext.Current.Rendering.Item.ID.ToString(),
                DatabaseName = Sitecore.Context.Database.Name,
                SiteName = Sitecore.Context.Site.Name,
                SiteUrlToEvents = LinkManager.GetItemUrl(RenderingContext.Current.PageContext.Item)
            };
            return View("EventListCalendar", calendar);
        }

        public string EventListCalendarData(string Id, string Database, string SiteName)
        {
            //The context here is not set; I'm getting info that I need via parameters
            ID sitecoreID;

            if (!ID.TryParse(Id, out sitecoreID))
            {
                return null;
            }
            List<EventMinifiedViewModel> items = null;
            using (new SiteContextSwitcher(SiteContextFactory.GetSiteContext(SiteName)))
            {

                this._eventsRepository = new EventsRepository(Sitecore.Configuration.Factory.GetDatabase(Database).GetItem(sitecoreID));
                items = this._eventsRepository.GetMinified().Select(sitecoreEvent => MapToEventMinifiedViewModel(sitecoreEvent)).ToList();
            }

            return JsonConvert.SerializeObject(items);

        }


        public ActionResult EventDetail()
        {
            SitecoreEvent sitecoreEvent = EventFromUrl();
            return this.View("EventDetail", MapToEventViewModel(sitecoreEvent));
        }

        [HttpPost]
        public ActionResult UnregisterToEvent(string listname, string sitecoreitemid)
        {

            //remove it from the contactList
            _contactListRepository.Remove(Tracker.Current.Contact, listname, sitecoreitemid);
            

            // return to the EventRegistration
            return base.Index();


        }

        public ActionResult RegisterToEventAdd(string listname, string sitecoreitemid, string email)
        {

            if (Tracker.Current.Contact.Identifiers.IdentificationLevel == Analytics.Model.ContactIdentificationLevel.None)
            {
                //Add it
                Tracker.Current.Session.Identify(email);

            }
            var emails = GetEmailFacetFromContact();
            if (emails != null)
            {
                emails.Preferred = "Primary";
                if (emails.Entries.Contains("Primary"))
                {
                    emails.Entries["Primary"].SmtpAddress = email;
                }
                else
                {
                    emails.Entries.Create("Primary").SmtpAddress = email;
                }
            }
            _contactListRepository.Add(Tracker.Current.Contact, listname, sitecoreitemid);
            

            // return to the EventRegistration
            return base.Index();


        }

        // If already identified ; No need for a form. 
        // If not identified; we will ask him for some information
        public ActionResult RegisterToEvent()
        {
            SitecoreEvent sitecoreEvent = EventFromUrl();
            var listItem = sitecoreEvent.ListName.TargetItem;
            if (listItem == null)
            {
                return View("NoListAttached");
            }



            ListContact listContact = new ListContact()
            {
                ListName = listItem[Templates.Event.Fields.ContactListName],
                SitecoreItemId = listItem.ID.ToString()
            };
            if (Tracker.Current != null)
            {
                var listTag = _contactListRepository.GetTags(Tracker.Current.Contact, Context.Database, listItem.ID.ToString());



                if (listTag.Any())
                {

                    // if empty we do not propose him to register - he is already register. 
                    return this.View("EventAlreadyRegistered", listContact);


                }
                else
                {
                    
                    var emailFacet = GetEmailFacetFromContact();

                    // We propose him to register
                    if (emailFacet.Entries.Contains("Primary"))
                    {
                        listContact.Email = emailFacet.Entries["Primary"].SmtpAddress;
                    }
                    return this.View("EventRegistration", listContact);
                }

            }
            else
            {
                //Tracker is null or you don't track the site; or you are on the Experience Editor

                return Sitecore.Context.PageMode.IsExperienceEditor ? 
                    View("NoTracker",DictionaryRepository.Get("/Events/Event Registration/Experience Editor", "You are in the page Editor, No Tracker")) :
                    this.View("NoTracker",string.Empty);
            }

        }


        private SitecoreEvent EventFromUrl()
        {
            // Get the Event from the URL name
            // Decode in case of space ; Should normally replace space by - but habitat does not do it right now ;)
            var eventName = HttpUtility.UrlDecode(HttpContext.Request.Url.Segments.LastOrDefault());

            var sitecoreEvent = this._eventsRepository.Get(eventName);
            return sitecoreEvent;
        }

        private IContactEmailAddresses GetEmailFacetFromContact()
        {
            return Tracker.Current.Contact.GetFacet<IContactEmailAddresses>("Emails");
        }
        private EventViewModel MapToEventViewModel(SitecoreEvent sitecoreEvent)
        {
            EventViewModel eventViewModel = new EventViewModel()
            {
                Event = sitecoreEvent
            };
            return eventViewModel;
        }

        private EventMinifiedViewModel MapToEventMinifiedViewModel(BaseSitecoreEvent sitecoreEventMinified)
        {
            EventMinifiedViewModel eventMinifiedViewModel = new EventMinifiedViewModel()
            {
                Event = sitecoreEventMinified
            };
            return eventMinifiedViewModel;
        }
                
    }
}