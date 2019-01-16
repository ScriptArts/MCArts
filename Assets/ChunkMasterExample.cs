using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ChunkMaster.Unity.Framework;

public class ChunkMasterExample : MonoBehaviour {

    public static UnityWorld world;

    public Transform[] blocks;

	// Use this for initialization
	void Start () {
        world = new UnityWorld(transform);

        for(int i = 0; i < 10; i++)
        {
            Vector3 point = new Vector3(i, 1, 1);
            
            var block = world.SetBlock(point, 0);
            SpawnBlock(block);
        }
	}

    public void SpawnBlock(UnityBlock block)
    {
        int i = (int)block.GetContent();
        Vector3 point = block.worldPosition;
        SpawnPrefab(blocks[i], point, block.gameObject.transform);
    }
	
    public Transform SpawnPrefab(Transform prefab, Vector3 point, Transform parent)
    {
        Transform tmp = Instantiate(prefab);
        tmp.SetParent(transform);
        tmp.position = point;
        return tmp;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
