using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class CharacterObject : ScriptableObject
{
    public Sprite[] idleSprites;
    public Sprite[] walkLeftSprites;
    public Sprite[] walkRightSprites;
}