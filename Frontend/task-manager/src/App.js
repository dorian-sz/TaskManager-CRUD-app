import './App.css';
import Login from './pages/Login';
import Tasks from './pages/Tasks';
import Register from './pages/Register';
import Layout from './components/Layout';
import RequireAuth from './components/RequireAuth';
import {Route, Routes} from "react-router-dom";

function App() {
  return (
    <Routes>
      <Route path='/' element={<Layout/>}>
        {/*Public routes*/}
        <Route path='login' element={<Login/>}/>
        <Route path='register' element={<Register/>}/>
        
        {/*Protected routes*/}
        <Route element={<RequireAuth/>}>
          <Route path='tasks' element={<Tasks/>}/>
        </Route>
  
      </Route>
    </Routes>
  );
}

export default App;
