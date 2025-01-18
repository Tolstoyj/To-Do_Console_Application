# Task Manager Console Application

A C# console application for managing tasks with features like adding, updating, completing, and deleting tasks.

## Features

- Add tasks with descriptions and due dates
- View all tasks (sorted by completion status and due date)
- Mark tasks as completed
- Update task descriptions and due dates
- Delete individual tasks
- Delete all tasks
- Persistent storage (tasks are saved to a JSON file)

## Requirements

- .NET 9.0 SDK
- Any text editor or IDE (Visual Studio, VS Code, Rider, etc.)

## Getting Started

1. Clone the repository:
```bash
git clone [repository-url]
```

2. Navigate to the project directory:
```bash
cd HelloRider
```

3. Build the project:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

## Project Structure

- `Models/` - Contains the TodoTask model
- `Services/` - Contains TaskManager and ConsoleUI services
- `Tests/` - Contains unit tests for the application

## Running Tests

```bash
dotnet test
```

## Contributing

1. Fork the repository
2. Create a new branch
3. Make your changes
4. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details. 