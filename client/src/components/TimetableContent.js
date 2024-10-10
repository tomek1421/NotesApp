import TimeTableEvent from "./TimeTableEvent";
import { useEffect, useState } from "react";
import { getTimetableEvents } from "../apiCalls/timetable";

function TimetableContent() {

    const [timetableData, setTimetableData] = useState([]);

    useEffect(() => {
        getTimetableEvents()
        .then(msg => {
            setTimetableData(msg.data);
        })
        .catch(err => {
            console.log(err);
        })
    }, [])

    return (
        <div className="timetable-content" >
            { timetableData.map(sub => <TimeTableEvent {...sub} /> ) }
        </div>
    )
}

export default TimetableContent;