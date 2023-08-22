﻿using ItemsManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemsManagement.Controllers
{
    public class StudentController : Controller
    {

        public static List<Student> students = new List<Student>()
        {
            new Student{Id = 1, FirstName = "Harry",LastName = "Potter", Email = "potter@gmail.com1", Age = 17 },
            new Student{Id = 2, FirstName = "Hermonie" , LastName = "Granger", Email = "granger@gmail.com", Age = 16},
            new Student{Id = 3, FirstName = "Ron" , LastName = "Weasley", Email = "weasley@gmail.com", Age = 18},
            new Student{Id = 4, FirstName = "Draco", LastName = "Malfoy" , Email = "malfoy@gmail.com", Age = 19}
        };

        private int studentNo = 3;
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Header = "Training Institution";
            ViewData[""] = "A Training";
            return View();
        }

        public IActionResult About()
        {
            return View("Student/About.cshtml");
        }
        [HttpGet]
        [ActionName("Display")]
        public IActionResult List()
        {
            return View(students);
        }
        public IActionResult Detail(int id) {

            var stu = from student in students
                      where student.Id == id
                      select student;
            Student st = stu.ToList()[0];
            return View(st);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string firstName, string lastName, string email, int age ) { 
            Student nStudent = new Student() { Id = ++studentNo, 
                FirstName = firstName, LastName = lastName, Email = email, Age = age};
            students.Add(nStudent);
            return View("Display" , students);
        }

        [NonAction]
        public String GetFirstName()
        {
            return students[0].FirstName;
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            var student = from st in students
                          where st.Id == id
                          select st;

            Student stu = student.ToList()[0];
            return View(stu);
        }

        [HttpPost]
        public IActionResult Edit(int id, string firstName, string lastName, string email, int age)
        {
            var student = from stu in students
                          where stu.Id == id
                          select stu;

            Student nStudent = student.ToList()[0];

            nStudent.FirstName = firstName;
            nStudent.LastName = lastName;
            nStudent.Email = email;
            nStudent.Age = age;

            return View("Detail" , nStudent);
        }

        public IActionResult Delete(int id)
        {
            Student student = students.Find(student =>student.Id == id);
            students.Remove(student);
            return RedirectToAction("List", students);
        }
    }
}
