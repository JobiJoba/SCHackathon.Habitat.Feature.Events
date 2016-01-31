# SCHackathon.Habitat.Feature.Events

#Intro

This module has been created for the Sitecore Hackathon 2016 http://sitecorehackathon.org/sitecore-hackathon-2016/

It follows the first idea to create a feature for Sitecore Habitat (3.0.319 night build)

#Installation 

* Install the package from the root/Sitecore Package/Sitecore Feature Events-2.0.zip
* Rebuild your indexes
* Rebuild your link databases
* Publish

The packages contains sample data as some part are quite fix right now.

#Purpose of the module & Feature

The idea is to let content editor create Events in Sitecore. An event is an item which have the following data

* Title
* Summary
* Body
* Image
* Type (A type for the event Business, Leasure, etc) 
* StartDate
* EndDate
* All day event (if no specified hours has been set)
* Location
    * Name
    * Address
    * City
    * Country
    * Postal Code
    * Phone
    * State or Province
    * Website Url
* Organizer
    * Name
    * Phone
    * Website Url
    * Email
* Website Url
* Currency Symbols
* Cost
* Social
* Facebook Url
* Twitter Url
* Google+ Url
* A Sitecore list associated to the event
* The fact that anonymous user can register or not

There are built-in view to manage those events: 

* Event List (Which display the list of events depending on the datasource - order by start date)
* Calendar view (Display a full calendar with the events - Choice of the color of the events depending on his type)
* Even Detail (Display a full detail page of the Event)
* Event Registration (Display a Textbox + a button to register to an event; if contact is already registered, will show a button unregister)

## Screenshot of built in components

Event List and Calendar List 

![Alt EventList and Calendar](.\ReadmeImage\EventListAndCalenderView.png)

Event Registration (when not registered) and Event detail 

![Alt Register and Detail](.\ReadmeImage\DetailAndRegister.png)

When you register, we will identify the contact with the email and so if he visit another event,
we will pre-fill the email field which he register before.

An event can be associate with a List (from the List Manager) which your content editor can then use
to send mass email or just to know how many people are registered to an event, etc. 

Four rules has been created to help your marketer to play with the events submission (to push an event or not for example).

* Where current visitor have not register to event since **X** months
* Where current visitor have participated to **x** events
* Where the current visitor is in a **specific list**
* Where the visitor have not participated to an event


#How to use it

After the installation of the package, two **root** folder will be created. 
One in Global, and another under Home; the two are called **Events** but they are different.

![Alt Sitecore Structure](.\ReadmeImage\sitecoreStructure.png)

One will contains **events** raw data (the one under global) and the other one is just an overview page containing
a **Wildcard event**. 

*Actually, the structure under home need to be respected*

You can create many-as-you-want Events under the **Global folder**, in fact it will be use by
the renderings as Datasource. So if you want multiple overview page to multiple events - It's possible.

**The Wildcard item should have an Event Detail Rendering with a datasource to an event folder or it crash, by default you cannot set a datasource from the Experience Editor (stupid refactoring ;) )** 

Create some Events in the Global folder and then use the Built-in renderings (or create your own - the EventRepository will help you)
to display those events. 

## Inspiration, Ressource and Thanks

I have use this wordpress module as inspiration for the feature: 
https://wordpress.org/plugins/the-events-calendar


* Thanks to Briand Pendersen for his blog post about the ContactRepository which I have taken and modify for the purpose of the project. https://briancaos.wordpress.com/ 
* Thanks to the bootstrap snippets website  http://snipplicious.com/  and http://bootsnipp.com/  

* Thanks to Full Calendar v2 which gave me headeaches ;)

## Futur improvement

* A refactoring will be necessary, there a still some dirty code their which I didn't had the time to correct.
* Create more built-in rendering
    * Who participated (Show participant of the events or the count)
    * Teasers
    * If logged "My Events" (Created, which he participated,...)
* Integrate it with an e-commerce system or add a better form for registration with payment methods, etc
* Add more feature like current event
* Let the users submit / edit events
* ... many more