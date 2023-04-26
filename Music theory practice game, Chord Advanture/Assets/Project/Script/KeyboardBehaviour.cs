using UnityEngine;
using System;
using UnityEngine.UI;

public class KeyboardBehaviour : MonoBehaviour
{
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
    }
}