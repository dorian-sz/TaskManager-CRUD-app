import React from 'react'
import "./styles/Task.css";

const Task = ({task}) => {
    console.log(task)
    return (
        <div className='task-container' key={task.userTaskID}>
            <input type="checkbox" className='task-checkbox' />
            <p className='task-name'>{task.taskName}</p>
            <p className='task-description'>{task.taskDescription}</p>
            <button className='task-delete-btn'>Delete</button>
        </div>
    )
}

export default Task