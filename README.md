# Task Manager with Real-Time Sync

A modern task management application built with React Spectrum, Relay, ASP.NET Core, and GraphQL.

## Project Overview

This project demonstrates a full-stack task management application with real-time synchronization capabilities. It uses modern technologies and follows best practices for both frontend and backend development.

### Technology Stack

#### Frontend
- React 18 with Adobe React Spectrum for UI components
- Relay for GraphQL client
- Modern CSS with Flexbox
- Docker containerization

#### Backend
- ASP.NET Core (.NET 9.0)
- HotChocolate GraphQL server
- Entity Framework Core with SQLite
- Clean Architecture pattern

## Features

- Create and manage tasks with title and description
- Toggle task status between "Pending" and "Completed"
- Real-time updates using GraphQL
- Modern, accessible UI using React Spectrum
- Persistent storage using SQLite
- Containerized deployment with Docker

## Project Structure

### Backend Structure
```
TodoApp/
├── TodoApp.Api/           # GraphQL API endpoints and configuration
├── TodoApp.Core/          # Domain entities and interfaces
└── TodoApp.Infrastructure/# Data access and implementations
```

### Frontend Structure
```
todo-frontend/
├── src/
│   ├── components/       # React Spectrum components
│   ├── relay/           # Relay configurations and fragments
│   ├── App.js           # Main application component
│   └── index.js         # Application entry point
└── public/              # Static assets
```

## Getting Started

### Prerequisites
- Docker and Docker Compose
- Node.js 14+ (for local development)
- .NET 9.0 SDK (for local development)

### Running with Docker

1. Clone the repository
2. Start the application:
   ```bash
   docker-compose up --build
   ```
3. Access the application:
   - Frontend: http://localhost
   - GraphQL Playground: http://localhost:5001/graphql

### Local Development

#### Backend
```bash
cd TodoApp.Api
dotnet restore
dotnet run
```

#### Frontend
```bash
cd todo-frontend
npm install
npm start
```

## GraphQL API

### Queries
```graphql
query GetAllTasks {
  getAllTasks {
    id
    title
    description
    status
  }
}
```

### Mutations
```graphql
# Create Task
mutation CreateTask($title: String!, $description: String) {
  createTask(title: $title, description: $description) {
    id
    title
    status
  }
}

# Update Task Status
mutation UpdateTaskStatus($id: UUID!, $status: TaskStatus!) {
  updateTaskStatus(id: $id, status: $status) {
    id
    status
  }
}
```

## Development Approach

This project was developed using an AI-assisted workflow, leveraging:
- GitHub Copilot for code suggestions
- ChatGPT for architecture decisions and debugging
- Cursor IDE for AI-powered development

The AI tools significantly accelerated development by:
- Generating boilerplate code
- Suggesting best practices
- Helping with Docker configuration
- Debugging and error resolution


## License

MIT 