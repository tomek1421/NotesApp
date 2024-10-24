import { useEffect, useState } from "react";
import { Link, useParams, useNavigate } from 'react-router-dom';
import { createTimetableEvent } from "../apiCalls/timetable";
import toast, { Toaster } from "react-hot-toast";
import { getTimetableEventById, updateTimetableEvent } from "../apiCalls/timetable";

function EditEventPage() {

    const [formData, setFormData] = useState({
        eventName: "",
        teacher: "",
        eventRoom: "",
        type: "lecture",
        day: "",
        duration: "",
        timespan: ""
    });

    const [error, setError] = useState({
        eventName: "",
        teacher: "",
        eventRoom: "",
        type: "",
        day: "",
        duration: "",
        timespan: "",
    })

    const [timespanArray, setTimespanArray] = useState([]);

    const { timetableEventId } = useParams();

    const navigate = useNavigate();

    function calculateMinutesBetween(time1, time2) {
        // Helper function to convert time string "HH:MM" to minutes
        function timeToMinutes(time) {
            const [hours, minutes] = time.split(':').map(Number);
            return hours * 60 + minutes;
        }
    
        // Convert both times to minutes
        const minutes1 = timeToMinutes(time1);
        const minutes2 = timeToMinutes(time2);
    
        // Calculate the difference in minutes
        return minutes2 - minutes1;
    }

    useEffect(() => {
        getTimetableEventById(timetableEventId)
        .then(msg => {
            const data = msg.data;
            const duration = calculateMinutesBetween(data.startTime, data.endTime)
            console.log(msg.data);
            setFormData({
                eventName: data.eventName,
                teacher: data.teacher,
                eventRoom: data.eventRoom,
                type: data.type,
                day: data.day,
                duration: String(duration),
                timespan: `${data.startTime}-${data.endTime}`,
            });
            console.log(duration);
            
            setTimespanArray(getAvaiableTimespan(parseInt(duration)))
        }).catch(err => {
            console.log(err);
        });
    }, [])

    // console.log(formData);
    

    function generateTimeArray() {
        const times = [];
        for (let hour = 8; hour <= 20; hour++) {
          for (let minute = 0; minute < 60; minute += 15) {
            const formattedHour = hour.toString().padStart(2, '0');
            const formattedMinute = minute.toString().padStart(2, '0');
            times.push(`${formattedHour}:${formattedMinute}`);
          }
        }
        return times;
    }

    function addMinutesToTime(time, minutesToAdd) {
        let [hours, minutes] = time.split(':').map(Number);  // Split the time into hours and minutes
        let date = new Date();  // Create a new Date object
        date.setHours(hours, minutes);  // Set the hours and minutes from the provided time
        
        date.setMinutes(date.getMinutes() + minutesToAdd);  // Add the minutes
      
        // Format the result to HH:MM format, making sure to pad with leading zeros if necessary
        let newHours = String(date.getHours()).padStart(2, '0');
        let newMinutes = String(date.getMinutes()).padStart(2, '0');
        
        return `${newHours}:${newMinutes}`;
    }

    function getAvaiableTimespan(duration) {
        const timespanArray = [];
        for (let hour = 8; hour <= 20; hour++) {
            for (let minute = 0; minute < 60; minute += 15) {
                if (hour === 20 && minute !== 0) continue;
                const startTime = `${hour.toString().padStart(2, '0')}:${minute.toString().padStart(2, '0')}`;
                const endTime = addMinutesToTime(startTime, duration);
            //   console.log(startTime, endTime);
                const hs = parseInt(startTime.substring(0, 2))
                const he = parseInt(endTime.substring(0, 2))
                const ms = parseInt(startTime.substring(3, 5))
                const me = parseInt(endTime.substring(3, 5))
                if (hs + (he - hs) < 20 || ((hs + (he - hs) === 20) && me === 0)) {
                    timespanArray.push([startTime, endTime]);
                } else {
                    return timespanArray;
                }
            }
        }
    }
      
    const timeArray = generateTimeArray();
    // console.log(timeArray);

    const durationArray = []

    for (let minutes = 45; minutes <= 600; minutes += 15) {
        durationArray.push(minutes);
    }
    
    // console.log(durationArray);
    

    function handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;
        
        setFormData(values => ({...values, [name]: value}))
        if (name === "duration") {
            setTimespanArray(getAvaiableTimespan(parseInt(value)))
        }
        
        //validation
        if (value.length == 0)
            setError(values => ({...values, [name]: "Field can't be empty"}))
        else setError(values => ({...values, [name]: ""}))

    }

    function handleSubmit(event) {
        event.preventDefault();

        //validation
        let hasError = false;
    
        if (formData.eventName === "") {
            setError(values => ({...values, eventName: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.eventRoom === "") {
            setError(values => ({...values, eventRoom: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.teacher === "") {
            setError(values => ({...values, teacher: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.type === "") {
            setError(values => ({...values, type: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.day === "") {
            setError(values => ({...values, day: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.timespan === "") {
            setError(values => ({...values, timespan: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.duration === "") {
            setError(values => ({...values, duration: "Field can't be empty"}));
            hasError = true;
        }
        
        if (hasError)
            return;

        const data = {
            eventName: formData.eventName,
            teacher: formData.teacher,
            eventRoom: formData.eventRoom,
            type: formData.type,
            day: formData.day,
            startTime: formData.timespan.substring(0, 5),
            endTime: formData.timespan.substring(6,11)
        }

        updateTimetableEvent(timetableEventId, data)
        .then(msg => {
            toast.success('Successfully created subject!', {
                position: 'bottom-center',
                style: {
                    background: '#d0f3d3'
                },
                duration: 1100
            });
            setTimeout(() => navigate("/timetable"), 1200);
        })
        .catch(err => {

            toast.error('Something gone wrong!', {
                position: 'bottom-center',
                style: {
                    background: '#fcaeae'
                },
                duration: 1100
            });
        });
    }

    const eventNameStyle = error.eventName ? {border: "1px solid red"} : null;
    const eventRoomStyle = error.eventRoom ? {border: "1px solid red"} : null;
    const teacherStyle = error.teacher ? {border: "1px solid red"} : null;
    const dayStyle = error.day ? {border: "1px solid red"} : null;
    const timespanStyle = error.timespan ? {border: "1px solid red"} : null;
    const duration = error.duration ? {border: "1px solid red"} : null;

    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <form onSubmit={handleSubmit} >
                    <h2>Add event</h2>
                    <div className="operation-inputs">
                        <div className="operation-flex-column">
                            <label>Event Name</label>
                            <input 
                                type="text"
                                name="eventName"
                                placeholder="event name"
                                value={formData.eventName}
                                onChange={handleChange}
                                style={eventNameStyle}
                            />
                            <div className="input-error" >{ error.eventName && error.eventName }</div>
                        </div>
                        <div className="operation-flex-column">
                            <label>Teacher</label>
                            <input 
                                type="text"
                                name="teacher"
                                placeholder="teacher"
                                value={formData.teacher}
                                onChange={handleChange}
                                style={teacherStyle}
                            />
                            <div className="input-error" >{ error.teacher && error.teacher }</div>
                        </div>
                        <div className="operation-flex-column">
                            <label>Event Room</label>
                            <input 
                                type="text"
                                name="eventRoom"
                                placeholder="event room"
                                value={formData.eventRoom}
                                onChange={handleChange}
                                style={eventRoomStyle}
                            />
                            <div className="input-error" >{ error.eventRoom && error.eventRoom }</div>
                        </div>
                        <div className="operation-flex-column">
                            <label>Type</label>
                            <div className="radio-field">
                                <div>
                                    <input 
                                        type="radio" 
                                        id="lecture"
                                        name="type"
                                        checked={formData.type === "lecture"}
                                        value="lecture" 
                                        onChange={handleChange}
                                    />
                                    <label for="lecture">Lecture</label>
                                </div>
                                <div>
                                    <input 
                                        type="radio" 
                                        id="practise" 
                                        name="type"
                                        checked={formData.type === "practise"} 
                                        value="practise" 
                                        onChange={handleChange}
                                    />
                                    <label for="practise">Practise</label>
                                </div>
                                <div>
                                    <input 
                                        type="radio"
                                        id="internship"
                                        name="type"
                                        checked={formData.type === "internship"} 
                                        value="internship" 
                                        onChange={handleChange}
                                    />
                                    <label for="internship">Internship</label>
                                </div>
                            </div>
                        </div>
                        <div className="operation-flex-column">
                            <div>Day</div>
                            <select 
                                className="select"
                                name="day"
                                value={formData.day}
                                onChange={handleChange}
                                style={dayStyle}
                            >
                                <option value="" disabled>Select day</option>
                                <option value="Monday" >Monday</option>
                                <option value="Tuesday" >Tuesday</option>
                                <option value="Wednesday" >Wednesday</option>
                                <option value="Thursday" >Thursday</option>
                                <option value="Friday" >Friday</option>
                            </select>
                            <div className="input-error" >{ error.day && error.day }</div>
                        </div>
                        <div className="operation-flex-column">
                            <label>Duration</label>

                            <select 
                                className="select"
                                name="duration"
                                value={formData.duration}
                                onChange={handleChange}
                                style={duration}
                            >
                                <option value="" disabled >Select duration</option>
                                { durationArray.map(minutes => {
                                    const min = parseInt(minutes);
                                    const hours = Math.round(min / 60 - 0.5)
                                    const minLeft = min % 60
                                    const time = hours > 0 ? (minLeft > 0 ? `${hours}h ${minLeft}min` : `${hours}h`) : `${minLeft}min`;
                                    return <option value={minutes} >{time}</option>
                                }) }
                            </select>
                            <div className="input-error" >{ error.duration && error.duration }</div>
                        </div>
                        <div className="operation-flex-column">
                            <label>Timespan</label>
                            <select 
                                className="select"
                                name="timespan"
                                value={formData.timespan}
                                onChange={handleChange}
                                style={timespanStyle}
                            >
                                <option value="" disabled>Select timespan</option>
                                { timespanArray.map(time => <option value={`${time[0]}-${time[1]}`} >{`${time[0]} - ${time[1]}`}</option>) }
                            </select>
                            <div className="input-error" >{ error.timespan && error.timespan }</div>
                        </div>
                    </div>
                    <div className="flex-operation-buttons" >
                        <Link to="/timetable" ><button className="cancel-button" >Cancel</button></Link>
                        <button type="submit" className="full-button" >Create</button>
                    </div>
                </form>
                <Toaster />
            </div>
        </div>
    )
}

export default EditEventPage;