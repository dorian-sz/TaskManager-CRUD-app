import React, { useEffect, useState } from 'react';
import useAuth from '../hooks/useAuth';
import Task from '../components/Task';
import AddTask from '../components/AddTask';
import "./styles/Tasks.css";

const taskUrl = "http://localhost:5084/api/Task/userstasks";


const Tasks = () => {
    const {auth} = useAuth();
    const [tasks, setTasks] = useState([]);
    const userID = auth?.userID;

    const  getTasks = async () => {
        let response = await fetch(`${taskUrl}/${userID}`, {
            method : 'GET',
            headers : {'Content-Type' : 'application/json'},
            credentials : 'include'
        });
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