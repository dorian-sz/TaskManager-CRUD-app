import React, { useEffect, useState } from 'react';
import "./styles/AddTask.css";

const taskUrl = "http://localhost:5084/api/Task";

const AddTask = ({userID}) => {
    const [TaskName, setTaskName] = useState('');
    const [TaskDescription, setTaskDescription] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();

        const response = await fetch(`${taskUrl}/${userID}`, {
            method : "POST",
            headers : {
                "Content-Type" : "application/json"
            },
            credentials : 'include',
            body : JSON.stringify({TaskName, TaskDescription})
        })
        setTaskName('');
        setTaskDescription('');
    }
    

    return (
        <div className="task-form-container">
            <form onSubmit={handleSubmit} className="task-form">
                <div className="form-input-container">
                    <input type="text" className="form-input" id="task-name" placeholder="Task name"
                        onChange={e => setTaskName(e.target.value)} value={TaskName} required
                    />
                </div>
                <div className="form-input-container">
                    <input type="text" className="form-input" id="task-description" placeholder="Task description"
                        onChange={e => setTaskDescription(e.target.value)} value={TaskDescription} required
                    />
                </div>
                <button className="form-task-submit-btn" type="submit">Add task</button>
            </form> 
        </div>
    )
}

export default AddTask