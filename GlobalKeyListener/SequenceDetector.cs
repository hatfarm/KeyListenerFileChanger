// <copyright file="SequenceDetector.cs" company="PlaceholderCompany">
// Licensed under the MIT License. See the LICENSE file for more details.
// </copyright>

namespace GlobalKeyListener;

/// <summary>
/// A class that detects a sequence of keys being pressed.
/// </summary>
internal class SequenceDetector
{
    private readonly Action sequenceDetected;
    private int currentIndex;

    public SequenceDetector(Keys[] sequence, Action sequenceDetected)
    {
        this.Sequence = sequence;
        this.currentIndex = 0;
        this.sequenceDetected = sequenceDetected;
    }

    public Keys[] Sequence { get; init; }

    public void OnKeyPressed(Keys key)
    {
        if (key == this.Sequence[this.currentIndex])
        {
            Console.WriteLine($"Detected key: {key}");
            this.currentIndex++;
            if (this.currentIndex == this.Sequence.Length)
            {
                this.sequenceDetected();
                this.currentIndex = 0; // Reset for next detection
            }
        }
        else
        {
            Console.WriteLine($"Didn't use key: {key}");
            this.currentIndex = 0; // Reset if the sequence is broken
        }
    }
}
