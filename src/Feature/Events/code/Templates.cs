namespace Sitecore.Feature.Events
{
    using Sitecore.Data;

    public struct Templates
    {
        public struct Event
        {
            public static ID ID = new ID("{B5E560C8-47E6-4003-A328-F38C21918118}");

            public struct Fields
            {

                #region Section Event
                public static readonly ID Title = new ID("{77C741E9-1903-4A10-BD03-DA34E5F250C7}");
                public const string Title_FieldName = "Title";

                public static readonly ID Summary = new ID("{14EE151E-888E-4439-B579-5D770DB65261}");
                public const string Summary_FieldName = "Summary";

                public static readonly ID Body = new ID("{AB750082-8359-4292-B65A-A868CD9225D0}");
                public const string Body_FieldName = "Body";

                public static readonly ID Image = new ID("{719EDCD6-E661-45D8-8863-F831CB15D794}");

                public static readonly ID EventType = new ID("{BE93D0D4-EB2E-40DD-8D50-C206E19DFDEA}");
                public static readonly ID EventTypeName = new ID("{B2CDAB6A-D797-44EB-9077-2F7A0649EC4F}");
                public static readonly ID EventTypeColor = new ID("{CD7D03A9-2655-4EEA-B1A9-A1C01D42D9A6}");


              
                #endregion

                #region Event Time

                public static readonly ID AllDayEvent = new ID("{07C65E4D-AAAF-4939-A0DD-60EF03FBF7A3}");
                public static readonly ID StartDate = new ID("{4295278A-D0C1-4300-83EE-6CECD0532A29}");
                public static readonly ID EndDate = new ID("{62BACDE3-3D73-44AB-8FE4-00D881F93EAE}");
                #endregion

                #region Location

                public static readonly ID Name = new ID("{E0B11314-F256-41E2-8AC8-191BA1194D73}");
                public static readonly ID Address = new ID("{1294B1C6-1627-4FCD-885B-F19979D03002}");
                public static readonly ID City = new ID("{E5D990E3-5776-4109-B394-39804086D640}");
                public static readonly ID Country = new ID("{6F89AB58-BCCB-4647-8A9F-C55F4BFB55CA}");
                public static readonly ID StateOrProvince = new ID("{F318496C-AE01-4007-8BA4-35DC8991169F}");
                public static readonly ID PostalCode = new ID("{1613F513-762C-47BF-BBD2-28B2EECFA6A4}");
                public static readonly ID Phone = new ID("{455582DC-D6DB-4E3B-A451-8713A0C82CDD}");
                public static readonly ID Website = new ID("{D287832C-7443-40FA-A3A9-60975387D8E6}");
                #endregion

                #region Organizer

                public static readonly ID OrganizerName = new ID("{372357CC-0EAA-4B79-926C-5E6F37E0678A}");
                public static readonly ID OrganizerPhone = new ID("{8C52CD89-6EA5-43F2-87C0-B99EF21B3215}");
                public static readonly ID OrganizerWebsite = new ID("{A290EC24-17D5-4BEE-B43C-54A807CF0916}");
                public static readonly ID OrganizerEmail = new ID("{31E2ED69-E64F-4328-9095-B3E174981BAF}");
                #endregion


                #region Website

                public static readonly ID Url = new ID("{65B97CD3-72AF-47CB-ABB9-018E3E85CB81}");
                #endregion


                #region Website

                public static readonly ID CurrencySymbols = new ID("{02FB5A12-DD09-43FF-89B7-B1AF187098CD}");
                public static readonly ID Cost = new ID("{1B6D2F9C-AC9F-4428-8BB2-45EA2E7EAF2F}");
                #endregion

                #region Social

                public static readonly ID FacebookUrl = new ID("{87D7DB6A-8D12-417F-9316-DA169C761848}");
                public static readonly ID TwitterUrl = new ID("{B03F50AA-ED8C-49EF-9904-EECE87ED62B5}");
                public static readonly ID GoogleUrl = new ID("{27BA1BC5-F103-419A-9E80-DD1D9F89A949}");
                #endregion

                #region Registration

                public static readonly ID ListName = new ID("{B12B55A2-A3B5-47D3-9529-013FCBCBB1CA}");
                public static readonly ID ContactListName = new ID("{55A40513-EB5E-4411-9128-38C2CAEC0C62}");
                public static readonly ID AnonymousCanRegister = new ID("{A358BDCA-3339-41F1-8F2E-B004461F7DC0}");

                

            #endregion

        }
        }

        public struct EventsFolder
        {
            public static readonly ID ID = new ID("{7861B223-4F41-4D03-9FFC-A8D5F050819D}");
        }
    }
}