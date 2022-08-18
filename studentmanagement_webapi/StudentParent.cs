using System;
namespace studentmanagement_webapi
{
    public class StudentParent
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public int StudentId { get; set; }
        public Parent Student { get; set; }
    }
}

