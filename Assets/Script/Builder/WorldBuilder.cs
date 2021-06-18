using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 世界生成器
/// </summary>
public class WorldBuilder : Singleton<WorldBuilder>
{
    /// <summary>
    /// 地平线高度
    /// </summary>
    private int horizon = 62;
    
    /// <summary>
    /// 地平线高度
    /// </summary>
    public int Horizon 
    {
        get {
            return horizon;
        }
    }

    private void Awake()
    {
        // 测试用区块生成
        Chunk Test = new Chunk(horizon);
        Vector3 testStart = Vector3.zero;
        TestBuildingChunk(Test, testStart);
    }

    private void BuildingChunk(Chunk chunk, Vector3 start) 
    {

    }

    /// <summary>
    /// 测试用区块生成方法
    /// </summary>
    /// <param name="chunk">传入的区块信息</param>
    /// <param name="start">起始位置坐标</param>
    private void TestBuildingChunk(Chunk chunk, Vector3 start) 
    {
        GameObject chunkRoot = new GameObject();
        chunkRoot.name = "Chunk 0 (x:0, y:0)";
        Vector3 curStart = Vector3.zero;
        for (int i = 0; i < Chunk.Height; i++)
        {
            curStart = new Vector3(curStart.x, i * 16, curStart.z);
            print("=================初始化测试分区： " + (i + 1) + "号=================");
            print("分区坐标为：x-" + curStart.x + "，y-" + curStart.y + "，z-" + curStart.z);
            if (!chunk.ActiveList[i])
            {
                print("当前分区为空分区不执行生成");
                continue;
            }

            GameObject sectionRoot = new GameObject();
            sectionRoot.transform.SetParent(chunkRoot.transform);
            sectionRoot.name = "Section " + i;
            CubeType[,,] curCubeList = chunk.sectionsList[i].cube;
            for (int y = 0; y < Section.Length; y++)
            {
                GameObject planeRoot = new GameObject();
                planeRoot.transform.SetParent(sectionRoot.transform);
                planeRoot.name = "Plane " + y;
                for (int x = 0; x < Section.Length; x++)
                {
                    for (int z = 0; z < Section.Length; z++)
                    {
                        CubeType curCubeType = curCubeList[x, y, z];
                        if (curCubeType == CubeType.Air)
                        {
                            continue;
                        }
                        string curPath = CubePath.Instance.GetPath(curCubeType);
                        GameObject curObj = Resources.Load<GameObject>(curPath);
                        Transform transform = Instantiate(curObj).transform;
                        transform.position = new Vector3(curStart.x + x, curStart.y + y, curStart.z + z);
                        transform.SetParent(planeRoot.transform);
                    }
                }
            }
        }
    }
}
