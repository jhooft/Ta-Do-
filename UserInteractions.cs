using System;
using CalendarMaster;
using System.Threading;



namespace UserInteractions
{

    class UserInteract
    {
        int bug_frequency;
        int dailyhour;//single hour of 24hr day for bugging to occur
        int dailyhour1;//1st of two bugs
        int dailyhour2;//2nd of two bugs
        int hour1, hour2, hour3, hour4, hour5, hour6;//hours for hourly bugs

        ////If user activates app, to-do creation/maintenance gets called. Otherwise, maintenance is done by app "bugging" the user.

        //Settings for  how often/times of day  app should bug user.
        //variables keeping track of when to bug

        internal bool ready_to_bug = true; //we assume for simplicity

        internal void set_bug_frequency()
        {
            Console.WriteLine("How often is it OK to bug you? (choose a number):");
            Console.WriteLine("1.Once a day.");
            Console.WriteLine("2.Twice a day.");
            Console.WriteLine("3.Once an hour.");
            bug_frequency = Convert.ToInt32(Console.ReadLine());
        }
        internal void set_bug_timer()
        {
            if (bug_frequency == 1)
            {
                Console.WriteLine("Are you more of a morning person... or afternoon? (enter am or pm)");
                string timeoday = Console.ReadLine();
                if (timeoday == "am")
                {
                    dailyhour = 10;
                }
                else if(timeoday == "pm")
                {
                    dailyhour = 14;
                }
                //TimerCallback callback = new TimerCallback(object state);
                //var dailytimer = new Timer(callback);
                //DateTime ten_am = DateTime.Today.AddHours(10);
                //DateTime two_pm = DateTime.Today.AddHours(14);
                //////set up timer- triggered events

            }
            else if(bug_frequency == 2)
            {
                dailyhour1 = 10;
                dailyhour2 = 14;
                //set timer
            }
            else//bug_frequency == 3
            {
                //set hours
                //set timer
            }
        }



    }// end class UserInteract

    class InputHopper  /////////////////////////////////////////////////////////////////////////////
    {

        internal InputHopper() { }

        HopperNode[] Priority10 = new HopperNode[100];//Highest priority to-dos
        int P10index;//array index at which most recently appended???
        HopperNode[] Priority9 = new HopperNode[100];
        int P9index;
        HopperNode[] Priority8 = new HopperNode[100];
        int P8index;
        HopperNode[] Priority7 = new HopperNode[100];
        int P7index;
        HopperNode[] Priority6 = new HopperNode[100];
        int P6index;
        HopperNode[] Priority5 = new HopperNode[100];
        int P5index;
        HopperNode[] Priority4 = new HopperNode[100];
        int P4index;
        HopperNode[] Priority3 = new HopperNode[100];
        int P3index;
        HopperNode[] Priority2 = new HopperNode[100];
        int P2index;
        HopperNode[] Priority1 = new HopperNode[100];//Lowest priority to-dos
        int P1index;
        HopperNode[] Unprioritized = new HopperNode[100];//No priority assigned
        int PUindex;

        
        internal void AddHopperNodeToArray(HopperNode node)
        {
            switch (node.priority)
            {
                case 10: Priority10[P10index] = node; P10index++;  break;
                case 9: Priority9[P9index] = node; P9index++; break;
                case 8: Priority8[P8index] = node; P8index++; break;
                case 7: Priority7[P7index] = node; P7index++; break;
                case 6: Priority6[P6index] = node; P6index++; break;
                case 5: Priority5[P5index] = node; P5index++; break;
                case 4: Priority4[P4index] = node; P4index++; break;
                case 3: Priority3[P3index] = node; P3index++; break;
                case 2: Priority2[P2index] = node; P2index++; break;
                case 1: Priority1[P1index] = node; P1index++; break;
                
            }
        }

        /*internal void InitialToDoEntry()      //For now this is read from typed user input.  Makes a to-do event and inserts in hopper.
        {
            string input = Console.ReadLine().ToString();
            HopperNode new_entry = new HopperNode(input);

        }*/

    }// end class InputHopper

    class HopperNode      ////////////////////////////////////////////////////////////////////////////
    {
        internal string tado_title;
        internal int priority;
        internal double hours_til_deadline;
        internal DateTime entry_date_time;
        internal int entry_day;
        internal int entry_month;
        internal int entry_year;
        internal int entry_hour;
        internal int entry_minute;
        internal DateTime deadline;
        internal int deadline_day;
        internal int deadline_month;
        internal int deadline_year;
        internal int deadline_hour;
        internal int deadline_minute;
        internal string length_at_entry;//long term or short term as entered by user
        internal bool placed_on_calendar;//does the Ta-Do item have a place on the calendar?

        internal HopperNode(string user_entry)//To-do event creation by user input. HOPPERNODE CONSTRUCTOR
        {
            entry_date_time = DateTime.Now;
            entry_day = entry_date_time.Day;
            entry_month = entry_date_time.Month;
            entry_year = entry_date_time.Year;
            entry_hour = entry_date_time.Hour;
            entry_minute = entry_date_time.Minute;
            tado_title = user_entry;
            Console.WriteLine("Want to add info right now?? (yes/no)");
            char ans = Console.ReadLine()[0];
            if (ans == 'y')
            {
                GetPriorityFromUser();
                GetDeadlineFromUser();
                Console.WriteLine("Got It! You need to do {0} by {1}!", tado_title, deadline);
            }
            else
            {
                Console.WriteLine("Got it!");
            }
        }//end HopperNode constructor

        void GetPriorityFromUser()
        {
            Console.Write("On a scale of 1 to 10, with 10 the highest, HOW IMPORTANT IS THIS TASK??");
            int priorityans = Convert.ToInt32(Console.ReadLine());
            if (priorityans >= 10)
            {
                priority = 10;
            }
            else if (priorityans <= 1)
            {
                priority = 1;
            }
            else
            {
                switch (priorityans)
                {
                    case 9: priority = 9; break;
                    case 8: priority = 8; break;
                    case 7: priority = 7; break;
                    case 6: priority = 6; break;
                    case 5: priority = 5; break;
                    case 4: priority = 4; break;
                    case 3: priority = 3; break;
                    case 2: priority = 2; break;
                }
            }
        }// end method GetPriorityFromUser

                void GetDeadlineFromUser()
                {
                    Console.WriteLine("Is there a hard deadline on this?");
                    string deadlineans = Console.ReadLine();
                    if (deadlineans == "yes" || deadlineans == "y" || deadlineans == "Y")
                    {
                        Console.WriteLine("Enter deadline as mm/dd/yyyy");
                        string dateans = Console.ReadLine();
                        string[] datevec = dateans.Split('/');
                        deadline_month = Convert.ToInt32(datevec[0]);
                        deadline_day = Convert.ToInt32(datevec[1]);
                        deadline_year = Convert.ToInt32(datevec[2]);
                        deadline = new DateTime(deadline_year, deadline_month, deadline_day, 17, 0, 0); //default deadline time is 5pm

                    }
                    else
                    {
                        Console.WriteLine("Is this a short-term or long-term to-do?");
                        length_at_entry = Console.ReadLine();
                    }
                    //Time of deadline????
                }//end method 
        internal void ComputeHoursTilDeadline()
        {
            TimeSpan span = deadline - DateTime.Now;
            hours_til_deadline = span.Days * 24 + span.Hours;
            
        }

    

    }// end class HopperNode
}//end namespace UserInteractions