import './App.css';
import Navigation from './components/Navigation';
import Login from './pages/Login';
import Home from './pages/Home';
import Register from './pages/Register';
import {BrowserRouter, Route, Routes} from "react-router-dom";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Navigation></Navigation>
        <main>
        
          <Routes>
            <Route path='/' exact Component={Home}/>
            <Route path='/login' Component={Login}/>
            <Route path='/register' Component={Register}/>
          </Routes>
        
      </main>
      </BrowserRouter>
    </div>
  );
}

export default App;
