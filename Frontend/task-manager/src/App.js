import './App.css';
import Navigation from './components/Navigation';
import Login from './pages/Login';
import Tasks from './pages/Tasks';
import Register from './pages/Register';
import {Route, Routes} from "react-router-dom";
import Layout from './components/Layout';

function App() {
  return (
    <Routes>
      <Route path='/' element={<Layout/>}>
        {/*Public routes*/}
        <Route path='login' element={<Login/>}/>
        <Route path='register' element={<Register/>}/>
        
        {/*Protected routes*/}
        <Route path='/' element={<Tasks/>}/>
  
      </Route>
    </Routes>
  );
}

export default App;
