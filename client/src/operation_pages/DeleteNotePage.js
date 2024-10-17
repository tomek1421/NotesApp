import { useParams } from "react-router-dom";
import { deleteNote } from "../apiCalls/notes";
import { Link, useNavigate } from 'react-router-dom';
import toast, { Toaster } from "react-hot-toast";

function DeleteNotePage() {

    const { subjectId, noteId } = useParams();

    const navigate = useNavigate();

    function handleDelete() {
        deleteNote(subjectId, noteId)
        .then(msg => {
            console.log(msg.data);
            toast.success('Successfully deleted note!', {
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
                <h2>Delete Note</h2>
                <div className="operation-inputs" >
                    Are you sure you want to permanently delete this note?
                    <br/>
                    ID: {noteId}
                </div>
                <div className="operation-buttons" >
                    <Link to={`/subjects/${subjectId}/notes`} ><button className="cancel-button" >Cancel</button></Link>
                    <button onClick={handleDelete} type="submit" className="delete-full-button" >Delete</button>
                </div>
                <Toaster />
            </div>
        </div>
    )
}

export default DeleteNotePage;