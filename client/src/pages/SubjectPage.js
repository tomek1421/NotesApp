import { useEffect, useState } from "react";
import { getSubjectWithNotes } from "../apiCalls/subjects";
import { Link, useParams } from "react-router-dom";

import Note from "../components/Note";

function SubjectPage() {

    const { subjectId } = useParams();

    const [subjectData, setSubjectData] = useState({
        subjectId: "",
        subjectName: "",
        subjectDescription: "",
        notes: []
    });

    useEffect(() => {
        getSubjectWithNotes(subjectId)
        .then(msg => {
            setSubjectData(msg.data);
        })
        .catch(err => {
            console.log(err);
        })
    }, []);

    return (
        <div className="flex-center">
            <div className="outlet-container">
                <div className="header-section-line flex-space-between">
                    <h1>{subjectData.subjectName}</h1>
                    <div>
                        <Link to={`/subjects/${subjectId}/edit`} ><button className="cancel-button" >edit</button></Link>
                        <Link to="/subjects" ><button className="cancel-button" >back to subjects</button></Link>
                    </div>
                </div>
                <div className="subject-props" >
                    <div className="subject-desc desc" >{subjectData.subjectDescription}</div>
                    <div className="datails" >
                        <div className="details-item" >13 notes</div>
                        <div className="details-item" >created 13.05.24</div>
                    </div>
                </div>
                <div className="header-section-line flex-space-between">
                    <h1>Notes</h1>
                    <Link to="add-note" ><button>create note</button></Link>
                </div>
                <div className="notes-list" >
                    {
                        subjectData.notes.length > 0 ?
                        subjectData.notes.map(note => <Note noteId={note.noteId} noteTitle={note.noteTitle} /> ) :
                        "no notes yet"
                    }
                </div>
            </div>
        </div>
    )
}

export default SubjectPage;