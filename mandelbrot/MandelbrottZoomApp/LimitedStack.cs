using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrottZoomApp
{
    class LimitedStack<T>
    {
        private LinkedList<T> list;
        int cap;
        public LimitedStack(int capacity)
        {
            cap = capacity;
            list = new LinkedList<T>();
        }

        public void Push(T value)
        {
            if (list.Count == cap)
            {
                list.RemoveLast();
            }
            list.AddFirst(value);
        }

        public T Pop()
        {
            if (list.Count > 0)
            {
                T value = list.First.Value;
                list.RemoveFirst();
                return value;
            }
            else
            {
                throw new InvalidOperationException("The Stack is empty");
            }
        }
        public T Peek()
        {
            if (list.Count > 0)
            {
                T value = list.First.Value;
                return value;
            }
            else
            {
                throw new InvalidOperationException("The Stack is empty");
            }

        }
        public int Count
        {
            get { return list.Count; }
        }
        public void Clear()
        {
            list.Clear();

        }
    }

}
