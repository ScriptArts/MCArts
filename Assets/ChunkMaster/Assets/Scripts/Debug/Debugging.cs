/*
 * This class debugs the current world details.
 * Displays 3D cubes to represents sectors, chunks, and a few blocks.
 * 
 * Author: Corey St-Jacques
 * */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ChunkMaster.Utils;

namespace ChunkMaster
{
    public class Debugging : MonoBehaviour
    {

        public bool cubed = true;

        // Draw text on gui
        private void OnGUI()
        {

            // Build string
            string details = "World Details:\n WorldName: " + WorldController.world.name + "\n"
                    + " Loaded Sectors: " + WorldController.world.sectors.Count + "\n"
                + " ChunkSize: " + WorldController.world.chunkSize + "\n SectorSize: "
                    + WorldController.world.sectorSize + "\n"
                + "\nSector Details:\n"
                + " Sector Position: " + InputScreen.MousePosition.Snap(
                    WorldController.world.sectorSize * WorldController.world.chunkSize)
                    + " " + "\n"
                + "\nChunk Details:\n"
                + " Chunk Position: " + InputScreen.MousePosition.Snap(WorldController.world.chunkSize)
                    + " " + "\n"
                + "\nScanning Block At:\n "
                + InputScreen.BelowMousePosition + " " + "\n"
                + "\nPreview Block At:\n "
                + InputScreen.MousePosition.Snap() + " " + "\n"
                + "\nSave file location:\n "
                + ".../Assets/" + WorldController.world.name + "\n"
                + "\nLegend:\n"
                + "  Blue: Sector" + "\n"
                + "  Red: Chunk" + "\n"
                + "  Green: Block Placement" + "\n"
                + "  Black: Block Scanning" + "\n"
    ;

            // Draw text shadow
            GUI.color = Color.black;
            GUI.Label(new Rect(1, 1, Screen.width, Screen.height), details);

            // Draw main text
            GUI.color = Color.white;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), details);
        }

        // Draw all cubes in editor and game views.
        private void OnRenderObject()
        {
            // Draw Sector
            DrawSquare(InputScreen.MousePosition.Snap(WorldController.world.sectorSize * WorldController.world.chunkSize), WorldController.world.sectorSize * WorldController.world.chunkSize, Color.blue);

            // Draw Chunk
            DrawSquare(InputScreen.MousePosition.Snap(WorldController.world.chunkSize), WorldController.world.chunkSize, Color.red);

            // If we are not cubing, we should disable a square
            if (cubed)
            {
                // Draw Scanned Block
                DrawSquare(InputScreen.BelowMousePosition.Snap(), 1, Color.black);
            }

            // Draw Preview Block
            DrawSquare(InputScreen.MousePosition.Snap(), 1, Color.green);

        }

        // Vector3 to Point Conversion
        private Point V2P(Vector3 vector3)
        {
            return new Point(vector3.x, vector3.y, vector3.z);
        }

        // Point to Vector3 Conversion
        private Vector3 P2V(Point point)
        {
            return new Vector3((float)point.x, (float)point.y, (float)point.z);
        }

        // Draws a Cube
        private void DrawSquare(Point position, float size, Color color)
        {
            DrawSquare(new Vector3((float)position.x, (float)position.y, (float)position.z), size, color);
        }

        // Draws a Cube Overload
        private void DrawSquare(Vector3 position, float size, Color color)
        {
            CreateLineMaterial();
            lineMaterial.SetPass(0);
            GL.PushMatrix();
            GL.Begin(GL.LINES);

            GL.Color(color);

            drawLine(position,
                        new Vector3(position.x + size, position.y, position.z),
                        color);

            drawLine(position,
                        new Vector3(position.x, position.y, position.z + size),
                        color);

            drawLine(new Vector3(position.x + size, position.y, position.z),
                        new Vector3(position.x + size, position.y, position.z + size),
                        color);

            drawLine(new Vector3(position.x, position.y, position.z + size),
                        new Vector3(position.x + size, position.y, position.z + size),
                        color);


            if (cubed)
            {
                drawLine(position,
                    new Vector3(position.x, position.y + size, position.z),
                    color);

                drawLine(new Vector3(position.x, position.y + size, position.z),
                    new Vector3(position.x + size, position.y + size, position.z),
                    color);

                drawLine(new Vector3(position.x + size, position.y + size, position.z),
                    new Vector3(position.x + size, position.y, position.z),
                    color);


                drawLine(new Vector3(position.x, position.y + size, position.z),
                    new Vector3(position.x, position.y + size, position.z + size),
                    color);

                drawLine(new Vector3(position.x, position.y + size, position.z + size),
                    new Vector3(position.x, position.y, position.z + size),
                    color);


                drawLine(new Vector3(position.x, position.y + size, position.z + size),
                    new Vector3(position.x, position.y + size, position.z + size),
                    color);

                drawLine(new Vector3(position.x, position.y + size, position.z + size),
                    new Vector3(position.x + size, position.y + size, position.z + size),
                    color);

                drawLine(new Vector3(position.x + size, position.y + size, position.z + size),
                    new Vector3(position.x + size, position.y, position.z + size),
                    color);

                drawLine(new Vector3(position.x + size, position.y + size, position.z + size),
                    new Vector3(position.x + size, position.y + size, position.z),
                    color);
            }

            GL.End();
            GL.PopMatrix();
        }

        // Draws a Line
        public static void drawLine(Vector3 start, Vector3 end, Color color)
        {
            GL.Vertex3(start.x, start.y, start.z);
            GL.Vertex3(end.x, end.y, end.z);

            Debug.DrawLine(start, end, color);
        }

        static Material lineMaterial;
        static void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                // Unity has a built-in shader that is useful for drawing
                // simple colored things.
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                // Turn on alpha blending
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // Turn backface culling off
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                // Turn off depth writes
                lineMaterial.SetInt("_ZWrite", 0);
            }
        }
    }
}