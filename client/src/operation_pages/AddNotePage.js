import { useState } from "react";
import { Link, useParams, useNavigate } from "react-router-dom";
import { createNote } from "../apiCalls/notes";
import quotes from "../json/quotes.json";
import toast, { Toaster } from "react-hot-toast";

function AddNotePage() {

    const { subjectId } = useParams();

    const [formData, setFormData] = useState({
        noteTitle: ""
    });

    const [error, setError] = useState({
        noteTitle: ""
    });

    const navigate = useNavigate();

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

        const randomIndex = Math.floor(Math.random() * quotes.length);
        const quote = quotes[randomIndex].text;
        
        const str = `[{\"insert\":\"Quote\"},{\"attributes\":{\"header\":1},\"insert\":\"\\n\"},{\"attributes\":{\"background\":\"#ffffff\",\"color\":\"#444444\"},\"insert\":\"${quote}\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\n\"}]`

        const body = { 
            noteTitle: formData.noteTitle,
            noteContent: str
        };

        console.log(body)

        createNote(subjectId, body)
        .then(msg => {
            console.log(msg.data);
            toast.success('Your note have been successfuly created!', {
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

    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <form onSubmit={handleSubmit} >
                    <h2>Create Note</h2>
                    <div className="operation-inputs" >
                        <div className="operation-flex-column">
                            <label>Subject name</label>
                            <input 
                                type="text"
                                name="subjectName"
                                placeholder="subject name"
                                value={formData.noteTitle}
                                onChange={handleChange}
                            />
                            <div className="input-error" >{ error.noteTitle && error.noteTitle }</div>
                        </div>
                    </div>
                    <div className="flex-operation-buttons" >
                        <Link to={`/subjects/${subjectId}/notes`} ><button className="cancel-button" >cancel</button></Link>
                        <button type="submit" className="full-button" >create</button>
                    </div>
                </form>
                <Toaster />
            </div>
        </div>
    )
}

export default AddNotePage;