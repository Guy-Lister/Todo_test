import React, { useState } from 'react';
import { useQuery, useMutation, gql } from '@apollo/client';
import TodoList from './components/TodoList';
import TodoForm from './components/TodoForm';
import EditTodoModal from './components/EditTodoModal';

// GraphQL queries and mutations
const GET_TODOS = gql`
  query GetTasks {
    tasks {
      id
      title
      description
      isCompleted
      createdAt
      completedAt
    }
  }
`;

const CREATE_TODO = gql`
  mutation CreateTask($title: String!, $description: String!) {
    createTask(title: $title, description: $description) {
      id
      title
      description
      isCompleted
      createdAt
    }
  }
`;

const UPDATE_TODO = gql`
  mutation UpdateTask($id: UUID!, $title: String!, $description: String!, $isCompleted: Boolean!) {
    updateTask(id: $id, title: $title, description: $description, isCompleted: $isCompleted) {
      id
      title
      description
      isCompleted
      completedAt
    }
  }
`;

const UPDATE_TODO_STATUS = gql`
  mutation UpdateTaskStatus($id: UUID!, $isCompleted: Boolean!) {
    updateTaskStatus(id: $id, isCompleted: $isCompleted) {
      id
      isCompleted
      completedAt
    }
  }
`;

const DELETE_TODO = gql`
  mutation DeleteTask($id: UUID!) {
    deleteTask(id: $id)
  }
`;

function App() {
  const [editTodo, setEditTodo] = useState(null);
  const { loading, error, data } = useQuery(GET_TODOS);
  
  const [createTodo] = useMutation(CREATE_TODO, {
    refetchQueries: [{ query: GET_TODOS }]
  });
  
  const [updateTodo] = useMutation(UPDATE_TODO, {
    refetchQueries: [{ query: GET_TODOS }]
  });

  const [updateTodoStatus] = useMutation(UPDATE_TODO_STATUS, {
    refetchQueries: [{ query: GET_TODOS }]
  });
  
  const [deleteTodo] = useMutation(DELETE_TODO, {
    refetchQueries: [{ query: GET_TODOS }]
  });

  const handleCreateTodo = (title, description) => {
    createTodo({ variables: { title, description } });
  };

  const handleUpdateTodo = (id, title, description, isCompleted) => {
    updateTodo({ 
      variables: { 
        id,
        title,
        description,
        isCompleted
      } 
    });
    setEditTodo(null);
  };

  const handleToggleTodo = (todo) => {
    updateTodoStatus({ 
      variables: { 
        id: todo.id,
        isCompleted: !todo.isCompleted 
      } 
    });
  };

  const handleDeleteTodo = (id) => {
    if (window.confirm('Are you sure you want to delete this todo?')) {
      deleteTodo({ variables: { id } });
    }
  };

  const handleEditClick = (todo) => {
    setEditTodo(todo);
  };

  const handleCloseModal = () => {
    setEditTodo(null);
  };

  if (loading) return <div className="container"><p>Loading...</p></div>;
  if (error) return <div className="container"><p>Error: {error.message}</p></div>;

  return (
    <div className="container">
      <div className="todo-app">
        <h1>Todo App</h1>
        <TodoForm onCreateTodo={handleCreateTodo} />
        <TodoList 
          todos={data.tasks}
          onToggleTodo={handleToggleTodo}
          onDeleteTodo={handleDeleteTodo}
          onEditTodo={handleEditClick}
        />
        {editTodo && (
          <EditTodoModal 
            todo={editTodo}
            onClose={handleCloseModal}
            onUpdateTodo={handleUpdateTodo}
          />
        )}
      </div>
    </div>
  );
}

export default App; 