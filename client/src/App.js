import './styles/App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import HomePage from './pages/HomePage';
import AllSubjectsPage from './pages/AllSubjectsPage';
import TasksPage from './pages/TasksPage';
import SemesterPlanPage from './pages/SemesterPlanPage';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />} >
          <Route path="" element={<HomePage />} />
          <Route path="subjects">
            <Route index element={<AllSubjectsPage />}/>
           
          </Route>
          <Route path="tasks" element={<TasksPage />} />
          <Route path="plan" element={<SemesterPlanPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
