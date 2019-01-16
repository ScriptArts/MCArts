/* The world edit class allows the user to input keys or mouse clicks
 * to place or remove objects in the scene.
 * 
 * Author: Corey St-Jacques
 */

using UnityEngine;

namespace ChunkMaster
{
    public class WorldEditor : MonoBehaviour
    {

        // The current block to be placed
        public int currentBlock = 0;

        // Update is called once per frame
        void Update()
        {
            // If mouse pointer is hovering over the game screen.
            if (GameScreenControl.mouseOver)
            {
                // Left Clik - Set block
                if (Input.GetMouseButtonDown(0))
                    WorldController.Instance.SetBlock(
                        InputScreen.MousePosition,
                        currentBlock);

                // Right Click - Delete block
                if (Input.GetMouseButtonDown(1))
                    WorldController.Instance.ClearBlock(
                        InputScreen.BelowMousePosition);

                // Delete key - Delete the current chunk
                if (Input.GetKeyDown(KeyCode.Delete))
                    WorldController.Instance.ClearChunk(
                            InputScreen.MousePosition);

                // Escape key - Delete the current sector
                if (Input.GetKeyDown(KeyCode.Escape))
                    WorldController.Instance.ClearSector(
                            InputScreen.MousePosition);
            }
        }

        // Save the current world.
        public void SaveWorld()
        {
            print("World saved at: "
                + WorldController.Instance.SaveWorld());
        }

        // Clear entire world.
        public void ClearWorld()
        {
            WorldController.Instance.ClearWorld();
            print("World has been cleared successfully.");
        }

        // Delete world files.
        public void DeleteWorld()
        {
            WorldLoader.loadedSectors.Clear();
            ClearWorld();
            try
            {
                WorldController.Instance.DeleteWorldFile();
                print("World external files have been erased successfully.");
            }
            catch
            {
                // No such world directory exists.
            }
        }
    }
}