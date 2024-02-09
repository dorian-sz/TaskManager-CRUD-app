import React from 'react'
import { twMerge } from 'tailwind-merge'

const TaskInput = ({object, value, setValue, id, className}) => {
  return (
    <input id={id} className={twMerge('italic p-1 tracking-wide rounded-md bg-zinc-100 focus:outline-none text-lg', className)} value={value} onChange={e => setValue({...object, [e.target.id]: e.target.value})}></input>
  )
}

export default TaskInput