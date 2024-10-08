import { useEffect, useState } from "react";
import { getSubjectWithNotes } from "../apiCalls/subjects";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Hashtag from "../components/Hashtag";
import hashtagsJson from "../json/hashtags"
import Note from "../components/Note";

function SubjectPage() {

    const { subjectId } = useParams();

    const [subjectData, setSubjectData] = useState({
        subjectId: "",
        subjectName: "",
        subjectDescription: "",
        dateOfCreation: "",
        hashtags: "",
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

    let hashtagsArray;

    try {
        hashtagsArray = JSON.parse(subjectData.hashtags);
    } catch {
        hashtagsArray = []
    }

    return (
        <div className="flex-center">
            <div className="outlet-container">
                <div className="header-section-line">
                    <h1>{subjectData.subjectName}</h1>
                    <div>
                        <Link to={`/subjects/${subjectId}/edit-subject`} ><button className="operation-button" ><span>Edit</span><FontAwesomeIcon icon="fa-solid fa-pen-to-square" /></button></Link>
                        <Link to="/subjects" ><button className="cancel-button" ><span>Subjects</span><FontAwesomeIcon icon="fa-right-from-bracket" /></button></Link>
                    </div>
                </div>
                <div className="subject-all" >
                    <div className="subject-props" >
                        <div className="subject-desc" >{subjectData.subjectDescription}</div>
                        <div className="datails" >
                            <div className="details-item" >{`${subjectData.notes.length} ${subjectData.notes.length == 1 ? "note" : "notes"}`}</div>
                            <div className="details-item" >{`created ${subjectData.dateOfCreation}`}</div>
                        </div>
                    </div>
                    <div className="hashtags-section" >
                        {
                            hashtagsArray && hashtagsArray.length > 0 &&
                            hashtagsArray.map(hash => <Hashtag title={hash} {...hashtagsJson[hash]} />)
                        }
                    </div>
                </div>
                <div className="header-section-line">
                    <h1 className="h1-notes" >Notes</h1>
                    <Link to="add-note" ><button><span>Create note</span><FontAwesomeIcon icon="fa-solid fa-file-circle-plus" /></button></Link>
                </div>
                {
                    <div className="notes-list" >
                        { subjectData.notes.map(note => <Note noteId={note.noteId} noteTitle={note.noteTitle} dateOfCreation={note.dateOfCreation} /> ) }
                    </div>
                }
                
            </div>
        </div>
    )
}

export default SubjectPage;