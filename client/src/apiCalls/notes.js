import axios from "axios";

const url = "http://localhost:5262/subjects"

async function getNote(subjectId, noteId) {
    return await axios.get(`${url}/${subjectId}/notes/${noteId}`);
}

async function saveNote(subjectId, noteId, body) {
    return await axios.put(`${url}/${subjectId}/notes/${noteId}`, body);
}

async function editNoteTitle(subjectId, noteId, body) {
    return await axios.put(`${url}/${subjectId}/notes/${noteId}/title`, body);
}

async function createNote(subjectId, body) {
    return await axios.post(`${url}/${subjectId}/notes`, body);
}

async function deleteNote(subjectId, noteId) {
    return await axios.delete(`${url}/${subjectId}/notes/${noteId}`);
}

export { getNote, saveNote, editNoteTitle, createNote, deleteNote };