// <copyright file="FileWriter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GlobalKeyListener;

using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Our tool for writing to various files in a specific root.
/// It merely changes the contents of the file to a new hash.
/// We do this so that we can detect when the file has been written to.
/// </summary>
internal class FileWriter
{
    // This is the folder that our files will be saved to.
    private readonly string fileSaveRoot = "your/folder/here";

    /// <summary>
    /// Initializes a new instance of the <see cref="FileWriter"/> class.
    /// </summary>
    /// <param name="fileSaveRoot">The root directory we would like to save our files to.</param>
    internal FileWriter(string fileSaveRoot)
    {
        this.fileSaveRoot = fileSaveRoot;
    }

    /// <summary>
    /// Write to a file with contents that SHOULD be new.
    /// </summary>
    /// <param name="fileName">The name of the file (without the full path).</param>
    public void WriteFileWithNewContents(string fileName)
    {
        string filePath = Path.Combine(this.fileSaveRoot, fileName);

        // This is the text that we will be writing to the file.
        var result = default(byte[]);

        using (var stream = new MemoryStream())
        {
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(DateTime.Now.Ticks);
                writer.Write(fileName);
            }

            stream.Position = 0;

            using var hash = SHA256.Create();
            result = hash.ComputeHash(stream);
        }

        string fileContents = string.Concat(result.Select(res => res.ToString("x2")).ToArray());

        // This is the method that writes the text to the file.
        File.WriteAllText(filePath, fileContents);
    }
}
