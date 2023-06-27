using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{

    public abstract class RunTimeSet<T> : ScriptableObject
    {
        public HashSet<T> Items = new HashSet<T>();

        public void OnEnable()
        {
            Items = new HashSet<T>();
        }

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

    public abstract class RunTimeSetList<T> : ScriptableObject
    {
        public List<T> Items = new List<T>();

        public void OnEnable()
        {
            Items = new List<T>();
        }

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

        public int Size()
        {
            return Items.Count;
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

        public int Size()
        {
            return Items.Count;
        }

    }

    public abstract class RunTimeSet<T1, T2, T3> : ScriptableObject
    {
        public List<Tuple<T1, T2, T3>> Items = new List<Tuple<T1, T2, T3>>();

        public void Add(Tuple<T1, T2, T3> t)
        {
            if (!Items.Contains(t))
                Items.Add(t);
        }

        public void Remove(Tuple<T1, T2, T3> t)
        {
            if (Items.Contains(t))
                Items.Remove(t);
        }

        public int Size()
        {
            return Items.Count;
        }

    }

    [System.Serializable]
    public struct Pair<T1, T2>
    {
        public T1 ItemOne;
        public T2 ItemTwo;

    }

    [System.Serializable]
    public struct Tuple<T1, T2, T3>
    {
        public T1 ItemOne;
        public T2 ItemTwo;
        public T3 ItemThree;

    }
}
