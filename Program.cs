
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CalendarMaster;
using UserInteractions;


namespace CalendarQuickstart
{
    class Program
    {
        

        static void Main(string[] args)
        {

            CalendarWork working_calendar = new CalendarWork();
            CalendarService working_calendar_service = working_calendar.SetUpAccess();
            var list = working_calendar_service.CalendarList.List().Execute();
            var cal_to_use = list.Items.SingleOrDefault(c => c.Summary == "johooft@gmail.com");
            
            //Calendar tadocal = working_calendar.CreateTaDoCalendar();            
            //working_calendar.insert_calendar_trial( working_calendar_service, tadocal );        
            //working_calendar.RequestAListOfEvents(working_calendar_service, "primary");//list of next 20 events from primary calendar
            //working_calendar.RequestAListOfEvents(working_calendar_service, tadocal.Id);//fails

            //***some user interrupt has resulted in entering quick-input/to-do string...simulated by this prompt:
            Console.WriteLine("What's your Ta-Do?");
                     
            HopperNode newtado = new HopperNode( Console.ReadLine() );
            Event myevent = working_calendar.CreateEvent(newtado);
            working_calendar.AddEventToCalendar(working_calendar_service, myevent, cal_to_use.Id);
            
            //working_calendar.insert_event_trial(working_calendar_service, newtado);


        }
    }
}