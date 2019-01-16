using System;
using System.Collections.Generic;

using ChunkMaster.Utils;

namespace ChunkMaster.NET.Interfaces
{
    /// <summary>
    /// Block class is the meat of the world class. 
    /// The world contains sectors, which contain chunks, 
    /// which contains blocks. The blocks can be edited by 
    /// changing their values. Values are stored as a byte array.
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// This object's guid.
        /// </summary>
        Guid guid { get; set; }

        /// <summary>
        /// The block's details stored as a multidimensional byte array.
        /// </summary>
        byte[] content { get; set; }

        /// <summary>
        /// The block's related world.
        /// </summary>
        IWorld world { get; set; }

        /// <summary>
        /// The block's position in world space.
        /// </summary>
        Point worldPosition { get; set; }

        /// <summary>
        /// The block's chunk parent.
        /// </summary>
        IChunk chunk { get; set; }

        /// <summary>
        /// The block's sector parent.
        /// </summary>
        ISector sector { get; set; }

        /// <summary>
        /// Retrieves this block's current content.
        /// </summary>
        /// <returns>object</returns>
        object GetContent();

        /// <summary>
        /// Sets this block's current content with new content;
        /// </summary>
        /// <param name="obj">The object that you would like to set to the block's content.</param>
        void SetContent(object obj);

        /// <summary>
        /// The Destructor.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Displays block's details.
        /// </summary>
        /// <returns>string</returns>
        string ToString();
    }
}
