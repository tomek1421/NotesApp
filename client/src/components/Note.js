import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

function Note({ noteId, noteTitle }) {
    return (
        <Link to={noteId} style={{ color: 'inherit', textDecoration: 'none'}}>
            <div className="note-tile" >
                <div>{noteTitle}</div>
                <div className="note-left" >
                    <div>
                        <div className="date" >created 24.03.24</div>
                        <Link to={`${noteId}/edit-note`} ><button className="operation-button" ><span>Edit</span><FontAwesomeIcon icon="fa-solid fa-pen-to-square" /></button></Link>
                        <Link to={`${noteId}/delete-note`} ><button className="delete-button" >Delete</button></Link>
                    </div>
                </div>
            </div>
        </Link>
    )
}

export default Note;