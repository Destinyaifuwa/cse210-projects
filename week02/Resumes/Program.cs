using System;

class Program
{
    static void Main(string[] args)
    {
        // Creat a first job
        Job job1 = new Job();
        job1._jobTitle = "Sofeware Engineer";
        job1._company = "Micorsoft";
        job1._startYear = 2019;
        job1._endYear = 2022;

        // Creat a second Job
        Job job2 = new Job();
        job2._jobTitle = "Manager";
        job2._company = "Appel";
        job2._startYear = 2022;
        job2._endYear = 2023;

        // Create Resume and Add Jobs
        Resume myResume = new Resume();
        myResume._name = "Destiny Aifuwa";
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        // How to display the Full Resume
        myResume.Display();

    }
}