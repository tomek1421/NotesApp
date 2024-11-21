import axios from "axios";

// const url = "http://localhost:5262/timetable"
const url = "http://localhost:8080/timetable"

async function getTimetableEvents() {
    return await axios.get(url);
}

async function getTimetableEventById(eventId) {
    return await axios.get(`${url}/${eventId}`);
}

async function createTimetableEvent(body) {
    return await axios.post(url, body);
}

async function updateTimetableEvent(timetableEventId, body) {
    return await axios.put(`${url}/${timetableEventId}`, body);
}

async function deleteTimetableEvent(timetableEventId) {
    return await axios.delete(`${url}/${timetableEventId}`);
}

export { getTimetableEvents, getTimetableEventById, createTimetableEvent, updateTimetableEvent, deleteTimetableEvent }