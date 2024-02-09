import React, { useState } from 'react';
import { useFetch } from '../../hooks/useFetch';


const AddTask = ({userID, refresh}) => {
    const [TaskName, setTaskName] = useState('');
    const [TaskDescription, setTaskDescription] = useState('');
    const {customFetch} = useFetch();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const body = JSON.stringify({TaskName, TaskDescription});
        await customFetch(`api/Task/${userID}`, "POST", body)
        setTaskName('');
        setTaskDescription('');
        refresh();
    }
    

    return (
            <form onSubmit={handleSubmit} className='flex flex-col gap-y-4 md:gap-y-0 md:flex-row w-full h-full justify-between items-center p-4'>
                <div className='w-full md:w-1/3'>
                    <input type="text" className='italic p-1 w-full tracking-wide rounded-md bg-zinc-100 focus:outline-none text-lg' id="task-name" placeholder="Task name"
                        onChange={e => setTaskName(e.target.value)} value={TaskName} required
                    />
                </div>
                <div className='w-full md:w-2/5'>
                    <textarea rows={3} className='italic w-full p-1 tracking-wide rounded-md bg-zinc-100 focus:outline-none text-lg' id="task-description" placeholder="Task description"
                            onChange={e => setTaskDescription(e.target.value)} value={TaskDescription} required
                        />
                </div>
                <div className='w-full md:w-1/5'>

                    <button className="bg-green-700 w-full p-2 rounded-md text-white font-semibold hover:bg-green-800" type="submit">Add task</button>
                </div>
            </form> 
    )
}

export default AddTask