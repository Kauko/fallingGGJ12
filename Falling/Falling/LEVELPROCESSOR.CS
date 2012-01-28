using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = Falling.Level;

namespace Falling
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "LevelProcessor")]
    public class LevelProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            string[] lines = input.Split(new char[] {'\n'});
            int rows = Convert.ToInt32(lines[2]);
            int cols = Convert.ToInt32(lines[3]);

            string[,] levelData = new string[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                string[] values = lines[r + 4].Split(new char[] { ' ' });
                for (int c = 0; c < cols; c++)
                {
                    levelData[r, c] = values[c];
                }
            }

            return new Falling.Level(levelData);
            
        }
    }
}