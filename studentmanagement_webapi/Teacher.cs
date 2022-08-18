using System;
namespace studentmanagement_webapi
{
    public class Teacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int LessonId { get; set; }
    }
}

