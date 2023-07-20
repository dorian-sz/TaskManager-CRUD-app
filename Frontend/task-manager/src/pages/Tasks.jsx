import React, { useEffect, useState } from 'react';
import useAuth from '../hooks/useAuth';
import Task from '../components/Task';
import AddTask from '../components/AddTask';
import "./styles/Tasks.css";
import { Fetch } from '../components/Fetch';

const Tasks = () => {
    const {auth} = useAuth();
    const [tasks, setTasks] = useState([]);
    const userID = auth?.userID;

    const  getTasks = async () => {
        const response = await Fetch(`Task/userstasks/${userID}`, "GET")
        const fetchedTasks = await response.json();
        setTasks(fetchedTasks);
    }

    useEffect(()=>{
        getTasks();
        return;
    },[])

    return (
        <div className='tasks-container'>
            <AddTask userID={userID} refresh={getTasks}/>
            {tasks ? tasks.map((task) => <Task task={task} refresh={getTasks}/>) : <></>}
            
        </div>
    );
};

export default Tasks;