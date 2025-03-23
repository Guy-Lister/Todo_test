import React from 'react';

const TodoList = ({ todos, onToggleTodo, onDeleteTodo, onEditTodo }) => {
  if (!todos || todos.length === 0) {
    return <p>No todos yet. Add one above!</p>;
  }

  return (
    <ul className="todo-list">
      {todos.map(todo => (
        <li key={todo.id} className="todo-item">
          <input
            type="checkbox"
            className="todo-checkbox"
            checked={todo.isCompleted}
            onChange={() => onToggleTodo(todo)}
          />
          <div className={`todo-text ${todo.isCompleted ? 'completed' : ''}`}>
            <h3>{todo.title}</h3>
            <p>{todo.description}</p>
          </div>
          <div className="todo-actions">
            <button
              className="btn btn-primary"
              onClick={() => onEditTodo(todo)}
            >
              Edit
            </button>
            <button
              className="btn btn-delete"
              onClick={() => onDeleteTodo(todo.id)}
            >
              Delete
            </button>
          </div>
        </li>
      ))}
    </ul>
  );
};

export default TodoList; 