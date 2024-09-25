import { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import Subject from '../components/Subject';
import '../styles/subjectPage.css';

function AllSubjectsPage() {

    const [subjects, setSubjects] = useState([]);

    useEffect(() => {
        axios.get("http://localhost:5262/subjects")
        .then(msg => {
            setSubjects(msg.data)
            console.log(msg);
        }).catch(err => {
            console.log(err);
        });
    }, [])

    return (
        <div className="flex-center">
            <div className="outlet-container" >
                <div className="header-section-line flex-space-between">
                    <h1>My Subjects</h1>
                    <Link to="add-subject" ><button>Create subject</button></Link>
                </div>
                <div className="subjects-list">
                    {
                        subjects.map(subject => <Subject {...subject} />) 
                    }
                </div>
            </div>
        </div>
    )
}

export default AllSubjectsPage;