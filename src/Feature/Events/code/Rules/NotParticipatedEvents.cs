using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Feature.Events.Repositories;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace Sitecore.Feature.Events.Rules
{

    //Where the visitor have not participated to an event
    public class NotParticipatedEvents<T> :  WhenCondition<T> where T : RuleContext
    {
        
        
        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(Tracker.Current, "Tracker.Current must be not null");
            Assert.IsNotNull(Tracker.Current.Contact, "Tracker.Current.Contact must be not null");
            if (Tracker.Current.Contact == null)
            {
                return false;
            }

            ContactListRepository contactListRepository = new ContactListRepository();
            var tag = contactListRepository.GetTag(Tracker.Current.Contact, Context.Database, "ContactLists");
            return (tag != null && tag.IsEmpty);
        }
    }
}