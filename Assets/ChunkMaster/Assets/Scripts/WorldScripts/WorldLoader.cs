/*
 * The purpose of this class is to load in sectors from the world file directory.
 * Sectors will only be loaded if the files exist and if the pointer enters a new sector.
 * 
 * Author:Corey St-Jacques
 */


using System.Collections.Generic;
using UnityEngine;

using ChunkMaster.Unity.Framework;
using ChunkMaster.Utils;

namespace ChunkMaster
{
    public class WorldLoader : MonoBehaviour
    {
        // The current loaded sectors;
        public static Dictionary<string, UnitySector> loadedSectors = new Dictionary<string, UnitySector>();

        // The current mouse position
        public Vector3 currentPosition;
        private Vector3 previousPosition;

        // Start Method
        void Start()
        {
            previousPosition = currentPosition + Vector3.one; // Make sure that these are not the same on startup
        }

        // Update is called once per frame
        void Update()
        {
            CheckOnSectorChange();
        }

        // Checks if mouse is in a new sector
        void CheckOnSectorChange()
        {
            currentPosition =
                InputScreen.MousePosition.Snap(WorldController.world.chunkSize * WorldController.world.sectorSize);
            if (previousPosition != currentPosition)
            {
                string key = currentPosition.ToString();
                previousPosition = currentPosition;

                if (!loadedSectors.ContainsKey(key))
                {
                    // Attempting to read a sector file
                    if (IO.FileExists(Application.dataPath + "/" + WorldController.world.name + "/" + (currentPosition / WorldController.world.chunkSize / WorldController.world.sectorSize)))
                    {
                        UnitySector sector = WorldController.Instance.LoadSector(InputScreen.MousePosition);

                        if (sector.isOccupied) // If sector has blocks
                            WorldController.Instance.SpawnSector(sector);
                        loadedSectors.Add(key, sector);
                    }

                }

            }
        }
    }
}
