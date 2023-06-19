import './App.css';
import Navigation from './components/Navigation';
import Login from './pages/Login';
import Home from './pages/Home';
import Register from './pages/Register';
import {BrowserRouter, Route, Routes} from "react-router-dom";

function App() {
  return (
    <div className="App">
      <Login></Login>
    </div>
  );
}

export default App;
