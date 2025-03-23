import React from 'react';
import { List, ListItem, ListItemText, Checkbox, IconButton, Typography } from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';

interface Task {
  id: string;
  title: string;
  description?: string;
  isCompleted: boolean;
  createdAt: string;
  completedAt: string | null;
}

interface TaskListProps {
  tasks: Task[];
  onTaskComplete: (taskId: string) => void;
  onTaskDelete: (taskId: string) => void;
}

export const TaskList: React.FC<TaskListProps> = ({ tasks, onTaskComplete, onTaskDelete }) => {
  if (tasks.length === 0) {
    return (
      <Typography variant="body1" color="text.secondary" align="center">
        No tasks
      </Typography>
    );
  }

  return (
    <List>
      {tasks.map((task) => (
        <ListItem
          key={task.id}
          secondaryAction={
            <IconButton
              edge="end"
              aria-label="delete"
              onClick={() => onTaskDelete(task.id)}
            >
              <DeleteIcon />
            </IconButton>
          }
        >
          <Checkbox
            edge="start"
            checked={task.isCompleted}
            onChange={() => onTaskComplete(task.id)}
            aria-label={`${task.title} completion status`}
          />
          <ListItemText
            primary={task.title}
            secondary={task.description}
            sx={{
              textDecoration: task.isCompleted ? 'line-through' : 'none',
              color: task.isCompleted ? 'text.secondary' : 'text.primary',
            }}
          />
        </ListItem>
      ))}
    </List>
  );
}; 