using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MEETTHEMONSTERS : ScriptableObject
{
    public Sprite sprite;
    public Tile.TypeMonster typeMonster;
    public int DamHeal;
    public int LevelRequire;
    public int MaxLevel;
}
