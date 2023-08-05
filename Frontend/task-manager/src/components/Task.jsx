import React, { useEffect, useState } from 'react'
import "./styles/Task.css";
import { Fetch } from './Fetch';

const Task = ({task, refresh}) => {
    const [taskDTO, setTaskDTO] = useState({})

    const deleteTask = async() => {
        await Fetch(`Task/${taskDTO.userTaskID}`, "DELETE");
        refresh();
    }

    const updateTask = async() => {
        await Fetch("Task", "PUT", JSON.stringify(taskDTO));
    }

    useEffect(()=>{
        setTaskDTO(task)
    }, [task])
    return (
        <div className='task-container' key={taskDTO?.userTaskID}>
            <input type="checkbox" className='task-checkbox' />
            <input className='task-input' value={taskDTO?.taskName} onChange={e => setTaskDTO({...taskDTO, taskName: e.target.value})}></input>
            <input className='task-input' value={taskDTO?.taskDescription} onChange={e => setTaskDTO({...taskDTO, taskDescription: e.target.value})}></input>
            <button className='task-update-btn' onClick={updateTask}>Change</button>
            <button className='task-delete-btn' onClick={deleteTask}>Delete</button>
        </div>
    )
}

export default Task