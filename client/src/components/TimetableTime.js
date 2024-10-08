

function Line() {
    return (
        <div className="timetable-line" ></div>
    )
}

function TimeLine({ time }) {
    return (
        <div className="timetable-timeline" >
            <div className="time" >{time}</div>
            <div className="timetable-line" ></div>
        </div>
    )
}

function TimetableTime() {
    const hours = [8,9,10,11,12,13,14,15,16,17,18,19]

    return (
        <div className="timetable-ruler" >
            {hours.map(h => { 
                return (
                    <>
                        <TimeLine time={`${h}:00`} />
                        <Line />
                        <Line />
                        <Line />
                    </>
                )
             })}
             <TimeLine time={"20:00"} />
        </div>
    )
}

export default TimetableTime;