using System;
using System.Collections.Generic;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace Sitecore.Feature.Events.Repositories
{

    // Taken from Brian and adapted a little bit (Just the string listName, create an interface, etc) 
    // I would have choose the ContactProfileServices if it was in foundation instead of Accounts 
    public class ContactListRepository : IContactListRepository
    {

        public IEnumerable<Item> GetTags(Contact contact, Database database, string id, string contactList = "ContactLists")
        {

            ITag tag = GetTag(contact, database, contactList);


            if (tag == null)
                yield break;

            foreach (ITagValue tagValue in tag.Values)
            {
                if (tagValue.Value.Equals(id))
                    yield return database.GetItem(tagValue.Value);
            }

        }

        public ITag GetTag(Contact contact, Database database, string contactList = "ContactLists")
        {
            return contact.Tags.Find(contactList);
        }



        public void Add(Contact contact, Item listName)
        {
            this.Add(contact, listName[Templates.Event.Fields.ContactListName], listName.ID.ToString());
        }


        public void Add(Contact contact, string item, string id)
        {
            using (new SecurityDisabler())
            {

                contact.Tags.Set("ContactLists", id);

            }
        }

        public void Remove(Contact contact, Item item)
        {

            this.Remove(contact, item[Templates.Event.Fields.ContactListName], item.ID.ToString());

        }

        public void Remove(Contact contact, string item, string id)
        {
            using (new SecurityDisabler())
            {

                contact.Tags.Remove("ContactLists", id);

            }
        }

      
    }
}