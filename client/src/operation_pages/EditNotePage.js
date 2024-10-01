import { useEffect, useState } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import { getNote, editNoteTitle } from "../apiCalls/notes";
import toast, { Toaster } from "react-hot-toast";

function EditNotePage() {

    const { subjectId, noteId } = useParams();

    const [formData, setFormData] = useState({
        noteTitle: ""
    });

    const [error, setError] = useState({
        noteTitle: ""
    });

    const navigate = useNavigate();

    useEffect(() => {
        getNote(subjectId, noteId)
        .then(msg => {
            setFormData({ noteTitle: msg.data.noteTitle });
            console.log(msg.data.noteTitle);
        })
        .catch(err => {
            console.log(err);
        })
    }, []);

    function handleChange(event) {
        const value = event.target.value;

        setFormData({noteTitle: value});

        //validation
        if (value.length == 0)
            setError({noteTitle: "Field can't be empty"});
        else setError({noteTitle: ""});
    }

    function handleSubmit(event) {
        event.preventDefault();

        //validation
        if (formData.noteTitle.length == 0) {
            setError({noteTitle: "Field can't be empty"});
            return;
        }

        const body = { 
            noteTitle: formData.noteTitle,
        };

        // console.log(body)

    editNoteTitle(subjectId, noteId, body)
        .then(msg => {
            console.log(msg.data);
            toast.success('Your note have been successfuly updated!', {
                position: 'bottom-center',
                style: {
                    background: '#d0f3d3'
                },
                duration: 1100
            });
        })
        .catch(err => {
            console.log(err);
            toast.error('Something gone wrong!', {
                position: 'bottom-center',
                style: {
                    background: '#fcaeae'
                },
                duration: 1100
            });
        });
        setTimeout(() => navigate(`/subjects/${subjectId}/notes`), 1200);
    }

    const titleStyle = error.noteTitle ? {border: "1px solid red"} : null;

    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <form onSubmit={handleSubmit} >
                    <h2>Edit Note</h2>
                    <div className="operation-inputs" >
                        <div className="operation-flex-column">
                            <label>Note name</label>
                            <input 
                                type="text"
                                name="subjectName"
                                placeholder="note name"
                                value={formData.noteTitle}
                                onChange={handleChange}
                                style={titleStyle}
                            />
                            <div className="input-error" >{ error.noteTitle && error.noteTitle }</div>
                        </div>
                    </div>
                    <div className="flex-operation-buttons" >
                        <Link to={`/subjects/${subjectId}/notes`} ><button className="cancel-button" >Cancel</button></Link>
                        <button type="submit" className="full-button" >Edit</button>
                    </div>
                </form>
                <Toaster />
            </div>
        </div>
    )
}

export default EditNotePage;