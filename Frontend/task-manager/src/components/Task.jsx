import React from 'react'
import "./styles/Task.css";

const taskUrl = "http://localhost:5084/api/Task";

const Task = ({task, refresh}) => {

    const deleteTask = async() => {
        await fetch(`${taskUrl}/${task.userTaskID}`, {
            method : "DELETE",
            headers : {
                "Content-Type" : "application/json"
            },
            credentials : 'include',
        })

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