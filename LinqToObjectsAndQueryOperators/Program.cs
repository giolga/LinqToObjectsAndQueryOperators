using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToObjectsAndQueryOperators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UniversityManager manager = new UniversityManager();
            manager.Male();
            Console.WriteLine("------------------------------------------------------------");
            manager.Female();

            Console.WriteLine("------------------------------------------------------------");
            UniversityManager natia = new UniversityManager();
            natia.CU();

            Console.WriteLine("------------------------------------------------------------");
            manager.SortStudentsByAge();

            Console.WriteLine("------------------------------------------------------------");
            manager.AllStudentsFromUni(1);

            Console.WriteLine("------------------------------------------------------------");
            manager.AllStudentsFromUni(2);

            Console.WriteLine("------------------------------------------------------------");
            manager.StudentAndUniversityNameCollection();

            Console.ReadKey();
        }
    }

    internal class UniversityManager
    {
        public List<University> universities;

        public List<Student> students;

        public UniversityManager()
        {
            universities = new List<University>();
            students = new List<Student>();

            universities.Add(new University() { Id = 1, Name = "CU" });
            universities.Add(new University() { Id = 2, Name = "BTU" });

            students.Add(new Student() { Id = 1, Name = "Juan", Gender = "Male", Age = 18, UniversityId = 1 });
            students.Add(new Student() { Id = 2, Name = "Kumi", Gender = "Male", Age = 21, UniversityId = 1 });
            students.Add(new Student() { Id = 3, Name = "Mari", Gender = "Female", Age = 21, UniversityId = 1 });
            students.Add(new Student() { Id = 4, Name = "Gvantsa", Gender = "Female", Age = 21, UniversityId = 1 });
            students.Add(new Student() { Id = 5, Name = "Sandro", Gender = "Male", Age = 21, UniversityId = 1 });
            students.Add(new Student() { Id = 6, Name = "Nika", Gender = "Male", Age = 24, UniversityId = 2 });
            students.Add(new Student() { Id = 7, Name = "Luka", Gender = "Male", Age = 18, UniversityId = 2 });
        }

        public void Male()
        {
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "Male" select student;

            Console.WriteLine("Male students: ");

            foreach (Student student in maleStudents)
            {
                student.Print();
            }
        }

        public void Female()
        {
            IEnumerable<Student> maleStudents = from student in students where student.Gender == "Female" select student;

            Console.WriteLine("Female students: ");

            foreach (Student student in maleStudents)
            {
                student.Print();
            }
        }

        public void CU()
        {
            //IEnumerable<Student> cuStudents = from student in students where student.UniversityId == 1 select student;
            //Console.WriteLine("CU Students: ");

            //foreach (Student student in cuStudents)
            //{
            //    student.Print();
            //}

            IEnumerable<Student> cuStudents = from student in students
                                              join university in universities on student.UniversityId equals university.Id
                                              where university.Name == "CU"
                                              select student;

            Console.WriteLine("CU Students: ");

            foreach (Student student in cuStudents)
            {
                student.Print();
            }
        }

        public void SortStudentsByAge()
        {
            var sortedStudents = from student in students orderby student.Age ascending select student;
            Console.WriteLine("Sorted students: ");

            foreach (var student in sortedStudents)
            {
                student.Print();
            }
        }

        public void AllStudentsFromUni(int id)
        {
            IEnumerable<Student> allStudents = from student in students
                                               join university in universities on student.UniversityId equals university.Id
                                               where university.Id == id
                                               select student;

            Console.WriteLine("Students from university with the id {0}", id);
            foreach (var student in allStudents)
            {
                student.Print();
            }
        }

        public void StudentAndUniversityNameCollection()
        {
            var newCollection = from student in students
                                join university in universities on student.UniversityId equals university.Id
                                orderby student.Name
                                select new { StudentName = student.Name, UniversityName = university.Name };

            Console.WriteLine("New collection: ");
            foreach (var col in newCollection)
            {
                Console.WriteLine("Student {0} from university {1}", col.StudentName, col.UniversityName);
            }
        }

    }

    internal class University
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Print()
        {
            Console.WriteLine("University {0} with Id {1}", Name, Id);
        }
    }

    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int UniversityId { get; set; }

        public void Print()
        {
            Console.WriteLine("Student {0} with Id {1} Gender {2} Age {3} from University with the Id {4}", Name, Id, Gender, Age, UniversityId);
        }
    }
}
