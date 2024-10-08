import { useState } from "react";
import { createSubject } from '../apiCalls/subjects';
import { Link, useNavigate } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import toast, { Toaster } from "react-hot-toast";
import hashtags from "../json/hashtags";
import Hashtag from "../components/Hashtag";

function AddSubjectPage() {

    const [formData, setFormData] = useState({
        subjectName: "",
        subjectDescription: ""
    });

    const [hashtagsOptions, setHashtagOptions] = useState([...Object.keys(hashtags)]);

    const [selectedHashtags, setSelectedHashtags] = useState([]);

    const [error, setError] = useState({
        subjectName: "",
        subjectDescription: ""
    })

    const navigate = useNavigate();

    function handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;
        
        setFormData(values => ({...values, [name]: value}) )
        
        //validation
        if (value.length == 0)
            setError(values => ({...values, [name]: "Field can't be empty"}))
        else setError(values => ({...values, [name]: ""}))

    }

    function handleAddHashtag(event) {
        event.preventDefault();
        const value = event.target.value;
        setSelectedHashtags(values => [...values, value]);
        setHashtagOptions(values => values.filter(h => h !== value));
    }

    function handleDeleteHashtag(value) {
        setSelectedHashtags(values => values.filter(h => h !== value));
        setHashtagOptions(values => [...values, value]);
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

        const stringifyHashtags = JSON.stringify(selectedHashtags);

        createSubject({...formData, hashtags: stringifyHashtags})
        .then(msg => {
            console.log(msg.data);
            toast.success('Successfully created subject!', {
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
        setTimeout(() => navigate("/subjects"), 1200);
        
    }

    const nameStyle = error.subjectName ? {border: "1px solid red"} : null;
    const descStyle = error.subjectDescription ? {border: "1px solid red"} : null;

    // console.log(formData)
    console.log(JSON.stringify(selectedHashtags))
    console.log(selectedHashtags)

    return (
        <div className="flex-center align-center width-max">
            <div className="operation-container" >
                <form onSubmit={handleSubmit} >
                    <h2> Subject</h2>
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
                        <div className="operation-flex-column">
                            <label>Tags</label>
                            <div className="hashtag-input" >
                                <select 
                                    className="select"
                                    name="hashtags"
                                    value=""
                                    onChange={handleAddHashtag}
                                >
                                    <option value="" disabled>Select tag</option>
                                    {
                                        hashtagsOptions.map((key, index) => <option value={key} >{key}</option>)
                                    }
                                </select>
                            </div>
                            <div className="hashtag-output" >
                                   {
                                    selectedHashtags.map(hash => <div><Hashtag title={hash} {...hashtags[hash]} /><FontAwesomeIcon className="x" onClick={() => handleDeleteHashtag(hash)} icon="fa-solid fa-xmark" /></div>)
                                   }
                            </div>
                        </div>
                        <div className="operation-flex-column" >
                            <label>Subject description</label>
                            <textarea 
                                name="subjectDescription" 
                                placeholder="subject description"
                                value={formData.subjectDescription}
                                onChange={handleChange}
                                rows="3"
                                style={descStyle}
                            >
                            </textarea>
                            <div className="input-error" >{ error.subjectDescription && error.subjectDescription }</div>
                        </div>
                    </div>
                    <div className="flex-operation-buttons" >
                        <Link to="/subjects" ><button className="cancel-button" >Cancel</button></Link>
                        <button type="submit" className="full-button" >Create</button>
                    </div>
                </form>
                <Toaster />
            </div>
        </div>
    )
}

export default AddSubjectPage;