using Exercises.Models.Data;
using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercises.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Majors()
        {
            var model = MajorRepository.GetAll();
            return View(model.ToList());
        }

        [HttpGet]
        public ActionResult AddMajor()
        {
            return View(new Major());
        }

        [HttpPost]
        public ActionResult AddMajor(Major major)
        {
            if (string.IsNullOrWhiteSpace(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "please enter the name of the major");
                return View("AddMajor", major);
            }
            MajorRepository.Add(major.MajorName);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult EditMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult EditMajor(Major major)
        {
            if (string.IsNullOrWhiteSpace(major.MajorName))
            {
                ModelState.AddModelError("MajorName", "please enter the name of the major");
                return View("EditMajor", major);
            }
            MajorRepository.Edit(major);
            return RedirectToAction("Majors");
        }

        [HttpGet]
        public ActionResult DeleteMajor(int id)
        {
            var major = MajorRepository.Get(id);
            return View(major);
        }

        [HttpPost]
        public ActionResult DeleteMajor(Major major)
        {
            MajorRepository.Delete(major.MajorId);
            return RedirectToAction("Majors");
        }
        [HttpGet]
        public ActionResult States()
        {
            var model = StateRepository.GetAll();
            return View(model.ToList());
        }
        [HttpGet]
        public ActionResult AddState()
        {
            return View(new State());
        }
        [HttpPost]
        public ActionResult AddState(State state)
        {
            if (string.IsNullOrWhiteSpace(state.StateName))
            {
                ModelState.AddModelError("StateName", "please enter the name of the state");
            }
            if (string.IsNullOrWhiteSpace(state.StateAbbreviation) || state.StateAbbreviation.Length != 2)
            {
                ModelState.AddModelError("StateAbbreviation", "Please enter a valid, two character abbreviation");
            }
            if (ModelState.IsValid)
            {
                StateRepository.Add(state);
                return RedirectToAction("States");
            }
            else
            {
                return View("AddState", state);
            }
        }
        [HttpGet]
        public ActionResult EditState(string id)
        {
            
            State model = StateRepository.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditState(State state)
        {
            if (string.IsNullOrWhiteSpace(state.StateName))
            {
                ModelState.AddModelError("StateName", "please enter the name of the state");
                return View("EditState", state);
            }
            else
            {
                StateRepository.Edit(state);
                return RedirectToAction("States");
            }
        }


        [HttpGet]
        public ActionResult DeleteState(string id)
        {
            State model = StateRepository.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteState(State state)
        {
            StateRepository.Delete(state.StateAbbreviation);
            return RedirectToAction("States");
        }
        [HttpGet]
        public ActionResult Courses()
        {
            List<Course> model = CourseRepository.GetAll().ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddCourse()
        {
            return View(new Course());
        }
        [HttpPost]
        public ActionResult AddCourse(string coursename)
        {
            if (string.IsNullOrWhiteSpace(coursename))
            {
                ModelState.AddModelError("CourseName", "Please enter the name of the course");
                return View("AddCourse");
            }

            CourseRepository.Add(coursename);
            return RedirectToAction("Courses");
        }


        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            Course model = CourseRepository.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseName))
            {
                ModelState.AddModelError("CourseName", "Please enter the name of the course");
                return View("AddCourse");
            }
            CourseRepository.Edit(course);
            return RedirectToAction("Courses");
        }

        [HttpGet]
        public ActionResult DeleteCourse(int id)
        {
            Course model = CourseRepository.Get(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteCourse(Course course)
        {
            CourseRepository.Delete(course.CourseId);
            return RedirectToAction("Courses");
        }
    }
}