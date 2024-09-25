import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"

function Message({ children }) {
    return (
        <div className="message" >
            <FontAwesomeIcon icon="fa-solid fa-circle-info" size="xl" />
            <div>{children}</div>
        </div>
    )
}

export default Message;