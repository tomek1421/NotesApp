import { Link, useNavigate, useParams } from 'react-router-dom';
import toast, { Toaster } from "react-hot-toast";
import { deleteTimetableEvent } from "../apiCalls/timetable";

function DeleteEventPage() {

    const { timetableEventId } = useParams();

    const navigate = useNavigate();

    function handleDelete() {
        deleteTimetableEvent(timetableEventId)
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
        setTimeout(() => navigate("/timetable"), 1200);
    }
    
    

    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <h2>Delete Subject</h2>
                <div className="operation-inputs" >
                    Are you sure you want to permanently delete this event?
                    <br/>
                    ID: {timetableEventId}
                </div>
                <div className="operation-buttons" >
                    <Link to="/timetable" ><button className="cancel-button" >Cancel</button></Link>
                    <button onClick={handleDelete} type="submit" className="delete-full-button" >Delete</button>
                </div>
                <Toaster />
            </div>
        </div>
    )
}

export default DeleteEventPage;