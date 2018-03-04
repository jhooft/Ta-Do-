using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Requests;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using UserInteractions;

//using CalendarQuickstart;

namespace CalendarMaster
{
    class CalendarWork
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        //static string[] Scopes = { CalendarService.Scope.CalendarReadonly };//original code
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        internal CalendarService SetUpAccess() {/////////////////////////////////////////////////SetUpAccess

            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                //Console.WriteLine("Credential file saved to: " + credPath);
            }
            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;

        }//endmethod SetUpAccess

        internal int count_day_scheduled_events(CalendarService service, string calendarId, DateTime daystart, DateTime dayend)
        {
            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = daystart;
            request.TimeMax = dayend;
            Events events = request.Execute();
            return events.Items.Count;
    
        }
        internal int compute_average_daily_scheduled_events(CalendarService service, string calendarId, DateTime firstdaystart, DateTime lastdayend)
        {
            DateTime temp = firstdaystart;
            
            TimeSpan span = firstdaystart - lastdayend;
            int totaldays = span.Days;
            int totalhours = 0;
            while (temp < lastdayend){
                totalhours += count_day_scheduled_events(service, calendarId, temp, temp.AddDays(1));
            }
            return totalhours / totaldays;
        }

        internal void RequestAListOfEvents(CalendarService service, string calendarId) { 

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List(calendarId);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 20;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime; 

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();
        
        }// end method RequestAListOfEvents

        internal void query_trial(CalendarService service)
        {
            //CalendarQuery query = new CalendarQuery();
            //EventsResource.ListRequest tadolist = new EventsResource.ListRequest();
        }// end method query_trial

        internal void insert_calendar_trial( CalendarService service, Calendar calendar )
        {
            //EventsResource.InsertRequest request = new EventsResource.InsertRequest(service, CreateEvent(), "Ta-Do");
            //CalendarsResource.InsertRequest ins_cal_request = new CalendarsResource.InsertRequest(service, CreateTaDoCalendar());
            CalendarsResource.InsertRequest ins_cal_request = service.Calendars.Insert(calendar);//builds!!!!
            ins_cal_request.Execute();
            //Calendar secondarycal = service.Calendars.Insert(calendar).Execute();
            //IList<CalendarListEntry> list = service.CalendarList.List().Execute().Items;

            Console.WriteLine("the insert method ran");
        }// end method insert_trial

        internal void insert_event_trial( CalendarService service, HopperNode node)
        {
            EventsResource.InsertRequest ins_event_request = service.Events.Insert(CreateEvent(node), "Ta-Do");
            node.placed_on_calendar = true;
            Console.WriteLine("Ta-Do {0} has been placed on the calendar!", node.tado_title);
        }

        internal Event CreateEvent(HopperNode node)
        {
                      
            Event newevent = new Event
            {
                Summary = node.tado_title,
                Start = new EventDateTime
                {
                    DateTime = new DateTime(2018, 3, 4, 11, 0, 0)
                },
                End = new EventDateTime
                {
                    DateTime = new DateTime(2018, 3, 4, 11, 15, 0)
                }

            };
            return newevent;
        }
        

        internal void AddEventToCalendar(CalendarService service, Event newevent, string calId )
        {
            var newEventRequest = service.Events.Insert(newevent, calId);
            var eventResult = newEventRequest.Execute();
        }

        internal Calendar CreateTaDoCalendar() {
            Calendar my_tado_calendar = new Calendar();
            my_tado_calendar.Summary = "Ta-Do";
            my_tado_calendar.Id = "Ta-Do";//???
            Console.WriteLine("the create tado cal method ran");
            return my_tado_calendar;
        }// end method CreateTaDoCalendar

        
    }//end class CreatedCalendar
}

