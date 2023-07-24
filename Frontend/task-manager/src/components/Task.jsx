import React from 'react'
import "./styles/Task.css";
import { Fetch } from './Fetch';

const Task = ({task, refresh}) => {

    const deleteTask = async() => {
        await Fetch("Task", "DELETE");
        refresh();
    }
    return (
        <div className='task-container' key={task.userTaskID}>
            <input type="checkbox" className='task-checkbox' />
            <p className='task-name'>{task.taskName}</p>
            <p className='task-description'>{task.taskDescription}</p>
            <button className='task-delete-btn' onClick={deleteTask}>Delete</button>
        </div>
    )
}

export default Task