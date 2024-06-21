# TD-UDP-Chat

TD-UDP-Chat is a Windows Forms application built using C#. This application allows users to chating with each other by UDP Protocol. Additionally, it provides functionality to display fonts,color of fonts,type of fonts.

## Features

- **View Messages**: Displays a list of all messages by users.
- **Change Fonts**: Provides information about font .
- **Change Color Font**: Allows users to change color of fonts.
- **Save chat**: Allows users to save chat in file.


## Getting Started

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) with .NET Desktop Development workload installed.
- Windows operating system.

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/toliaa/TD-UDP-Chat.git
    ```
2. Open the solution file (`UDP Chat.sln`) in Visual Studio.
3. Build the solution to restore dependencies and compile the project.

### Usage

1. Run the application from Visual Studio by pressing `F5` or `Ctrl+F5`.
2. Input IP and port.
3. Input message
4. Use the Export button to save chat to a text file.

## Code Structure

- **ProcessViewForm.cs**: Contains the main logic for loading, displaying, and managing processes.
- ***ProcessViewForm.Designer.cs**: Contains the designer-generated code for UI layout and component initialization.
- **Program.cs**: Entry point of the application, starting the `Form1` form.


## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
