using UnityEngine;
using System;
using UnityEngine.UI;

public class KeyboardBehaviour : MonoBehaviour
{
    public static KeyboardBehaviour instance;

    public Image key_C;
    public Image key_D;
    public Image key_E;
    public Image key_F;
    public Image key_G;
    public Image key_A;
    public Image key_B;
    public Image key_sharp;
    public Image key_b;
    public TMPro.TextMeshProUGUI prefix;

    private bool _isSharpPressed;
    private bool _isBPressed;

    public TMPro.TextMeshProUGUI displayNotes;

    private void Awake()
    {
        instance = this;
    }

    public void OnPress(int key)
    {
        Debug.Log("OnPress " + key);
        var defNames = Enum.GetNames(typeof(NoteKey));
        foreach (var defName in defNames)
        {
            NoteKey defKey = (NoteKey)Enum.Parse(typeof(NoteKey), defName);
            if ((int)defKey == key)
            {
                OnPressNoteKey(defKey);
                break;
            }
        }
    }

    public void OnPressNoteKey(NoteKey key)
    {
        Debug.Log("OnPressNoteKey " + key);
        switch (key)
        {
            case NoteKey.None:
                break;
            case NoteKey.C:
                if (_isSharpPressed) SubmitNote(Note.C1); else if (_isBPressed) SubmitNote(Note.C2); else SubmitNote(Note.C);
                break;
            case NoteKey.D:
                if (_isSharpPressed) SubmitNote(Note.D1); else if (_isBPressed) SubmitNote(Note.D2); else SubmitNote(Note.D);
                break;
            case NoteKey.E:
                if (_isSharpPressed) SubmitNote(Note.E1); else if (_isBPressed) SubmitNote(Note.E2); else SubmitNote(Note.E);
                break;
            case NoteKey.F:
                if (_isSharpPressed) SubmitNote(Note.F1); else if (_isBPressed) SubmitNote(Note.F2); else SubmitNote(Note.F);
                break;
            case NoteKey.G:
                if (_isSharpPressed) SubmitNote(Note.G1); else if (_isBPressed) SubmitNote(Note.G2); else SubmitNote(Note.G);
                break;
            case NoteKey.A:
                if (_isSharpPressed) SubmitNote(Note.A1); else if (_isBPressed) SubmitNote(Note.A2); else SubmitNote(Note.A);
                break;
            case NoteKey.B:
                if (_isSharpPressed) SubmitNote(Note.B1); else if (_isBPressed) SubmitNote(Note.B2); else SubmitNote(Note.B);
                break;
            case NoteKey.sharp:
                _isSharpPressed = !_isSharpPressed;
                _isBPressed = false;
                RefreshPrefix();
                break;
            case NoteKey.b:
                _isBPressed = !_isBPressed;
                _isSharpPressed = false;
                RefreshPrefix();
                break;
        }
    }

    void RefreshPrefix()
    {
        prefix.text = "";
        if (_isSharpPressed)
        {
            prefix.text = "#";
            return;
        }

        if (_isBPressed)
            prefix.text = "b";
    }

    public void SubmitNote(Note note)
    {
        Debug.Log("SubmitNote " + note);
        _isSharpPressed = false;
        _isBPressed = false;
        prefix.text = "";
        PlayNoteSound(note);
        GameResultComparer.instance.Add(note);
        SyncNoteDisplayer();
    }

    void SyncNoteDisplayer()
    {
        var notes = GameResultComparer.instance.GetCurrentResult();
        var res = "";
        foreach (var n in notes)
            res += GetNoteString(n);

        displayNotes.text = res;
    }

    string GetNoteString(Note note)
    {
        var res = "";
        switch (note)
        {
            case Note.None:
                break;
            case Note.C:
                res = "<sprite index=2> ";
                break;
            case Note.D:
                res = "<sprite index=3> ";
                break;
            case Note.E:
                res = "<sprite index=4> ";
                break;
            case Note.F:
                res = "<sprite index=5> ";
                break;
            case Note.G:
                res = "<sprite index=6> ";
                break;
            case Note.A:
                res = "<sprite index=0> ";
                break;
            case Note.B:
                res = "<sprite index=1> ";
                break;
            case Note.C1:
                res = "#<sprite index=2> ";
                break;
            case Note.D1:
                res = "#<sprite index=3> ";
                break;
            case Note.E1:
                res = "#<sprite index=4> ";
                break;
            case Note.F1:
                res = "#<sprite index=5> ";
                break;
            case Note.G1:
                res = "#<sprite index=6> ";
                break;
            case Note.A1:
                res = "#<sprite index=0> ";
                break;
            case Note.B1:
                res = "#<sprite index=1> ";
                break;
            case Note.C2:
                res = "b<sprite index=2> ";
                break;
            case Note.D2:
                res = "b<sprite index=3> ";
                break;
            case Note.E2:
                res = "b<sprite index=4> ";
                break;
            case Note.F2:
                res = "b<sprite index=5> ";
                break;
            case Note.G2:
                res = "b<sprite index=6> ";
                break;
            case Note.A2:
                res = "b<sprite index=0> ";
                break;
            case Note.B2:
                res = "b<sprite index=1> ";
                break;
        }
        return res;
    }

    public static void PlayNoteSound(Note note)
    {
        switch (note)
        {
            case Note.None:
                break;
            case Note.C:
                SoundSystem.instance.Play("C");
                break;
            case Note.D:
                SoundSystem.instance.Play("D");
                break;
            case Note.E:
                SoundSystem.instance.Play("E");
                break;
            case Note.F:
                SoundSystem.instance.Play("F");
                break;
            case Note.G:
                SoundSystem.instance.Play("G");
                break;
            case Note.A:
                SoundSystem.instance.Play("A");
                break;
            case Note.B:
                SoundSystem.instance.Play("B");
                break;
            case Note.C1:
                SoundSystem.instance.Play("C1");
                break;
            case Note.D1:
                SoundSystem.instance.Play("D1");
                break;
            case Note.E1:
                SoundSystem.instance.Play("E1");
                break;
            case Note.F1:
                SoundSystem.instance.Play("F1");
                break;
            case Note.G1:
                SoundSystem.instance.Play("G1");
                break;
            case Note.A1:
                SoundSystem.instance.Play("A1");
                break;
            case Note.B1:
                SoundSystem.instance.Play("B1");
                break;
            case Note.C2:
                SoundSystem.instance.Play("C2");
                break;
            case Note.D2:
                SoundSystem.instance.Play("D2");
                break;
            case Note.E2:
                SoundSystem.instance.Play("E2");
                break;
            case Note.F2:
                SoundSystem.instance.Play("F2");
                break;
            case Note.G2:
                SoundSystem.instance.Play("G2");
                break;
            case Note.A2:
                SoundSystem.instance.Play("A2");
                break;
            case Note.B2:
                SoundSystem.instance.Play("B2");
                break;
        }
    }

    public void OnPressUndo()
    {
        GameResultComparer.instance.Remove();
        SyncNoteDisplayer();
    }

    public void Clear()
    {
        GameResultComparer.instance.Clear();
        SyncNoteDisplayer();
    }
}