import { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import Subject from '../components/Subject';
import '../styles/subjectPage.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

function AllSubjectsPage() {

    // const hashtagsOptions = Object.keys(hashtags);

    const [subjects, setSubjects] = useState([]);

    useEffect(() => {
        // axios.get("http://localhost:5262/subjects")
        axios.get("http://localhost:8080/subjects")
        .then(msg => {
            setSubjects(msg.data)
            console.log(msg);
        }).catch(err => {
            console.log(err);
        });
    }, []);

    const [queryParams, setQueryParams] = useState({
        searchBy: "",
        searchString: "",
        sortBy: "",
        sortOrder: "",
    });

    function handleChange(event) {
        const name = event.target.name;
        const value = event.target.value;

        setQueryParams(values => ({...values, [name]: value}));
    }

    function handleSubmit(event) {
        event.preventDefault();

        // const url = `http://localhost:5262/subjects?searchBy=${queryParams.searchBy}&searchString=${queryParams.searchString}&sortBy=${queryParams.sortBy}&sortOrder=${queryParams.sortOrder}`;
        const url = `http://localhost:8080/subjects?searchBy=${queryParams.searchBy}&searchString=${queryParams.searchString}&sortBy=${queryParams.sortBy}&sortOrder=${queryParams.sortOrder}`;

        axios.get(url)
        .then(msg => {
            setSubjects(msg.data);
        })
        .catch(err => {
            console.log(err);
        });
    }

    console.log(queryParams);

    let sortByASC = "A-Z";
    let sortByDESC = "Z-A";

    switch (queryParams.sortBy) {
        case "SubjectName":
            sortByASC = "A-Z";
            sortByDESC = "Z-A";
            break;
        case "NotesCount":
            sortByASC = "Low to High";
            sortByDESC = "High to Low";
            break;
        case "DateOfCreation":
            sortByASC = "Oldest to Newest";
            sortByDESC = "Newest to Oldest";
            break;
        default:
            break;
    }

    return (
        <div className="flex-center">
            <div className="outlet-container" >
                <div className="header-section-line flex-space-between">
                    <h1>My Subjects</h1>
                    <Link to="add-subject" ><button><span>Create subject</span><FontAwesomeIcon icon="fa-solid fa-folder-plus" /></button></Link>
                </div>
                <div className="subject-filter" >
                    <form onSubmit={handleSubmit} >
                        <div>
                            <div>Search</div>
                            <input 
                                type="text"
                                name="searchString"
                                placeholder="..."
                                value={queryParams.searchString}
                                onChange={handleChange}
                            />
                        </div>
                        <div>
                            <div>Search by</div>
                            <select 
                                className="select"
                                name="searchBy"
                                value={queryParams.searchBy}
                                onChange={handleChange}
                            >
                                <option value="SubjectName" >Subject name</option>
                                <option value="Hashtags" >Tags</option>
                            </select>
                        </div>
                        <div>
                            <div>Sort By</div>
                            <select 
                                className="select"
                                name="sortBy"
                                value={queryParams.sortBy}
                                onChange={handleChange}
                            >
                                <option value="SubjectName" ><div>Subject name</div></option>
                                <option value="NotesCount" >Note count</option>
                                <option value="DateOfCreation" >Date of creation</option>
                            </select>
                        </div>
                        <div>
                            <div>Sort Order</div>
                            <select
                                className="select"
                                name="sortOrder"
                                value={queryParams.sortOrder}
                                onChange={handleChange}
                            >
                                <option value="ASC" >{sortByASC}</option>
                                <option value="DESC" >{sortByDESC}</option>
                            </select>
                        </div>
                        {/* <button type="submit" className="search-full-button" ><div><span>Apply</span><FontAwesomeIcon icon="fa-solid fa-magnifying-glass" size='s'/></div></button> */}
                        <button type="submit" className="search-full-button" >Apply</button>
                    </form>  
                </div>
                <div className="subjects-list">
                    {
                        subjects.map(subject => <Subject {...subject} />) 
                    }
                </div>
            </div>
        </div>
    )
}

export default AllSubjectsPage;