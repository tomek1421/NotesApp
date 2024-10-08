import { Link } from "react-router-dom";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import Hashtag from "../components/Hashtag";
import hashtagsJson from "../json/hashtags"

function Subject({ subjectName, subjectId, dateOfCreation, hashtags, notesCount }) {
    
    // console.log(hashtags)

    const hashtagsArray = JSON.parse(hashtags);

    return (
        <div className="subject-tile" >
            <div className="header" >
                <h2>{subjectName}</h2>
                <div>
                    <Link to={`${subjectId}/edit-subject`} style={{ color: 'inherit', textDecoration: 'inherit'}}><FontAwesomeIcon icon="fa-regular fa-pen-to-square" size="xl" /></Link>
                    <Link to={`${subjectId}/delete-subject`} style={{ color: 'inherit', textDecoration: 'inherit'}}><FontAwesomeIcon icon="fa-regular fa-trash-can" size="xl" /></Link>
                </div>
            </div>
            <div className="hashtags-section" >
                {
                    hashtagsArray && hashtagsArray.length > 0 &&
                    hashtagsArray.map(hash => <Hashtag title={hash} {...hashtagsJson[hash]} />)
                }
            </div>
            <div className="details" >
                <div>{`${notesCount} ${notesCount === 1 ? "note" : "notes"}`}</div>
                <div>{`created ${dateOfCreation}`}</div>
            </div>
            <Link to={`${subjectId}/notes`} style={{ color: 'inherit', textDecoration: 'inherit'}}>
                <button><span>Explore</span><FontAwesomeIcon icon="fa-solid fa-right-long" /></button>
            </Link>
        </div>
    )
}

export default Subject;