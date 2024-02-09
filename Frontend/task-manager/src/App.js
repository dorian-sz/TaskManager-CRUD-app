import './App.css';
import Tasks from './pages/Tasks';
import {Route, Routes} from "react-router-dom";
import Profile from './pages/Profile';
import AuthLayout from './outlet/AuthLayout';
import AuthenticationPage from './pages/AuthenticationPage';

function App() {
  return (
    <Routes>
        {/*Public routes*/}
        <Route path='/' element={<AuthenticationPage/>}/>
        
        {/*Protected routes*/}
        <Route element={<AuthLayout/>}>
          <Route path='tasks' element={<Tasks/>}/>
          <Route path='profile' element={<Profile/>}/>
        </Route>
    </Routes>
  );
}

export default App;
