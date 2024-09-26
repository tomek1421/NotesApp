import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

function Hashtag({ title, color, icon }) {

    return (
        <div className="hashtag" style={{color: color ? color : "#595959"}} >
            <FontAwesomeIcon icon={icon ? icon : "fa-solid fa-clipboard-question"} />
            <div>{`#${title}`}</div>
        </div>
    )
}

export default Hashtag;