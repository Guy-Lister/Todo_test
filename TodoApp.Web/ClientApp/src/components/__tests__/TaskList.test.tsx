import React from 'react';
import { render, screen, fireEvent } from '@testing-library/react';
import { TaskList } from '../TaskList';

// Mock the task data
const mockTasks = [
  {
    id: '1',
    title: 'Test Task 1',
    description: 'Test Description 1',
    isCompleted: false,
    createdAt: new Date().toISOString(),
    completedAt: null
  },
  {
    id: '2',
    title: 'Test Task 2',
    description: 'Test Description 2',
    isCompleted: true,
    createdAt: new Date().toISOString(),
    completedAt: new Date().toISOString()
  }
];

describe('TaskList', () => {
  const mockOnTaskComplete = jest.fn();
  const mockOnTaskDelete = jest.fn();

  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('renders all tasks', () => {
    render(
      <TaskList
        tasks={mockTasks}
        onTaskComplete={mockOnTaskComplete}
        onTaskDelete={mockOnTaskDelete}
      />
    );

    // Check if all tasks are rendered
    expect(screen.getByText('Test Task 1')).toBeInTheDocument();
    expect(screen.getByText('Test Description 1')).toBeInTheDocument();
    expect(screen.getByText('Test Task 2')).toBeInTheDocument();
    expect(screen.getByText('Test Description 2')).toBeInTheDocument();
  });

  it('displays correct completion status for each task', () => {
    render(
      <TaskList
        tasks={mockTasks}
        onTaskComplete={mockOnTaskComplete}
        onTaskDelete={mockOnTaskDelete}
      />
    );

    // Check completion status
    const checkboxes = screen.getAllByRole('checkbox');
    expect(checkboxes[0]).not.toBeChecked();
    expect(checkboxes[1]).toBeChecked();
  });

  it('calls onTaskComplete when a task is checked', () => {
    render(
      <TaskList
        tasks={mockTasks}
        onTaskComplete={mockOnTaskComplete}
        onTaskDelete={mockOnTaskDelete}
      />
    );

    const checkboxes = screen.getAllByRole('checkbox');
    fireEvent.click(checkboxes[0]);

    expect(mockOnTaskComplete).toHaveBeenCalledWith(mockTasks[0].id);
  });

  it('calls onTaskDelete when delete button is clicked', () => {
    render(
      <TaskList
        tasks={mockTasks}
        onTaskComplete={mockOnTaskComplete}
        onTaskDelete={mockOnTaskDelete}
      />
    );

    const deleteButtons = screen.getAllByRole('button', { name: /delete/i });
    fireEvent.click(deleteButtons[0]);

    expect(mockOnTaskDelete).toHaveBeenCalledWith(mockTasks[0].id);
  });

  it('displays empty state when no tasks are provided', () => {
    render(
      <TaskList
        tasks={[]}
        onTaskComplete={mockOnTaskComplete}
        onTaskDelete={mockOnTaskDelete}
      />
    );

    expect(screen.getByText(/no tasks/i)).toBeInTheDocument();
  });
}); 