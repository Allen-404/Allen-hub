using UnityEngine;

[CreateAssetMenu]
public class RoundsDefinition : ScriptableObject
{
    public int m_NumRoundsToWin = 5;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public float anyVariable;
}
