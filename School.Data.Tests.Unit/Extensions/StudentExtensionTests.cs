using NUnit.Framework;
using School.Data.Extensions;
using School.Core.Models;
using System.Collections.Generic;
using System.Linq;

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
    }
}
