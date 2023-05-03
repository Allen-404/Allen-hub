using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    public Sprite enemySprite;
    public Note[] goals;
    public float interval = 1f;
}