using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Exercises.Models.Data;
using Exercises.Models.Repositories;

namespace SIS_Test
{
    [TestFixture]
    public class Test
    {
      

        [Test]
        public void CanLoadStates()
        {
            List<State> States = StateRepository.GetAll().ToList();
            Assert.AreEqual(States.Count, 3);
        }
        [Test]
        public void CanDeleteStudent()
        {

            StudentRepository.Delete(3);
            List<Student> Students = StudentRepository.GetAll().ToList();
            Assert.AreEqual(2, Students.Count);
            Assert.AreEqual(Students.Max(s => s.StudentId), 2);
        }
        [Test]
        public void CanLoadStudents()
        {
            List<Student> Students = StudentRepository.GetAll().ToList();
            Assert.AreEqual(Students[1].StudentId, 2);
        }
        

    }
}
