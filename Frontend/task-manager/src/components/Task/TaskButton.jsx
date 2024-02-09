import React from 'react'
import { twMerge } from 'tailwind-merge'

const TaskButton = ({className, onclick, label}) => {
  return (
    <button className={twMerge('p-2 text-white font-semibold rounded-md', className)} onClick={onclick}>{label}</button>
  )
}

export default TaskButton