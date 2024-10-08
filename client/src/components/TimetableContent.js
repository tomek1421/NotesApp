import TimeTableSubject from "./TimeTableSubject";
import timetable from "../json/timetable.json";

function TimetableContent() {

    return (
        <div className="timetable-content" >
            { timetable.map(sub => <TimeTableSubject {...sub} /> ) }
        </div>
    )
}

export default TimetableContent;