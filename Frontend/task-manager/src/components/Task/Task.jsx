import React, { useEffect, useState } from 'react'
import { useFetch } from '../../hooks/useFetch';
import TaskInput from './TaskInput';
import TaskButton from './TaskButton';
import TaskTextarea from './TaskTextarea';

const Task = ({task, refresh}) => {
    const [taskDTO, setTaskDTO] = useState({})
    const {customFetch} = useFetch();

    const deleteTask = async(id) => {
        await customFetch(`api/Task/${id}`, "DELETE");
        refresh();
    }

    const updateTask = async() => {
        await customFetch(`api/Task`,"PUT", JSON.stringify(taskDTO));
    }

    useEffect(()=>{
        setTaskDTO(task)
    }, [task])
    return (
        <div className='flex w-full justify-between items-center p-4 border-b-2 border-dashed overflow-x-auto gap-x-4' key={taskDTO?.userTaskID}>
            <TaskInput object={taskDTO} id={"taskName"} value={taskDTO?.taskName} setValue={setTaskDTO}/>
            <TaskTextarea object={taskDTO} id={"taskDescription"} value={taskDTO?.taskDescription} className={"block min-h-[auto] items-center md:items-start"} setValue={setTaskDTO} />
            <div className='flex gap-x-2'>
                <TaskButton onclick={updateTask} className={'bg-blue-900 hover:bg-blue-950'} label={"Update"}/>
                <TaskButton onclick={() => deleteTask(task?.userTaskID)} className={'bg-red-700 hover:bg-red-800'} label={"Delete"} />
            </div>

        </div>
    )
}

export default Task