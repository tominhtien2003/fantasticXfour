using System.Collections.Generic;
using UnityEngine;
public class LevelComposite : LevelCompoent
{
    private List<LevelLeaf> childrens = new List<LevelLeaf>();
    public override void Add(LevelLeaf component)
    {
        childrens.Add(component);
    }
    private void Start()
    {
        countLevel = 0;
        LevelLeaf[] levels = GetComponentsInChildren<LevelLeaf>(true);
        foreach (var level in levels)
        {
            level.numberLevel = ++countLevel;
            if (countLevel <= 3)
            {
                level.lockLevel = false;
            }
            Add(level);
        }
    }

    public override void Remove(LevelLeaf component)
    {
        childrens.Remove(component);
    }
}
