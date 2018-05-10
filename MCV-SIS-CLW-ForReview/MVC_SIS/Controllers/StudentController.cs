using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exercises.Models.Data;
using Exercises.Models.ViewModels;

namespace Exercises.Controllers
{
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var model = StudentRepository.GetAll();

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new StudentVM();
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(StudentVM studentVM)
        {
            
                studentVM.Student.Courses = new List<Course>();

                foreach (var id in studentVM.SelectedCourseIds)
                    studentVM.Student.Courses.Add(CourseRepository.Get(id));

                studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            if (ModelState.IsValid)
            {
                StudentRepository.Add(studentVM.Student);

                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View("Add", studentVM);
            }
        }
        [HttpGet]
        public ActionResult EditStudent(int id)
        {
            Student model = StudentRepository.Get(id);//need to return strudent vm after this step to allow course selection
            var viewModel = new StudentVM();
            viewModel.Student = model;
            viewModel.SetCourseItems(CourseRepository.GetAll());
            viewModel.SetMajorItems(MajorRepository.GetAll());
            return View(viewModel);
           
        }
        [HttpPost]
        public ActionResult EditStudent(StudentVM student)//student not returning WHY
        {
            
                student.Student.Major = MajorRepository.Get(student.Student.Major.MajorId);//why does't this work???
                student.Student.Courses = new List<Course>();

                foreach (var id in student.SelectedCourseIds)
                {
                    student.Student.Courses.Add(CourseRepository.Get(id));
                }
            if (ModelState.IsValid)
            {
                StudentRepository.Edit(student.Student);
                return RedirectToAction("List");
            }
            else
            {
                student.SetCourseItems(CourseRepository.GetAll());
                student.SetMajorItems(MajorRepository.GetAll());
                return View("Add", student);
            }
        }
        [HttpPost]
        public ActionResult EditStudentInfo(StudentVM studentVM)
        {
            studentVM.Student.Courses = new List<Course>();

            foreach (var id in studentVM.SelectedCourseIds)
                studentVM.Student.Courses.Add(CourseRepository.Get(id));

            studentVM.Student.Major = MajorRepository.Get(studentVM.Student.Major.MajorId);
            if (ModelState.IsValid)
            {
                StudentRepository.Edit(studentVM.Student);

                return RedirectToAction("List");
            }
            else
            {
                studentVM.SetCourseItems(CourseRepository.GetAll());
                studentVM.SetMajorItems(MajorRepository.GetAll());
                return View("EditStudent", studentVM);
            }

        }
        [HttpPost]
        public ActionResult EditStudentAddress(Student student)
        {
            StudentRepository.SaveAddress(student.StudentId, student.Address);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult DeleteStudent(int ID)
        {
            Student model = StudentRepository.Get(ID);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteStudentEx(int StudentID)
        {
            StudentRepository.Delete(StudentID);
            return RedirectToAction("List");
        }
    }
}