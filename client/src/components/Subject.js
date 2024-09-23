import { Link } from "react-router-dom";

function Subject({ subjectName, subjectDescription, subjectId }) {
    return (
        <div className="subjectTile" >
            <Link className="subject-button" to={`${subjectId}/notes`} ><h2 className="subject-h2" >{subjectName}</h2></Link>
            <div className="subject-content" >
                <div className="subject-desc" >{subjectDescription}</div>
                <div className="subject-bottom" >
                    <div className="subject-details" >
                        <div>13 notes</div>
                        <div>created 13.09.24</div>
                    </div>
                    {/* <Link to={`delete-subject/${subjectId}`} ><button className="delete-button" >delete</button></Link> */}
                    <Link to={`${subjectId}/delete-subject`} ><button className="delete-button" >delete</button></Link>
                </div>
            </div>
        </div>
    )
}

export default Subject;