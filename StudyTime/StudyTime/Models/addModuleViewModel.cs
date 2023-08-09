
using System.Globalization;

namespace StudyTime.Models
{
    public class addModuleViewModel
    {
        

        public int ID { get; set; }

        public string Email { get; set; }

        public string moduleCode { get; set; }
        public string moduleName { get; set; }
        public int moduleCredit { get; set; }
        public int semesterWeeks { get; set; }
        public int moduleHours { get; set; }
        public int moduleSelfStudy { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public List<int> weeklyHours = new List<int>();

        public int GetWeek(DateTime day)
        {
            //method selects the week number/index that the user enters from the callender
            // DateTime endDate;


            //Adapted from c-sharp corner
            //Author:Chindirala sampath kumar
            //Author Link:https://www.c-sharpcorner.com/members/chindirala-sampath-kumar
            //Date:19 June 2015
            //Link:https://stackoverflow.com/questions/7341351/get-the-current-index-of-a-combobox

            //advanced coding concept

            CultureInfo cul = CultureInfo.CurrentCulture;

            int firstDayWeek = cul.Calendar.GetWeekOfYear(
                this.SemesterStartDate,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            int selectedDay = cul.Calendar.GetWeekOfYear(
               day,
               CalendarWeekRule.FirstDay,
               DayOfWeek.Monday);


            int weeks = selectedDay - firstDayWeek;

            return weeks;

        }
        //method calculates the study ors for each module
        public void CalcTime()
        {

            this.moduleSelfStudy = ((this.moduleCredit * 10) / this.semesterWeeks) - this.moduleHours;

            //Adapted fromStackOverflow
            //Author:Termininja
            //Date:23 Decemnber 2015
            //Link:https://stackoverflow.com/questions/552766/for-and-while-loop-in-c-sharp

            for (int x = 0; x <= this.semesterWeeks - 1; x++)
            {
                weeklyHours.Add(this.moduleSelfStudy);


            }
        }
    }

    
}
