using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Diagnostics;
using Sitecore.Feature.Events.Repositories;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Events.Rules
{

    //Where the current visitor is in a specific list
    public class SpecificList<T> :  WhenCondition<T> where T : RuleContext
    {
        public string List
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

            var tag = contactListRepository.GetTags(Tracker.Current.Contact, Context.Database,List);
            
            return tag.Any();
        }
    }
}