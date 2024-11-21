import React, { useEffect, useRef, useState } from 'react';
import Editor from '../components/Editor'
import Quill from 'quill';
import "quill/dist/quill.core.css";
import "quill/dist/quill.snow.css";
import { Link, useParams, useNavigate } from 'react-router-dom';
import { getNote, saveNote } from '../apiCalls/notes';
import toast, { Toaster } from "react-hot-toast";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const Delta = Quill.import('delta');

function NotePage() {
    const [range, setRange] = useState();
    const [lastChange, setLastChange] = useState({
        ops: []
    });
    const [readOnly, setReadOnly] = useState(false);

    // Use a ref to access the quill instance directly
    const quillRef = useRef();

    const { subjectId, noteId } = useParams();

    const str = "[{\"insert\":\"Title\"},{\"attributes\":{\"header\":1},\"insert\":\"\\n\"},{\"attributes\":{\"background\":\"#ffffff\",\"color\":\"#444444\"},\"insert\":\"First, solve the problem. Then, write the code. – John Johnson\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\n\"}]"

    const defaultJson = JSON.parse(str);

    const [noteData, setNoteData] = useState({
        noteTitle: "",
        noteContent: ""
    });

    const navigate = useNavigate();

    useEffect(() => {
        getNote(subjectId, noteId)
        .then(msg => {
            setNoteData(msg.data);
            const json = JSON.parse(msg.data.noteContent);
            quillRef.current?.setContents(json);

            setLastChange(values => ({...values, ops: []}));
        })
        .catch(err => {
            console.log(err);
        })
    }, []);

    function handleSave(event) {
        event.preventDefault();
        if (lastChange.ops.length === 0) return;
        const stringifyJson = JSON.stringify(quillRef.current?.getContents().ops);
        saveNote(subjectId, noteId, { noteContent: stringifyJson })
        .then(msg => {
            console.log(msg.data);
            toast.success('Your note have been saved!', {
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
        console.log(lastChange.ops);
        setLastChange(values => ({...values, ops: []}));
    }
    
    function handleUnsaveAlert(id) {
        toast.dismiss(id);
        navigate(`/subjects/${subjectId}/notes`);
    }

    function handleSaveAlert(event, id) {
        handleSave(event);
        toast.dismiss(id);
        navigate(`/subjects/${subjectId}/notes`);
    }

    function handleExit() {
        if (lastChange.ops.length == 0) {
            navigate(`/subjects/${subjectId}/notes`);
        } else {
            toast((t) => (
                <span className="unsaved-alert" >
                  File is unsaved!
                  <br />
                  Do you want to save it ?
                  <div>
                    <button className="delete-button" onClick={() => handleUnsaveAlert(t.id)}>
                        no
                    </button>
                    <button className="full-button" onClick={(event) => handleSaveAlert(event, t.id)} >
                        yes
                    </button>
                  </div>
                </span>
            ));
        }
    }

    console.log(lastChange);
    console.log(quillRef.current?.getText());
    
    return (
        <div className="flex-center" >
            <div className="outlet-container" >
                <div className="header-section-line flex-space-between">
                    <h1>{noteData.noteTitle}</h1>
                    <button className="cancel-button" onClick={handleExit} ><span>Notes</span><FontAwesomeIcon icon="fa-right-from-bracket" /></button>
                </div>
                <form onSubmit={handleSave} >
                    <div id="editor" >
                        <Editor
                            ref={quillRef}
                            readOnly={readOnly}
                            defaultValue={defaultJson}
                            onSelectionChange={setRange}
                            onTextChange={setLastChange}
                        />
                    </div>
                    <div className="note-readonly-section">
                        Read Only:{' '}
                        <input
                            type="checkbox"
                            value={readOnly}
                            onChange={(e) => setReadOnly(e.target.checked)}
                        />
                    </div>
                    {/* <button
                        className="controls-right"
                        type="button"
                        onClick={() => {
                            alert(quillRef.current?.getLength());
                        }}
                        >
                        Get Content Length
                    </button> */}
                    <div className="section-line" ></div>
                    <div className="flex-center margin" >
                        { 
                            lastChange.ops.length == 0 ? 
                            <button type="submit" className="disabled-button" disabled>Save the note</button> :
                            <button type="submit" onClick={handleSave}>Save the note</button> 
                        }
                    </div>
                </form>
                <Toaster />
            </div>
        </div>
  );
};

export default NotePage;