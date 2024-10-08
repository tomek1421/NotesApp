import { useEffect, useState } from "react";
import {  updateSubject, getSubjectWithNotes } from '../apiCalls/subjects';
import { Link, useNavigate, useParams } from 'react-router-dom';
import { usePreviousLocation } from "../components/PreviousLocationContext";
import toast, { Toaster } from "react-hot-toast";


function EditSubjectPage() {

    const [formData, setFormData] = useState({
        subjectName: "",
        subjectDescription: ""
    });

    const [error, setError] = useState({
        subjectName: "",
        subjectDescription: ""
    })

    const { subjectId } = useParams();

    const navigate = useNavigate();

    const prevLocation = usePreviousLocation();

    useEffect(() => {
        getSubjectWithNotes(subjectId)
        .then(msg => {
            setFormData(msg.data);
        })
        .catch(err => {
            console.log(err);
        })
    }, []);

    function handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;
        
        setFormData(values => ({...values, [name]: value}) )
        
        //validation
        if (value.length == 0)
            setError(values => ({...values, [name]: "Field can't be empty"}))
        else setError(values => ({...values, [name]: ""}))

    }

    function handleSubmit(event) {
        event.preventDefault();


        //validation
        if (formData.subjectName.length == 0) {
            setError(values => ({...values, subjectName: "Field can't be empty"}));
        } 
        
        if (formData.subjectDescription.length == 0) {
            setError(values => ({...values, subjectDescription: "Field can't be empty"}));
        } 

        if (formData.subjectName.length == 0 || formData.subjectDescription.length == 0)
            return;

        updateSubject(subjectId, formData)
        .then(msg => {
            console.log(msg.data);
            toast.success('Successfully updated subject!', {
                position: 'bottom-center',
                style: {
                    background: '#d0f3d3'
                },
                duration: 1100
            });
        })
        .catch(err => {
            console.log(err);
            toast.error('Something gone wrong!', {
                position: 'bottom-center',
                style: {
                    background: '#fcaeae'
                },
                duration: 1100
            });
        });
        setTimeout(() => navigate(prevLocation), 1200);
    }

    const nameStyle = error.subjectName ? {border: "1px solid red"} : null;
    const descStyle = error.subjectDescription ? {border: "1px solid red"} : null;

    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <form onSubmit={handleSubmit} >
                    <h2>Edit Subject</h2>
                    <div className="operation-inputs">
                        <div className="operation-flex-column">
                            <label>Subject name</label>
                            <input 
                                type="text"
                                name="subjectName"
                                placeholder="subject name"
                                value={formData.subjectName}
                                onChange={handleChange}
                                style={nameStyle}
                            />
                            <div className="input-error" >{ error.subjectName && error.subjectName }</div>
                        </div>
                        <div className="operation-flex-column" >
                            <label>Subject description</label>
                            <textarea 
                                name="subjectDescription" 
                                placeholder="subject description"
                                value={formData.subjectDescription}
                                onChange={handleChange}
                                rows="4"
                                style={descStyle}
                            >
                            </textarea>
                            <div className="input-error" >{ error.subjectDescription && error.subjectDescription }</div>
                        </div>
                    </div>
                    <div className="flex-operation-buttons" >
                        <Link to={prevLocation} ><button className="cancel-button" >Cancel</button></Link>
                        <button type="submit" className="full-button" >Edit</button>
                    </div>
                </form>
                <Toaster />
            </div>
        </div>
    )
}

export default EditSubjectPage;