import React from 'react'
import { twMerge } from 'tailwind-merge'

const TaskTextarea = ({id, className, value, setValue, taskDTO}) => {
  return (
    <textarea rows={2} id={id} className={twMerge('italic p-1 tracking-wide rounded-md bg-zinc-100 focus:outline-none text-lg', className)} value={value} onChange={e => setValue({...taskDTO, [e.target.id]: e.target.value})}></textarea>
  )
}

export default TaskTextarea