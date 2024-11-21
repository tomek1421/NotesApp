import './styles/App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import HomePage from './pages/HomePage';
import AllSubjectsPage from './pages/AllSubjectsPage';
import AddSubjectPage from './operation_pages/AddSubjectPage';
import DeleteSubjectPage from './operation_pages/DeleteSubjectPage';
import EditSubjectPage from './operation_pages/EditSubjectPage';
import SubjectPage from './pages/SubjectPage';
import NotePage from './pages/NotePage';
import AddNotePage from './operation_pages/AddNotePage';
import DeleteNotePage from './operation_pages/DeleteNotePage';
import EditNotePage from './operation_pages/EditNotePage';
import TimetablePage from './pages/TimetablePage';
import AddEventPage from './operation_pages/AddEventPage';
import DeleteEventPage from './operation_pages/DeleteEventPage';
import { PreviousLocationProvider } from './components/PreviousLocationContext';

// import the library
import { library } from '@fortawesome/fontawesome-svg-core'

// import your icons
import { fab } from '@fortawesome/free-brands-svg-icons'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { faL } from '@fortawesome/free-solid-svg-icons';
import EditEventPage from './operation_pages/EditEventPage';
import CalculatorPage from './pages/CalculatorPage';

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
              <Route path=":subjectId/edit-subject" element={<EditSubjectPage />} />
              <Route path=":subjectId/notes">
                <Route index element={<SubjectPage />} />
                <Route path=":noteId" element={<NotePage />} />
                <Route path="add-note" element={<AddNotePage />} />
                <Route path=":noteId/delete-note" element={<DeleteNotePage />} />
                <Route path=":noteId/edit-note" element={<EditNotePage />} />
              </Route>
            </Route>
            <Route path="calculator" element={<CalculatorPage />} />
            <Route path="timetable">
              <Route index element={<TimetablePage />} />
              <Route path="add-event" element={<AddEventPage />} />
              <Route path=":timetableEventId/delete-event" element={<DeleteEventPage />} />
              <Route path=":timetableEventId/edit-event" element={<EditEventPage />} />
            </Route>
          </Route>
        </Routes>
      </PreviousLocationProvider>
    </BrowserRouter>
  );
}

export default App;

library.add(fab, fas, far, faL);