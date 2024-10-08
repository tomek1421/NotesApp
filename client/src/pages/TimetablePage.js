import TimetableTime from "../components/TimetableTime";
import TimetableContent from "../components/TimetableContent";
import "../styles/timetablePage.css";

function TimetablePage() {

    const daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

    return (
        <div className="flex-center">
             <div className="outlet-container" >
                <div className="header-section-line flex-space-between">
                    <h1>Timetable</h1>
                </div>
                <div className="timetable-header" >
                        { daysOfWeek.map(day => <div>{day}</div> ) }
                    </div>
                <div className="timetable" >
                    <TimetableTime />
                    <TimetableContent />
                </div>
            </div>
        </div>
    )
}

export default TimetablePage;