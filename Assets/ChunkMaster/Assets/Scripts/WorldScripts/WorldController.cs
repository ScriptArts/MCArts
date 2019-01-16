/*
 * The purpose of this class is to show you how easy it can be to create
 * an infinite world. The input for nearly all object manipulation requires
 * a point in world space, and the output is a world save file.
 * 
 * Author: Corey St-Jacques
*/

using UnityEngine;

using ChunkMaster.Utils;
using ChunkMaster.Unity.Framework;

namespace ChunkMaster
{
    // This is a sample for using and manipulating a world class.
    public class WorldController : MonoBehaviour
    {

        // Instance set to static to be accessed by other classes.
        public static WorldController Instance;

        // The current world
        public static UnityWorld world;

        // Your block palette here
        public Transform[] blocks;

        // Use this for initialization
        void Awake()
        {
            // Get this instance
            Instance = this;

            // World Constructor
            world = new UnityWorld(transform, "myWorld");
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Spawn Block
        public void SpawnBlock(UnityBlock block)
        {
            // The block's world position.
            Vector3 worldPosition = block.worldPosition;

            // The block's content.
            object content = block.GetContent();

            // Spawn block code here!
            Transform tmp = Instantiate(blocks[(int)content]);
            tmp.position = worldPosition + new Vector3(0.5f, 0.5f, 0.5f);
            tmp.SetParent(block.gameObject.transform);
        }

        // Iterates through all chunks.
        public void SpawnChunks(UnityChunk chunk)
        {
            foreach (UnityBlock block in chunk.blocks)
            {
                if (block != null)  // Does the block exist?
                    SpawnBlock(block);
            }
        }

        // Iterates through all chunks.
        public void SpawnSector(UnitySector sector)
        {
            foreach (UnityChunk chunk in sector.chunks)
            {
                if (chunk != null)
                {  // Does the Chunk exist?
                    SpawnChunks(chunk);
                    GraphicalRenderDistance.renderedBlocks.Add(chunk); //Add 
                }
            }
        }

        // Set the block in world space.
        public void SetBlock(Vector3 point, object content)
        {
            UnityBlock block;
            UnityChunk chunk;

            block = world.SetBlock(point, content) as UnityBlock;
            chunk = block.chunk as UnityChunk;

            SpawnBlock(block);

            if (!GraphicalRenderDistance.renderedBlocks.Contains(chunk))
                GraphicalRenderDistance.renderedBlocks.Add(chunk);
        }

        // Retrieves a block.
        public UnityBlock GetBlock(Vector3 point)
        {
            return world.GetBlock(point) as UnityBlock;
        }

        // Retrieves a chunk.
        public UnityChunk GetChunk(Vector3 point)
        {
            return world.GetChunk(point) as UnityChunk;
        }

        // Retrieves a sector.
        public UnitySector GetSector(Vector3 point)
        {
            return world.GetSector(point) as UnitySector;
        }

        // Clear the world.
        public void ClearWorld()
        {
            world.ClearSectors();
        }

        // Clears a sector given a point in world space.
        public void ClearSector(Vector3 point)
        {
            world.GetSector(point).Destroy();
        }

        // Clears a chunk given a point in world space.
        public void ClearChunk(Vector3 point)
        {
            world.GetChunk(point).Destroy();
        }

        // Clears a block given a point in world space.
        public void ClearBlock(Vector3 point)
        {
            world.ClearBlock(point);
        }

        // Save the entire world.
        public string SaveWorld(string path = "")
        {
            return world.SaveWorld(path);
        }

        // Load a sector into memory.
        public UnitySector LoadSector(Vector3 point, string path = "")
        {
            return world.LoadSector(point, path) as UnitySector;
        }

        // Save a sector into an external file.
        public string SaveSector(Vector3 point, string path = "")
        {
            return world.SaveSector(point, path);
        }

        // Deletes all files and directory for the world save files.
        public bool DeleteWorldFile(string path = "")
        {
            if (path.Equals(""))
                path = Application.dataPath;
            path += "/" + world.name;
            IO.DestroyDirectory(path);
            return !IO.FileExists(path);
        }
    }
}