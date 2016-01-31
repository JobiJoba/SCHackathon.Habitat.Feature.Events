using System.Collections.Generic;
using Sitecore.Analytics.Tracking;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Analytics.Model.Entities;

namespace Sitecore.Feature.Events.Repositories
{
    public interface IContactListRepository
    {
        void Add(Contact contact, Item listName);
        void Add(Contact contact, string listName, string sitecoreItemId);
        IEnumerable<Item> GetTags(Contact contact, Database database, string id, string contactList = "ContactLists");
        ITag GetTag(Contact contact, Database database, string contactList = "ContactLists");

        void Remove(Contact contact, Item listName);
        void Remove(Contact contact, string listName, string sitecoreItemId);
    }
}