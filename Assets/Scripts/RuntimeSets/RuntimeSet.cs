using System;
using System.Collections;
using System.Collections.Generic;
using Codetox;
using UnityEngine;

namespace RuntimeSets
{
    public abstract class RuntimeSet<T> : CustomScriptableObject, IEnumerable<T>
    {
        [SerializeField] private List<T> items;

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event Action<T> OnItemAdded;
        public event Action<T> OnItemRemoved;

        public void Add(T item)
        {
            if (items.Contains(item)) return;
            items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        public void Remove(T item)
        {
            if (items.Remove(item)) OnItemRemoved?.Invoke(item);
        }

        public void Clear()
        {
            items.Clear();
        }
    }
}