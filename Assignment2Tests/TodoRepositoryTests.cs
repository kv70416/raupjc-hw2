using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment2;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using enumgenlist;
using @interface;

namespace Assignment2.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        [TestMethod()]
        public void TodoRepositoryTest()
        {
            TodoRepository t1 = new TodoRepository();

            IGenericList<TodoItem> l = new GenericList<TodoItem>();
            TodoRepository t2 = new TodoRepository(l);
        }

        [TestMethod()]
        public void AddTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");

            IGenericList<TodoItem> l = new GenericList<TodoItem>();
            l.Add(a);
            l.Add(b);
        }

        [TestMethod()]
        public void GetTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");
            TodoItem c = new TodoItem("task c");

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);
            t.Add(c);

            TodoItem i = t.Get(b.Id);

            Assert.AreEqual(b, i);
        }

        [TestMethod()]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void GetTest2()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");
            TodoItem c = new TodoItem("task c");

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);
            t.Add(c);
            t.Add(b);
        }


        [TestMethod()]
        public void RemoveTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");
            TodoItem c = new TodoItem("task c");
            TodoItem d = new TodoItem("task d");

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);
            t.Add(c);

            Assert.IsTrue(t.Remove(c.Id));
            Assert.IsFalse(t.Remove(d.Id));
       }

        [TestMethod()]
        public void UpdateTest()
        {
            TodoItem a = new TodoItem("task a");

            TodoRepository t = new TodoRepository();
            t.Add(a);

            TodoItem b = new TodoItem("task b");
            TodoItem c = new TodoItem("task c");
            b.Id = a.Id;

            t.Update(b);
            t.Update(c);

            Assert.AreEqual(t.Get(a.Id).Text, "task b");
            Assert.AreEqual(t.Get(c.Id).Text, "task c");
        }

        [TestMethod()]
        public void MarkAsCompletedTest()
        {
            TodoItem a = new TodoItem("task a");

            TodoRepository t = new TodoRepository();
            t.Add(a);

            t.MarkAsCompleted(a.Id);

            Assert.IsTrue(a.IsCompleted);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");
            b.DateCreated = a.DateCreated.Add(TimeSpan.FromMinutes(1));

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);

            List<TodoItem> l = t.GetAll();

            Assert.IsTrue(l.Count == 2);
            Assert.IsTrue(l.ElementAt(0).Text == "task b");
        }

        [TestMethod()]
        public void GetActiveTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");
            a.MarkAsCompleted();

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);

            List<TodoItem> l = t.GetActive();

            Assert.IsTrue(l.Count == 1);
            Assert.IsTrue(l.ElementAt(0).Text == "task b");
        }

        [TestMethod()]
        public void GetCompletedTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");
            b.MarkAsCompleted();

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);

            List<TodoItem> l = t.GetCompleted();

            Assert.IsTrue(l.Count == 1);
            Assert.IsTrue(l.ElementAt(0).Text == "task b");
        }

        [TestMethod()]
        public void GetFilteredTest()
        {
            TodoItem a = new TodoItem("task a");
            TodoItem b = new TodoItem("task b");

            TodoRepository t = new TodoRepository();
            t.Add(a);
            t.Add(b);

            List<TodoItem> l = t.GetFiltered(i => i.Text == "task b");

            Assert.IsTrue(l.Count == 1);
            Assert.IsTrue(l.ElementAt(0).Text == "task b");
        }
    }
}