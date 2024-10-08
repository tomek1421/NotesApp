
function TimeTableSubject({ lectureName, teacher, lectureRoom, type, day, startTime, endTime }) {

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
            <div className="title" >{lectureName}</div>
            <div className="teacher" >{teacher}</div>
            <div>
                <div>
                    <div className="lecture-room" >{lectureRoom}</div>
                    { type == "lecture" ? <div className="lecture" >Lecture</div> : <div className="practise" >Practise</div> }
                </div>
                <div className="time" >{`${startTime} - ${endTime}`}</div>
            </div>
        </div>
    )
}

export default TimeTableSubject;