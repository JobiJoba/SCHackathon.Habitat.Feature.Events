using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Feature.Events.Repositories;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System.Linq;

namespace Sitecore.Feature.Events.Rules
{

    //Where current visitor have participated to x events
    public class ParticipateAtLeastAtX<T> : WhenCondition<T> where T : RuleContext
    {
        public string Number
        {
            get;
            set;
        }

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
            var tag =  contactListRepository.GetTag(Tracker.Current.Contact, Context.Database, "ContactLists");

            // Sitecore Validator for integer
            return (tag != null && tag.Values.Count() >= int.Parse(Number));
        }
    }
}