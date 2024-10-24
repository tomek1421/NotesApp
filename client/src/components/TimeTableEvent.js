import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Link } from 'react-router-dom';

function TimeTableEvent({ timetableEventId, eventName, teacher, eventRoom, type, day, startTime, endTime }) {

    const daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

    const gridColumnStart = daysOfWeek.indexOf(day) + 1;

    const [startH, startM] = parseTime(startTime);
    const [endH, endM] = parseTime(endTime);

    const gridRowStart = ((startH - 8) * 4 + 1) + (startM / 15);
    const gridRowEnd = ((endH - 8) * 4 + 1) + (endM / 15);

    function parseTime(time) {
        const [hours, minutes] = time.split(":").map(Number);
        return [hours, minutes];
    }

    return (
        <div className="timetable-tile timetable-tile-notfit" style={{ gridRow: `${gridRowStart} / ${gridRowEnd}`, gridColumn: `${gridColumnStart} / ${gridColumnStart + 1}` }} >
            <div>
                <div>
                    <div className="title" ><div>{eventName}</div></div>
                    <div className="teacher" >{teacher}</div>
                </div>
                <div className="icons">
                    <div >
                        <Link to={`${timetableEventId}/delete-event`} style={{ color: 'inherit', textDecoration: 'inherit'}}><FontAwesomeIcon icon="fa-regular fa-trash-can" size="l" /></Link> 
                    </div>
                    <div >
                        <Link to={`${timetableEventId}/edit-event`} style={{ color: 'inherit', textDecoration: 'inherit'}}><FontAwesomeIcon icon="fa-regular fa-pen-to-square" size="l" /></Link>
                    </div>
                </div>
            </div>
            <div>
                <div>
                    <div className="lecture-room" >{eventRoom}</div>
                    { type == "lecture" && <div className="tag lecture" >Lecture</div> }
                    { type == "practise" && <div className="tag practise" >Practise</div> }
                    { type == "internship" && <div className="tag internship" >Internship</div> }
                </div>
                <div className="time" >{`${startTime} - ${endTime}`}</div>
            </div>
        </div>
    )
}

export default TimeTableEvent;