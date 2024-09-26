import { deleteSubject } from "../apiCalls/subjects";
import { Link, useNavigate, useParams } from 'react-router-dom';
import toast, { Toaster } from "react-hot-toast";
import { usePreviousLocation } from "../components/PreviousLocationContext";

function DeleteSubjectPage() {

    const { subjectId } = useParams();

    const navigate = useNavigate();

    const prevLocation = usePreviousLocation();

    function handleDelete() {
        deleteSubject(subjectId)
        .then(msg => {
            console.log(msg.data);
            toast.success('Successfully deleted subject!', {
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
        setTimeout(() => navigate("/subjects"), 1200);
    }
    
    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <h2>Delete Subject</h2>
                Are you sure you want to permanently delete this subject?
                <br/>
                ID: {subjectId}
                <div className="flex-operation-buttons" >
                    <Link to="/subjects" ><button className="cancel-button" >Cancel</button></Link>
                    <button onClick={handleDelete} type="submit" className="delete-full-button" >Delete</button>
                </div>
                <Toaster />
            </div>
        </div>
    )
}

export default DeleteSubjectPage;