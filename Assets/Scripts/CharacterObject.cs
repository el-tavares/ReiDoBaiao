using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class CharacterObject : ScriptableObject
{
    public string fullName;
    public EType type;
    public Sprite sprite;
}

public enum EType
{
    Unknow,
    Emo,
    Goat
}
