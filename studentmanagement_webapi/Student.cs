using System;
namespace studentmanagement_webapi
{
    public class Student
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public int grade { get; set; }
        public string Gender { get; set; }
        public int age { get; set; }
        public int LessonId { get; set; }

    }
}

