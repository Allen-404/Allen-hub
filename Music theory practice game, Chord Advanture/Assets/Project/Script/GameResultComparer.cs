using UnityEngine;
using System.Collections.Generic;

public class GameResultComparer : MonoBehaviour
{
    public static GameResultComparer instance;

    private List<Note> _result = new List<Note>();

    private void Awake()
    {
        instance = this;
    }

    public Note[] GetCurrentResult()
    {
        return _result.ToArray();
    }

    public void Clear()
    {
        _result = new List<Note>();
    }

    public void Add(Note n)
    {
        _result.Add(n);
    }

    public void Remove()
    {
        if (_result.Count < 1)
            return;

        _result.RemoveAt(_result.Count - 1);
    }

    public bool Compare(Note a, Note b)
    {
        return a == b;
    }

    public bool Compare(List<Note> a, List<Note> b)
    {
        if (a.Count != b.Count)
            return false;

        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] != b[i])
                return false;
        }

        return true;
    }
}
