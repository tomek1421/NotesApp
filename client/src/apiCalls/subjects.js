import axios from "axios";

// const url = "http://localhost:5262/subjects"
const url = "http://localhost:8080/subjects"

async function createSubject(body) {
    return await axios.post(url, body);
}

async function deleteSubject(subjectId) {
    return await axios.delete(`${url}/${subjectId}`);
}

async function getSubjectWithNotes(subjectId) {
    return await axios.get(`${url}/${subjectId}/notes`);
}

async function updateSubject(subjectId, body) {
    return await axios.put(`${url}/${subjectId}`, body);
}

export { createSubject, deleteSubject, getSubjectWithNotes, updateSubject };