# GlobalKeyListener

This project is a global key listener that allows you to map specific keys to create files with unique contents. It provides a simple way to listen for key presses and generate files based on the pressed keys.

## Prerequisites

- .NET Framework 4.7.2 or higher

## Getting Started

1. Clone the repository or download the source code.
2. Open the solution in Visual Studio.
3. Build the solution to restore NuGet packages.
4. Run the application.

## Usage

1. Set the path to the folder that will hold your files in the `Program.cs` file:
2. Define the button-to-file mapping in the `GetButtonToFileMap` method in the `Program.cs` file:
This mapping determines which keys will trigger the creation of specific files.

3. Run the application. It will start listening for key presses.
4. Press the mapped keys to create files with unique contents.

## Customization

- You can modify the file save path and the button-to-file mapping to suit your needs.
- Additional customization options can be explored by modifying the code in the respective files.

## License

This project is licensed under the [MIT License](LICENSE).
