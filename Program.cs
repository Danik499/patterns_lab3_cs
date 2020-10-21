using System;

namespace c_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter format");
            String format = Console.ReadLine();
            Console.WriteLine("enter type of event");
            String eventType = Console.ReadLine();
            String startTime = "", endTime = "", venue = "", date = "", contacts = "";

            Event UsersEvent;

            if (eventType == "meeting")
            {
                Console.WriteLine("enter start time");
                startTime = Console.ReadLine();
                Console.WriteLine("enter end time");
                endTime = Console.ReadLine();
                Console.WriteLine("enter venue");
                venue = Console.ReadLine();
                UsersEvent = new Meeting(startTime, endTime, venue);
            }
            else
            {
                Console.WriteLine("enter date");
                date = Console.ReadLine();
                Console.WriteLine("enter contacts");
                contacts = Console.ReadLine();
                UsersEvent = new Birthday(date, contacts);
            }

            Output output = UsersEvent.ToConsole(format);
            output.Print(startTime, endTime, venue, date, contacts, eventType);
        }
    }
    abstract class Output
    {
        public abstract void Print(string startTime, string endTime, string venue, string date, string contacts, string eventType);
    }
    class JSON : Output
    {
        public override void Print(string startTime, string endTime, string venue, string date, string contacts, string eventType)
        {
            Console.WriteLine("{");
            if (eventType == "meeting")
            {
                Console.WriteLine("    \"startTime\": \"{0}\"", startTime);
                Console.WriteLine("    \"endTime\": \"{0}\"", endTime);
                Console.WriteLine("    \"venue\": \"{0}\"", venue);
            }
            else
            {
                Console.WriteLine("    \"date\": \"{0}\"", date);
                Console.WriteLine("    \"contacts\": \"{0}\"", contacts);
                Console.WriteLine("    \"eventType\": \"{0}\"", eventType);
            }
            Console.WriteLine("}");
        }
    }
    class XML : Output
    {
        public override void Print(string startTime, string endTime, string venue, string date, string contacts, string eventType)
        {
            Console.WriteLine("<?xml version=\"1.0\">");
            if (eventType == "meeting")
            {
                Console.WriteLine("<startTime>{0}</startTime>", startTime);
                Console.WriteLine("<endTime>{0}<endTime>", endTime);
                Console.WriteLine("<venue>{0}</venue>", venue);
            }
            else
            {
                Console.WriteLine("<date>{0}</date>", date);
                Console.WriteLine("<contacts>{0}</contacts>", contacts);
            }
        }
    }

    abstract class Event
    {
        public abstract Output ToConsole(String type);
    }

    class Meeting : Event
    {
        String startTime, endTime, venue;
        public Meeting(String startTime, String endTime, String venue)
        {
            this.startTime = startTime;
            this.endTime = endTime;
            this.venue = venue;
        }
        public override Output ToConsole(String type)
        {
            if (type == "JSON")
            {
                return new JSON();
            }
            return new XML();
        }
    }

    class Birthday : Event
    {
        String date, contacts;
        public Birthday(String date, String contacts)
        {
            this.date = date;
            this.contacts = contacts;
        }

        public override Output ToConsole(string type)
        {
            if (type == "JSON")
            {
                return new JSON();
            }
            return new XML();
        }
    }
}
