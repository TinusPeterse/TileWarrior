using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGen : ItemLayOut
{
    public Tile[] Monsters;
    private List<Tile> LevelMonsters;
    private Tile CurrentMonster;
    private int Level;
    int Difficulty = 0;
    public void GenerateNext()
    {
        Difficulty = 0;
        while ((Difficulty < Level && LevelMonsters.Count < 3 + Level) || Difficulty > Level * 1.5)
        {
            LevelMonsters.Add(Monsters[Random.Range(0, Monsters.Length - 1)]);
            Difficulty += CalculateDifficulty(CurrentMonster);
            if (LevelMonsters.Count > 5 + Level)
            {
                Difficulty -= CalculateDifficulty(LevelMonsters[0]);
                LevelMonsters.RemoveAt(0);
            }
        }
        AllTiles = LevelMonsters.ToArray();
    }
    public int CalculateDifficulty(Tile YEET)
    {
        int Mult = 0;
        int Difficulty = 0;
        if (YEET.MonsterType == Tile.TypeMonster.Potion)
        {
            Mult = -3;
        }
        else if (YEET.MonsterType == Tile.TypeMonster.Armor)
        {
            Mult = -1;
        }
        else if (YEET.MonsterType == Tile.TypeMonster.Monster)
        {
            Mult = 2;
        }
        Difficulty = Mult * YEET.DamHeal;
        return Difficulty;
    }
}