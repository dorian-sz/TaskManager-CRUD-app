import './App.css';
import Tasks from './pages/Tasks';
import Layout from './components/Layout';
import RequireAuth from './components/RequireAuth';
import {Route, Routes} from "react-router-dom";
import Welcome from './pages/Welcome';

function App() {
  return (
    <Routes>
      <Route path='/' element={<Layout/>}>
        {/*Public routes*/}
        <Route path='' element={<Welcome/>}/>
        
        {/*Protected routes*/}
        <Route element={<RequireAuth/>}>
          <Route path='tasks' element={<Tasks/>}/>
        </Route>
  
      </Route>
    </Routes>
  );
}

export default App;
