using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分区结构声明
/// </summary>
public class Section
{
    static byte length = 16;
    public short[,,] cubeList = new short[16,16,16];
}

/// <summary>
/// 区块结构声明
/// </summary>
public class Chunk 
{
    static byte height = 16;
    public byte[] activeList = new byte[16];
    public 

}