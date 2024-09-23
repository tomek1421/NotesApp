import { Link } from "react-router-dom";

function Note({ noteId, noteTitle }) {
    return (
        <Link style={{ color: 'inherit', textDecoration: 'none'}} to={noteId} >
            <div className="note-tile" >
                {/* <Link to={noteId} ><button className="note-button" >{noteTitle}</button></Link> */}
                <div>{noteTitle}</div>
                <div className="note-left" >
                    <div className="date" >created 24.03.24</div>
                    <Link to={`${noteId}/delete-note`} ><button className="delete-button" >delete</button></Link>
                </div>
            </div>
        </Link>
    )
}

export default Note;