import axios from "axios";

const url = "http://localhost:5262/timetable"

async function getTimetableEvents() {
    return await axios.get(url);
}

async function createTimetableEvent(body) {
    return await axios.post(url, body);
}

export { getTimetableEvents, createTimetableEvent }