using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = System.IO.File.ReadAllLines(@"..\..\..\input.txt");

            List<LogEntry> sortedlog = new List<LogEntry>();
            Dictionary<int, Guard> GuardLookup = new Dictionary<int, Guard>();
            foreach (string input in inputs)
            {
                sortedlog.Add(new LogEntry(input));
            }
            sortedlog.Sort(delegate (LogEntry l1, LogEntry l2) { return l1.TimeStamp.CompareTo(l2.TimeStamp); });

            int previousID = 0;
            foreach(LogEntry le in sortedlog)
            {
                if (le.Message.Contains("wakes up"))
                {
                    GuardLookup[previousID].WakesUp(le.TimeStamp);
                }
                else if (le.Message.Contains("falls asleep"))
                {
                    GuardLookup[previousID].FallsAsleep(le.TimeStamp);
                }
                else
                {
                    Regex findID = new Regex(@".*#(\d*).*");
                    MatchCollection mc = findID.Matches(le.Message);
                    foreach(Match m in mc)
                    {
                        GroupCollection g = m.Groups;
                        previousID = int.Parse(g[1].Value);
                        if (!GuardLookup.ContainsKey(previousID))
                        { 
                            GuardLookup.Add(previousID, new Guard(previousID));
                        }
                    }
                }
            }
            Guard SleepsTheMost = null;
            Guard MostConsistent = null;
            MostSleptMinute msm = new MostSleptMinute(0, 0);
            foreach (KeyValuePair<int,Guard> kvp in GuardLookup)
            {               
                if (SleepsTheMost == null)
                {
                    SleepsTheMost = kvp.Value;
                }
                else if (SleepsTheMost.TotalMinutesSlept < kvp.Value.TotalMinutesSlept)
                {
                    SleepsTheMost = kvp.Value;
                }
                if (MostConsistent == null)
                {
                    MostConsistent = kvp.Value;
                }
                else if (kvp.Value.GetMostSleptMinute().Duration > msm.Duration)
                {
                    msm = kvp.Value.GetMostSleptMinute();
                    MostConsistent = kvp.Value;
                }

            }
            KeyValuePair<int, int> MostSleptMinute = new KeyValuePair<int, int>(0, 0);
            foreach (KeyValuePair<int,int> kvp in SleepsTheMost.MinuteData)
            {
                if (kvp.Value > MostSleptMinute.Value)
                {
                    MostSleptMinute = kvp;
                }
            }
            Console.WriteLine(string.Format("Part1:{0}", (MostSleptMinute.Key * SleepsTheMost.ID)));
            Console.WriteLine(string.Format("Part2:{0}", (MostConsistent.ID * msm.Minute)));
        }
    }

    class LogEntry
    {

        public DateTime TimeStamp;
        public string Message;
        private Regex inputparsing = new Regex(@"\[(\d*)-(\d*)-(\d*) (\d*):(\d*)\] (.*)");
        public LogEntry(string entry)
        {
            MatchCollection mc = inputparsing.Matches(entry);
            foreach (Match m in mc)
            {
                GroupCollection g = m.Groups;
                int year = int.Parse(g[1].Value);
                int month = int.Parse(g[2].Value);
                int day = int.Parse(g[3].Value);
                int hour = int.Parse(g[4].Value);
                int minute = int.Parse(g[5].Value);
                Message = g[6].Value;

                TimeStamp = new DateTime(year, month, day, hour, minute, 0);
               
            }
        }
    }
    class Guard
    {
        public int ID;
        public int TotalMinutesSlept;
        public Dictionary<int, int> MinuteData = new Dictionary<int, int>();
        private DateTime fallAsleepTime;
        public Guard(int ID)
        {
            this.ID = ID;
            TotalMinutesSlept = 0;
        }
        public void FallsAsleep(DateTime dt)
        {
            fallAsleepTime = dt;
        }
        public void WakesUp(DateTime dt)
        {
            TotalMinutesSlept += (dt.Minute - fallAsleepTime.Minute);
            for (int i = fallAsleepTime.Minute; i < dt.Minute; i++)
            {
                if (MinuteData.ContainsKey(i))
                {
                    MinuteData[i]++;
                }
                else
                {
                    MinuteData.Add(i, 1);
                }
            }
        }
        public MostSleptMinute GetMostSleptMinute()
        {
            MostSleptMinute retVal = new MostSleptMinute(0, 0);
            foreach (KeyValuePair<int,int> kvp in MinuteData)
            {
                if (kvp.Value > retVal.Duration)
                {
                    retVal.Minute = kvp.Key;
                    retVal.Duration = kvp.Value;
                }

            }
            return retVal;
        }
    }
    class MostSleptMinute
    {
        public int Minute;
        public int Duration;
        public MostSleptMinute(int Minute, int Duration)
        {
            this.Minute = Minute;
            this.Duration = Duration;
        }
    }
    
}
