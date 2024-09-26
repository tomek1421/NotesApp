import './styles/App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import HomePage from './pages/HomePage';
import AllSubjectsPage from './pages/AllSubjectsPage';
import AddSubjectPage from './operation_pages/AddSubjectPage';
import DeleteSubjectPage from './operation_pages/DeleteSubjectPage';
import SubjectPage from './pages/SubjectPage';
import NotePage from './pages/NotePage';
import AddNotePage from './operation_pages/AddNotePage';
import DeleteNotePage from './operation_pages/DeleteNotePage';
import UpdateSubjectPage from './operation_pages/UpdateSubjectPage';
import TasksPage from './pages/TasksPage';
import SemesterPlanPage from './pages/SemesterPlanPage';
import { PreviousLocationProvider } from './components/PreviousLocationContext';

// import the library
import { library } from '@fortawesome/fontawesome-svg-core'

// import your icons
import { fab } from '@fortawesome/free-brands-svg-icons'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { faL } from '@fortawesome/free-solid-svg-icons';

function App() {
  return (
    <BrowserRouter>
      <PreviousLocationProvider>  
        <Routes>
          <Route path="/" element={<Layout />} >
            <Route path="" element={<HomePage />} />
            <Route path="subjects">
              <Route index element={<AllSubjectsPage />}/>
              <Route path="add-subject" element={<AddSubjectPage />} />
              <Route path=":subjectId/delete-subject" element={<DeleteSubjectPage />} />
              <Route path=":subjectId/edit" element={<UpdateSubjectPage />} />
              <Route path=":subjectId/notes">
                <Route index element={<SubjectPage />} />
                <Route path=":noteId" element={<NotePage />} />
                <Route path="add-note" element={<AddNotePage />} />
                <Route path=":noteId/delete-note" element={<DeleteNotePage />} />
              </Route>
            </Route>
            <Route path="tasks" element={<TasksPage />} />
            <Route path="plan" element={<SemesterPlanPage />} />
          </Route>
        </Routes>
      </PreviousLocationProvider>
    </BrowserRouter>
  );
}

export default App;

library.add(fab, fas, far, faL);