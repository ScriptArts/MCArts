/*
 * The purpose of this class is to render only the chunks
 * that are within the render distance property.
 * 
 * Author: Corey St-Jacques
 */

using System.Collections.Generic;
using UnityEngine;

using ChunkMaster.Unity.Framework;

namespace ChunkMaster
{
    public class GraphicalRenderDistance : MonoBehaviour
    {
        // Chunks to be rendered
        private static List<Vector3> renderBlocks = new List<Vector3>();

        // Chunks currently rendered
        public static List<UnityChunk> renderedBlocks = new List<UnityChunk>();

        // The player target to render distance
        public Transform target;

        // The current mouse position
        public Vector3 currentPosition;
        private Vector3 previousPosition;

        // The render distance in chunks
        private int _renderDistance;
        public int RenderDistance
        {
            get
            {
                return _renderDistance;
            }
            set
            {
                _renderDistance = value;
                getRenderBlocks();
            }
        }

        // Start method
        void Start()
        {
            RenderDistance = 3; // Set render distance to 3 chunks
        }

        // Update method
        void Update()
        {
            CheckOnPositionChange();
        }

        // Checks if the player has moved to a new chunk
        private void CheckOnPositionChange()
        {
            currentPosition =
                target.position.Snap(WorldController.world.chunkSize);
            if (previousPosition != currentPosition)
            {
                previousPosition = currentPosition;
                ShowRenderChunks();
            }
        }

        // Enabled and disables chunks to be rendered
        private void ShowRenderChunks()
        {
            UnityChunk[] tmpRenderBlocks = new UnityChunk[renderedBlocks.Count];
            renderedBlocks.CopyTo(tmpRenderBlocks);

            foreach (UnityChunk chunk in tmpRenderBlocks)
            {
                if (chunk != null)
                    if (chunk.gameObject != null)
                        chunk.gameObject.SetActive(
                            renderBlocks.Contains(currentPosition
                                - chunk.worldPosition)
                            );
                    else
                        renderedBlocks.Remove(chunk);
                else
                    renderedBlocks.Remove(chunk);
            }
        }

        // Get Render Block positions to be rendered
        private void getRenderBlocks()
        {
            Vector3 position;
            renderBlocks.Clear();
            for (int x = -RenderDistance; x < RenderDistance; x++)
            {
                for (int y = -RenderDistance; y < RenderDistance; y++)
                {
                    for (int z = -RenderDistance; z < RenderDistance; z++)
                    {
                        position = new Vector3(x, y, z);
                        if (Vector3.Distance(position, Vector3.zero) <= RenderDistance)
                            renderBlocks.Add(position * WorldController.world.chunkSize);

                    }
                }
            }
        }

    }
}
