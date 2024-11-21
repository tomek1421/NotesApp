import axios from "axios";

const url = "http://localhost:5045"
// const url = "http://localhost:8080"


async function postExpression(body) {
    return await axios.post(`${url}/calculate`, body);
}

export { postExpression }