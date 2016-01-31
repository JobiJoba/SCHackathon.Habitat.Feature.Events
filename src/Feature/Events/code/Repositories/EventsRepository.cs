namespace Sitecore.Feature.Events.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using News.Repositories; //That's an error in the code of Habitat --> wrong namespace in ISearchServiceRepository 
    using Model;
    using ContentSearch;
    using ContentSearch.SearchTypes;
    using ContentSearch.Linq;
    public class EventsRepository : IEventsRepository
    {
        public Item ContextItem { get; set; }

        private readonly ISearchServiceRepository searchServiceRepository;

        public EventsRepository(Item contextItem) : this(contextItem, new SearchServiceRepository(new SearchSettingsRepository())) { }

        public EventsRepository(Item contextItem, ISearchServiceRepository searchServiceRepository)
        {
            if (contextItem == null)
            {
                throw new ArgumentNullException(nameof(contextItem));
            }
            if (!contextItem.IsDerived(Templates.EventsFolder.ID))
            {
                throw new ArgumentException("Item must derive from EventsFolder", nameof(contextItem));
            }
            this.ContextItem = contextItem;

            this.searchServiceRepository = searchServiceRepository;
        }

        public IEnumerable<SitecoreEvent> Get()
        {
            var searchService = this.searchServiceRepository.Get();

            searchService.Settings.Root = this.ContextItem;
            var results = searchService.FindAll();
            return results.Results.Select(x => MapToSitecoreEvent(x.Item)).OrderBy(i => i.StartDate.DateTime);
        }

        public SitecoreEvent Get(string itemName)
        {
            //I should use the foundation index but not enough time
            using (var providerSearchContext = ContentSearchManager.GetIndex((SitecoreIndexableItem)Context.Item).CreateSearchContext())
            {
                var query = providerSearchContext.GetQueryable<SearchResultItem>().Where(o => o.TemplateId == Templates.Event.ID && o.Language == Sitecore.Context.Language.Name
                && o.Name == itemName);

                var result = query.GetResults();

                if (result.TotalSearchResults > 0)
                {
                    var i = result.Hits.Select(o => o.Document.GetItem()).FirstOrDefault();
                    return MapToSitecoreEvent(i);

                }
            }
            return null;
        }

        public IEnumerable<BaseSitecoreEvent> GetMinified()
        {
            var searchService = this.searchServiceRepository.Get();
            searchService.Settings.Root = this.ContextItem;
            var item = Context.Item;
            Context.Item = this.ContextItem;
            var results = searchService.FindAll();
            Context.Item = item;
            return results.Results.Select(x => MapToSitecoreEventMinified(x.Item)).OrderBy(i => i.StartDate.DateTime);

        }


        private BaseSitecoreEvent MapToSitecoreEventMinified(Item item)
        {

            BaseSitecoreEvent sitecoreEventMinified = new BaseSitecoreEvent();
            sitecoreEventMinified.AllDayEvent = item.Fields[Templates.Event.Fields.AllDayEvent];
            sitecoreEventMinified.StartDate = item.Fields[Templates.Event.Fields.StartDate];
            sitecoreEventMinified.EndDate = item.Fields[Templates.Event.Fields.EndDate];
            sitecoreEventMinified.Title = item[Templates.Event.Fields.Title];
            sitecoreEventMinified.EventType = item.Fields[Templates.Event.Fields.EventType];
            sitecoreEventMinified.Item = item;

            return sitecoreEventMinified;

        }
        private SitecoreEvent MapToSitecoreEvent(Item item)
        {
            SitecoreEvent sitecoreEvent = new SitecoreEvent();

            sitecoreEvent.Address = item[Templates.Event.Fields.Address];
            sitecoreEvent.AllDayEvent = item.Fields[Templates.Event.Fields.AllDayEvent];
            sitecoreEvent.City = item[Templates.Event.Fields.City];
            sitecoreEvent.Cost = item[Templates.Event.Fields.Cost];
            sitecoreEvent.CurrencySymbols = item[Templates.Event.Fields.CurrencySymbols];
            sitecoreEvent.Description = item[Templates.Event.Fields.Body];
            sitecoreEvent.Email = item.Fields[Templates.Event.Fields.OrganizerEmail];
            sitecoreEvent.EndDate = item.Fields[Templates.Event.Fields.EndDate];
            sitecoreEvent.EventWebsite = item.Fields[Templates.Event.Fields.Url];
            sitecoreEvent.FacebookUrl = item.Fields[Templates.Event.Fields.FacebookUrl];
            sitecoreEvent.GoogleUrl = item.Fields[Templates.Event.Fields.GoogleUrl];
            sitecoreEvent.Image = item.Fields[Templates.Event.Fields.Image];
            sitecoreEvent.LocationName = item[Templates.Event.Fields.Name];
            sitecoreEvent.LocationWebsite = item.Fields[Templates.Event.Fields.Website];
            sitecoreEvent.OrganizerName = item[Templates.Event.Fields.OrganizerName];
            sitecoreEvent.OrganizerPhone = item[Templates.Event.Fields.Phone];
            sitecoreEvent.PostalCode = item[Templates.Event.Fields.PostalCode];
            sitecoreEvent.StartDate = item.Fields[Templates.Event.Fields.StartDate];
            sitecoreEvent.StateOrProvince = item[Templates.Event.Fields.StateOrProvince];
            sitecoreEvent.Summary = item[Templates.Event.Fields.Summary];
            sitecoreEvent.Title = item[Templates.Event.Fields.Title];
            sitecoreEvent.TwitterUrl = item.Fields[Templates.Event.Fields.TwitterUrl];
            sitecoreEvent.EventType = item.Fields[Templates.Event.Fields.EventType];
            sitecoreEvent.ListName = item.Fields[Templates.Event.Fields.ListName];
            sitecoreEvent.Item = item;




            return sitecoreEvent;
        }
    }
}