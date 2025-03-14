﻿namespace SixShaded.CatGlance;

public static class Extensions
{
    public static CatGlanceableTest ToGlancable(this DeTes.Declaration.IDeTesTest deTesTest, string name) =>
        new(name)
        {
            Declaration = deTesTest.Declaration,
            InitialMemory = deTesTest.InitialMemory,
        };
}