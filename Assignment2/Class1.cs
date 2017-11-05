using enumgenlist;
using @interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Assignment2
{
    public interface ITodoRepository
    {
        TodoItem Get(Guid todoId);

        TodoItem Add(TodoItem todoItem);

        bool Remove(Guid todoId);

        TodoItem Update(TodoItem todoItem);

        bool MarkAsCompleted(Guid todoId);

        List<TodoItem> GetAll () ;

        List<TodoItem> GetActive();

        List<TodoItem> GetCompleted () ;

        List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction);
    }

    public class TodoRepository : ITodoRepository
    {
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;

        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.FirstOrDefault(i => i.Id == todoId);
        }

        public TodoItem Add(TodoItem todoItem)
        {
            IEnumerable<TodoItem> temp = _inMemoryTodoDatabase.Where(i => i.Equals(todoItem));
            if (temp.Any())
            {
                throw new DuplicateTodoItemException("duplicate id: " + todoItem.Id);
            }

            _inMemoryTodoDatabase.Add(todoItem);
            return todoItem;
        }

        public bool Remove(Guid todoId)
        {
            IEnumerable<TodoItem> temp = _inMemoryTodoDatabase.Where(i => i.Id == todoId);

            if (temp.Any())
            {
                return _inMemoryTodoDatabase.Remove(temp.FirstOrDefault());
            }

            return false;
        }

        public TodoItem Update(TodoItem todoItem)
        {
            Remove(todoItem.Id);
            Add(todoItem);
            return todoItem;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            IEnumerable<TodoItem> temp = _inMemoryTodoDatabase.Where(i => i.Id == todoId);

            if (temp.Any())
            {
                TodoItem t = temp.FirstOrDefault();
                t.MarkAsCompleted();
                Update(t);

                return true;
            }

            return false;
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(i => i.DateCreated).ToList();
        }

        public List<TodoItem> GetActive()
        {
            return GetAll().Where(i => !i.IsCompleted).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return GetAll().Where(i => i.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return GetAll().Where(filterFunction).ToList();
        }
    }

    [Serializable]
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException()
        {
        }

        public DuplicateTodoItemException(string message) : base(message)
        {
        }

        public DuplicateTodoItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateTodoItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }

        public bool IsCompleted => DateCompleted.HasValue;

        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }

        public TodoItem(string text)
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.UtcNow;
            Text = text;
        }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted)
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TodoItem))
                return false;

            return this.Id.Equals(((TodoItem) obj).Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class Class1
    {
    }
}
