using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分区结构声明
/// </summary>
public class Section
{
    private static int length = 16;
    public CubeType[,,] cube = new CubeType[length, length, length];

    public static int Length 
    {
        get {
            return length;
        }
    }

    public Section() { }

    public bool SectionBuilder(int line)
    {
        //实际的第一遍生成世界由分区的初始化进行完成
        //根据地平线与分区的位置关系进行三种不同的初始化
        if (line == length)
        {
            //地平线不在分区内且高于分区
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        cube[x, z, y] = CubeType.Stone;
                    }
                }
            }
            return true;
        }
        else if (line == 0) 
        {
            //地平线不在分区内且低于分区
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        cube[x, z, y] = CubeType.Air;
                    }
                }
            }
            return false;
        }
        else{
            //地平线位于分区中
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        if (line - 3 > 0)
                        {
                            if (y <= line - 3)
                            {
                                cube[x, y, z] = CubeType.Stone;
                            }
                            else if (y > line)
                            {
                                cube[x, y, z] = CubeType.Air;
                            }
                            else
                            {
                                cube[x, y, z] = CubeType.Grass;
                            }
                        }
                        else 
                        {
                            if (y <= line)
                            {
                                cube[x, y, z] = CubeType.Grass;
                            }
                            else 
                            {
                                cube[x, y, z] = CubeType.Air;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }

}

/// <summary>
/// 区块结构声明
/// </summary>
public class Chunk
{
    /// <summary>
    /// 区块中含有的分区数
    /// </summary>
    private static int height = 16;
    /// <summary>
    /// 当前区块中分区的信息
    /// </summary>
    public Section[] sectionsList = new Section[height];
    /// <summary>
    /// 标记内容分区，true为非空分区，false为空分区
    /// </summary>
    private bool[] activeList = new bool[height];

    public static int Height 
    {
        get {
            return height;
        }
    }

    public bool[] ActiveList 
    {
        get {
            return activeList;
        }
    }

    public Chunk(int horizon)
    {
        //在第一遍的世界生成中区块只负责标记分区状态以及向分区传入地平线信息
        for (int i = 0; i < height; i++)
        {
            if ((i + 1) * Section.Length < horizon - Section.Length)
            {
                sectionsList[i] = new Section();
                activeList[i] = sectionsList[i].SectionBuilder(Section.Length);
            }
            else if ((i + 1) * Section.Length <= horizon)
            {
                sectionsList[i] = new Section();
                activeList[i] = sectionsList[i].SectionBuilder(horizon - (Section.Length * (i + 1)));
            }
            else 
            {
                sectionsList[i] = new Section();
                activeList[i] = sectionsList[i].SectionBuilder(0);
            }
        }
    }


}