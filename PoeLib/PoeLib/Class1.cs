using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace PoeLib 
{ 
public class Module
{
    public string? moduleCode;
    public string? moduleName;
    public int moduleCredit, semesterWeeks, moduleHours, moduleSelfStudy;
    public DateTime SemesterStartDate;
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


public class Hash
{
    public static string Hash_SHA1(string input)
    {
        //Adapted from Youtube
        //Author:SkillCafe
        //Author profile:https://www.youtube.com/channel/UCiB8kObX-grPRaavKUCOY6Q
        //Date:28 January 2021
        //Link:https://www.youtube.com/watch?v=tRaFiPRlphs&t=152s

        using (SHA1Managed sha1 = new SHA1Managed())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}



public class Student : Module
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? HashPassword { get; set; }
    public List<Module> modules = new List<Module>();

    public static string HashPasword(string Password)
    {
        var sha = SHA256.Create();
        var asByteArray = Encoding.Default.GetBytes(Password);
        var hashedPassword = sha.ComputeHash(asByteArray);

        return Convert.ToBase64String(hashedPassword);
    }

    //}
    //public void Update()
    //{
    //    // Songs st = new Songs(artist, songName, year, genre);
    //    using (SqlConnection sqlcon = new SqlConnection(conn))
    //    {
    //        sqlcon.Open();
    //        string update = "Update Student set Artist='{0}' where 'SONGID=1'";
    //        sqlcon.Close();
    //    }
    //}


}
    

}
