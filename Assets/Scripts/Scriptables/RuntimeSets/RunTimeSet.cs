using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{

    public abstract class RunTimeSet<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();

        public void Add(T t)
        {
            if (!Items.Contains(t))
                Items.Add(t);
        }

        public void Remove(T t)
        {
            if (Items.Contains(t))
                Items.Remove(t);
        }

    }


    public abstract class RunTimeSet<T1, T2> : ScriptableObject
    {
        public List<Pair<T1, T2>> Items = new List<Pair<T1, T2>>();

        public void Add(Pair<T1, T2> t)
        {
            if (!Items.Contains(t))
                Items.Add(t);
        }

        public void Remove(Pair<T1, T2> t)
        {
            if (Items.Contains(t))
                Items.Remove(t);
        }

    }

    [System.Serializable]
    public struct Pair<T1, T2>
    {
        public T1 ItemOne;
        public T2 ItemTwo;

    }
}
