import React, { useEffect, useState } from 'react';
import "./styles/AddTask.css";
import { Fetch } from './Fetch';

const AddTask = ({userID, refresh}) => {
    const [TaskName, setTaskName] = useState('');
    const [TaskDescription, setTaskDescription] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const body = JSON.stringify({TaskName, TaskDescription});
        await Fetch(`Task/${userID}`, "POST", body)
        setTaskName('');
        setTaskDescription('');
        refresh();
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