namespace Albayan_Task
{
    public class Utiliz
    {
        static int hoursShift = 0; 
        public static DateTime CurrentDate { get { return DateTime.Now.AddHours(hoursShift); } }
    }
}
