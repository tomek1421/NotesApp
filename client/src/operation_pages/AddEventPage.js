import { useState } from "react";
import { Link, useNavigate } from 'react-router-dom';
import { createTimetableEvent } from "../apiCalls/timetable";
import toast, { Toaster } from "react-hot-toast";

function AddEventPage() {

    const [formData, setFormData] = useState({
        eventName: "",
        teacher: "",
        eventRoom: "",
        type: "lecture",
        day: "",
        startTime: "",
        endTime: ""
    });

    const [error, setError] = useState({
        eventName: "",
        teacher: "",
        eventRoom: "",
        type: "",
        day: "",
        startTime: "",
        endTime: ""
    })

    const navigate = useNavigate();

    function handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;
        
        setFormData(values => ({...values, [name]: value}))
        
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

        if (formData.startTime === "") {
            setError(values => ({...values, startTime: "Field can't be empty"}));
            hasError = true;
        }

        if (formData.endTime === "") {
            setError(values => ({...values, endTime: "Field can't be empty"}));
            hasError = true;
        }
        
        if (hasError)
            return;

        createTimetableEvent(formData)
        .then(msg => {
            toast.success('Successfully created subject!', {
                position: 'bottom-center',
                style: {
                    background: '#d0f3d3'
                },
                duration: 1100
            });
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
        setTimeout(() => navigate("/timetable"), 1200);
        
    }

    const eventNameStyle = error.eventName ? {border: "1px solid red"} : null;
    const eventRoomStyle = error.eventRoom ? {border: "1px solid red"} : null;
    const teacherStyle = error.teacher ? {border: "1px solid red"} : null;
    const dayStyle = error.day ? {border: "1px solid red"} : null;
    const startTimeStyle = error.startTime ? {border: "1px solid red"} : null;
    const endTimeStyle = error.endTime ? {border: "1px solid red"} : null;

    console.log(formData)

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
                            <label>Start time</label>
                            <input 
                                type="time" 
                                id="" 
                                name="startTime" 
                                onChange={handleChange}
                                min="08:00" 
                                max="20:00"
                                style={startTimeStyle}
                            />
                            <div className="input-error" >{ error.startTime && error.startTime }</div>
                        </div>
                        <div className="operation-flex-column">
                            <label>End time</label>
                            <input 
                                type="time" 
                                id="" 
                                name="endTime"
                                onChange={handleChange}
                                min="08:00" 
                                max="20:00"
                                style={endTimeStyle}
                            />
                            <div className="input-error" >{ error.endTime && error.endTime }</div>
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

export default AddEventPage;