import React, { useState } from 'react';

const TodoForm = ({ onCreateTodo }) => {
  const [title, setTitle] = useState('');
  const [description, setDescription] = useState('');
  const [showFullForm, setShowFullForm] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if (!title.trim()) {
      alert('Please enter a title for your todo!');
      return;
    }
    
    onCreateTodo(title, description);
    setTitle('');
    setDescription('');
    setShowFullForm(false);
  };

  return (
    <div>
      {!showFullForm ? (
        <div className="todo-form">
          <input
            type="text"
            placeholder="Add a new todo..."
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            onFocus={() => setShowFullForm(true)}
          />
          <button type="button" onClick={() => setShowFullForm(true)}>
            Add
          </button>
        </div>
      ) : (
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="title">Title</label>
            <input
              id="title"
              type="text"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              placeholder="Enter todo title"
              autoFocus
            />
          </div>
          <div className="form-group">
            <label htmlFor="description">Description</label>
            <textarea
              id="description"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Enter todo description (optional)"
            />
          </div>
          <div className="form-actions">
            <button
              type="button"
              className="btn btn-secondary"
              onClick={() => setShowFullForm(false)}
            >
              Cancel
            </button>
            <button type="submit" className="btn btn-primary">
              Add Todo
            </button>
          </div>
        </form>
      )}
    </div>
  );
};

export default TodoForm; 