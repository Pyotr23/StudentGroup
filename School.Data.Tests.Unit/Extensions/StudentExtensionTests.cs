using NUnit.Framework;
using School.Data.Extensions;
using School.Core.Models;
using System.Collections.Generic;
using System.Linq;
using School.Core.Filtration.Parameters;

namespace School.Data.Tests.Unit.Extensions
{
    [TestFixture]
    public class StudentExtensionTests
    {
        [Test]
        public void FilterByGroupName_EmptyGroupName_AreEqualStudentCount()
        {
            var students = new List<Student> 
            {  
                new(),
                new()                
            };

            var filteredStudents = students
                .AsQueryable()
                .FilterByGroupName(string.Empty);

            Assert.AreEqual(students.Count(), filteredStudents.Count());
        }

        [Test]
        public void FilterByGroupName_NoGroupsForFilter_AreEqualStudentCount()
        {
            var groupName = "RightName";
            var students = new List<Student>
            {
                new()
                {
                    Groups = new List<Group>
                    {
                        new()
                        {
                            Name = groupName
                        }
                    }
                },
                new()
                {
                    Groups = new List<Group>
                    {
                        new()
                        {
                            Name = groupName
                        }
                    }
                }
            };

            var filteredStudents = students
                .AsQueryable()
                .FilterByGroupName(groupName);
            
            Assert.AreEqual(students.Count(), filteredStudents.Count());
        }

        [Test]
        public void FilterByGroupName_AllStudentsForFilter_AreEqualZero()
        {
            var groupName = "RightName";
            var anotherGroupName = "AnotherName";
            var students = new List<Student>
            {
                new()
                {
                    Groups = new List<Group>
                    {
                        new()
                        {
                            Name = anotherGroupName
                        }
                    }
                },
                new()
                {
                    Groups = new List<Group>
                    {
                        new()
                        {
                            Name = anotherGroupName
                        }
                    }
                }
            };

            var filteredStudents = students
                .AsQueryable()
                .FilterByGroupName(groupName);

            Assert.AreEqual(0, filteredStudents.Count());
        }

        [Test]
        public void WithPagination_DefaultPagination_ResultAreEqualStudents()
        {
            var students = new List<Student>
            {
                new(),
                new()
            }
                .AsQueryable();

            var filterParameters = new StudentFilterParameters();

            var paginationStudents = students.WithPagination(filterParameters);

            Assert.AreEqual(students, paginationStudents);
        }

        [Test]
        public void WithPagination_SkipOne_ResultCountAreEqualIncrementStudentCount()
        {
            var students = new List<Student>
            {
                new(),
                new()
            };
            var filterParameters = new StudentFilterParameters
            {
                SkipCount = 1
            };

            var paginationStudents = students
                .AsQueryable()
                .WithPagination(filterParameters);

            Assert.AreEqual(students.Count - 1, paginationStudents.Count());            
        }

        [Test]
        public void WithPagination_SkipOne_FirstStudentAreEqualSecondStudent()
        {
            var students = new List<Student>
            {
                new()
                {
                    Id = 1
                },
                new()
                {
                    Id = 2
                }
            };
            var filterParameters = new StudentFilterParameters
            {
                SkipCount = 1
            };

            var paginationStudents = students
                .AsQueryable()
                .WithPagination(filterParameters);

            Assert.AreEqual(students.ElementAt(1), paginationStudents.FirstOrDefault());
        }

        [Test]
        public void WithPagination_TakeOne_CountAreEqualOne()
        {
            var students = new List<Student>
            {
                new()
                {
                    Id = 1
                },
                new()
                {
                    Id = 2
                }
            };
            var filterParameters = new StudentFilterParameters
            {
                PageSize = 1
            };

            var paginationStudents = students
                .AsQueryable()
                .WithPagination(filterParameters);

            Assert.AreEqual(1, paginationStudents.Count());
        }
    }
}
