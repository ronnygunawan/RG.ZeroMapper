# Contributing to ZeroMapper

Thank you for your interest in contributing to ZeroMapper!

## Building the Project

### Prerequisites
- .NET 10 SDK or later

### Build
```bash
dotnet build
```

### Run Tests
```bash
dotnet test
```

## Development

### Project Structure
- `src/RG.ZeroMapper` - Core mapper source generator
- `src/RG.ZeroMapper.Structural` - Structural typing source generator
- `tests/RG.ZeroMapper.Tests` - xUnit tests with Shouldly assertions
- `samples/BasicSample` - Basic mapper usage example
- `samples/StructuralSample` - Structural typing example

### Testing
All tests use the Arrange-Act-Assert pattern with Shouldly for assertions.

### Code Style
- Follow standard C# conventions
- Use nullable reference types
- Write tests for new features

## Pull Requests
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Ensure tests pass
5. Submit a pull request

All pull requests are automatically built and tested via GitHub Actions.
