using UnityEngine;
using System.Collections.Generic;

namespace Hitcode_RoomEscape.UI
{
 
    public class SimpleList<T>
    {
   
        public T[] data;

        public int Count = 0;

    
        public T this[int i]
        {
            get { return data[i]; }
            set { data[i] = value; }
        }

     
        private void ResizeArray()
        {
            T[] newData;

            if (data != null)
                newData = new T[Mathf.Max(data.Length << 1, 64)];
            else
                newData = new T[64];

            if (data != null && Count > 0)
                data.CopyTo(newData, 0);

            data = newData;
        }

      
        public void Clear()
        {
            Count = 0;
        }

      
        public T First()
        {
            if (data == null || Count == 0) return default(T);
            return data[0];
        }

        public T Last()
        {
            if (data == null || Count == 0) return default(T);
            return data[Count - 1];
        }

   
        public void Add(T item)
        {
            if (data == null || Count == data.Length)
                ResizeArray();

            data[Count] = item;
            Count++;
        }

        public void AddStart(T item)
        {
            Insert(item, 0);
        }

        public void Insert(T item, int index)
        {
            if (data == null || Count == data.Length)
                ResizeArray();

            for (var i = Count; i > index; i--)
            {
                data[i] = data[i - 1];
            }

            data[index] = item;
            Count++;
        }

        public T RemoveStart()
        {
            return RemoveAt(0);
        }

      
        public T RemoveAt(int index)
        {
            if (data != null && Count != 0)
            {
                T val = data[index];

                for (var i = index; i < Count - 1; i++)
                {
                    data[i] = data[i + 1];
                }

                Count--;
                data[Count] = default(T);
                return val;
            }
            else
            {
                return default(T);
            }
        }

    
        public T Remove(T item)
        {
            if (data != null && Count != 0)
            {
                for (var i = 0; i < Count; i++)
                {
                    if (data[i].Equals(item))
                    {
                        return RemoveAt(i);
                    }
                }
            }

            return default(T);
        }

    
        public T RemoveEnd()
        {
            if (data != null && Count != 0)
            {
                Count--;
                T val = data[Count];
                data[Count] = default(T);

                return val;
            }
            else
            {
                return default(T);
            }
        }

        public bool Contains(T item)
        {
            if (data == null)
                return false;

            for (var i = 0; i < Count; i++)
            {
                if (data[i].Equals(item))
                    return true;
            }

            return false;
        }
    }
}