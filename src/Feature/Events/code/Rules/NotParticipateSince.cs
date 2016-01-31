using Sitecore.Analytics;
using Sitecore.Diagnostics;
using Sitecore.Feature.Events.Repositories;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;

namespace Sitecore.Feature.Events.Rules
{

    //	Where current visitor have not register to event since X months
    public class NotParticipateSince<T> : WhenCondition<T> where T : RuleContext
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
            var tags = contactListRepository.GetTag(Tracker.Current.Contact, Context.Database, "ContactLists");
            if (tags == null)
            {
                return false;
            }
            // Sitecore Validator for Intenger
            var dateNowMinusNumber = DateTime.Now.AddMonths(-int.Parse(Number));
            DateTime max = DateTime.MinValue;
            foreach (var tag in tags.Values)
            {
                if(tag.DateTime > max)
                {
                    max = tag.DateTime;
                }
            }

            return max != DateTime.MinValue && max < dateNowMinusNumber;
            
        }
    }
}