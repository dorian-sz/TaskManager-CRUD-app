import React, { useEffect, useState } from 'react';
import Task from '../components/Task/Task';
import AddTask from '../components/Task/AddTask';
import { useAuthContext } from '../hooks/useAuthContext';
import { useGetTasks } from '../hooks/useGetTasks';

const Tasks = () => {
    const [tasks, setTasks] = useState([]);
    const {user} = useAuthContext();
    const [userId, setUserId] = useState();
    const {getTasks} = useGetTasks();

    const refresh = async() => {
        if (userId) {
            const data = await getTasks(userId);
            setTasks(data);
        }
    }

    useEffect(() => {
        if (user) {
            setUserId(user.userID)
            fetch(`${process.env.REACT_APP_API_URL}api/Task/userstasks/${user.userID}`, {
                headers : {
                    "Content-Type" : "application/json"
                },
                credentials : 'include',
            })
            .then(response => response.json())
            .then(data => setTasks(data))
        }
    }, [user])

    return (
        <div className='flex flex-col gap-y-4 items-center justify-center w-full overflow-hidden  h-full'>
            <div className='w-1/2 md:w-1/5 p-2'>
                <p className='text-3xl italic font-bold underline'>Your tasks</p>
            </div>
            <div className='w-full h-4/6 overflow-y-auto border-y-2'>
                {tasks.length > 0 && tasks.map((task) => <Task key={task.userTaskID} task={task} refresh={refresh} />)}
                {tasks.length === 0 && 
                    <div className='flex items-center justify-center w-full h-full p-2'>
                        <p className='text-3xl italic font-bold text-zinc-200'>No tasks</p>
                    </div>
                }
            </div>
            <div className='flex flex-col items-center w-full'>
                <div className='flex justify-center w-full md:w-1/5 p-2'>
                    <p className='text-2xl italic font-bold underline'>Add a new task</p>
                </div>
                <AddTask userID={userId} refresh={refresh}/>            
            </div>
        </div>
    );
};

export default Tasks;