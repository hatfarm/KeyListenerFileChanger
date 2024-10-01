// <copyright file="GlobalKeyListenerConfig.cs" company="PlaceholderCompany">
// Licensed under the MIT License. See the LICENSE file for more details.
// </copyright>

namespace GlobalKeyListener.Config;

using System.Text.Json.Serialization;
using GlobalKeyListener.Serialization;

public record GlobalKeyListenerConfig
{
    /// <summary>
    /// The directory where the files will be created.
    /// </summary>
    required public string FileCreationDirectory { get; init; }

    /// <summary>
    /// This is used to map a button to a file that it will update.
    /// </summary>
    [JsonConverter(typeof(DictionaryKeysJsonConverter))]
    required public Dictionary<Keys, string> ButtonToFileMap { get; init; }

    /// <summary>
    /// The key combo that will exit the application.
    /// </summary>
    [JsonConverter(typeof(KeysArrayJsonConverter))]
    required public Keys[] AppExitKeyCombo { get; set; }

    /// <summary>
    /// The key combo that will toggle file creation.
    /// </summary>
    [JsonConverter(typeof(KeysArrayJsonConverter))]
    required public Keys[] FileCreationToggleCombo { get; set; }
}
