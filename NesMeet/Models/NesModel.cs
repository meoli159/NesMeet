using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NesMeet.Models
{
        public enum PreferredCampus { PPT, NX}
        public enum Gender { Male, Female, Other}
        public enum Semester { Spring, Summer, Fall, Winter}
        public enum TrainerType { Internal, External}
        public enum Role { Admin, Staff, Trainer, Trainee}
        
        public class CUser : IdentityUser
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime? Birthday { get; set; }

            [Required]
            public Gender Gender { get; set; }
            public string Address { get; set; }

            [Required]
            public int DepartmentId { get; set; }
            public virtual Department Department { get;set; }

            public virtual ICollection<TraineeClassroom> TraineeClassrooms { get; set; }
            public virtual ICollection<Topic> Topics { get; set; }
        }

        [Table("Trainee")]
        public class Trainee: CUser
        {
            public int? TOEIC { get; set; }
            
        }

        [Table("Trainer")]
        public class Trainer : CUser
        {
            public TrainerType? Type { get; set; }
            //..
        }

        //Staff

        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public virtual ICollection<Course> Courses { get; set; }
        }

        public class Course
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Description { get; set; }
            public string Name_EN { get; set; }
            public string Name_VN { get; set; }
            public int? Credit { get; set; }
            public int? Hour { get; set; }
            //..

            [Required]
            public int CategoryId { get; set; }
            public virtual Category Category { get; set; }

            public virtual ICollection<Classroom> Classrooms { get; set; }
        }

        public class Department
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public virtual ICollection<CUser> Users { get; set; }
        }

        public class ClassProfile
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public int? EnrolmentYear { get; set; }
            public PreferredCampus? PreferredCampus { get; set; }
            //..

            public virtual ICollection<Classroom> Classrooms { get; set; }
        }

        public class Classroom
        {
            public int Id { get; set; }
            public string Code { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:dd//MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime StartDate { get; set; }

            [Required]
            [DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{0:dd//MM/yyyy}", ApplyFormatInEditMode = true)]
            public DateTime EndDate { get; set; }

            [Required]
            public int Year { get; set; }

            [Required]
            public Semester Semester{get; set;}

            [Required]
            public int Part { get; set; }

            public int? ClassProfileId { get; set; }
            public virtual ClassProfile ClassProfile { get; set; }

            [Required]
            public int CourseId { get; set; }
            public virtual Course Course { get; set; }

            public virtual ICollection<TraineeClassroom> TraineeClassrooms { get; set; }
            public virtual ICollection<Topic> Topics { get; set; }
        }

        public class TraineeClassroom
        {
            public int Id { get; set; }

            [Required]
            public int ClassroomId { get; set; }
            public virtual Classroom Classroom { get; set; }

            [Required]
            public string TraineeId { get; set; }
            public virtual CUser Trainee { get; set; }
        }

        public class Topic
        {
            public int Id { get; set; }
            public string Name { get; set; }

            [Required]
            public int ClassroomId { get; set; }
            public virtual Classroom Classroom { get; set; }

            [Required]
            public string TrainerId { get; set; }
            public virtual CUser Trainer { get; set; }
        }


    
}
