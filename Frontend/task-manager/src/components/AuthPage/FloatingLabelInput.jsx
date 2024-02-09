import React from 'react'

const FloatingLabelInput = ({label, id, type, setFormData, formData, isLoading}) => {
  return (
    <div class="relative">
        <input type={type} id={id} class="
        block px-2.5 pb-2.5 pt-4 w-full text-sm text-gray-900 bg-transparent rounded-md border-1 border
         appearance-none focus:outline-none focus:ring-0 focus:border-blue-900 peer disabled:opacity-75" 
         placeholder="" onChange={(e) => setFormData({...formData, [e.target.id]: e.target.value})} disabled={isLoading}/>
        <label for={id} class="absolute font-semibold text-sm text-blue-900 duration-300 transform -translate-y-4 scale-75 top-2 z-10 origin-[0] bg-white px-2 peer-focus:px-2 peer-focus:text-blue-900 peer-placeholder-shown:scale-100 peer-placeholder-shown:-translate-y-1/2 peer-placeholder-shown:top-1/2 peer-focus:top-2 peer-focus:scale-75 peer-focus:-translate-y-4 rtl:peer-focus:translate-x-1/4 rtl:peer-focus:left-auto start-1">{label}</label>
    </div>
  )
}

export default FloatingLabelInput