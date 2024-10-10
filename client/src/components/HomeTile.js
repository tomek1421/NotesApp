import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Link } from 'react-router-dom';

function HomeTile({ title, text, icon, backgroundColor, color, link, cssClass }) {
    return (
        <Link to={link} style={{ color: 'inherit', textDecoration: 'inherit'}} >
            <div className={`home-tile ${cssClass}`} style={{backgroundColor: backgroundColor}} >
                <div>
                    <h2>{title}</h2>
                    <FontAwesomeIcon icon={icon} size="xl" style={{color: color}} /> 
                </div>
                <p>{text}</p>
            </div>
        </Link>
    )
}

export default HomeTile;