import { useState } from "react"; 

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { postExpression } from "../apiCalls/calculator";

import "../styles/calculator.css";

function CalculatorPage() {

    const [expression, setExpression] = useState("");

    const [expressionHistory, setExpressionHistory] = useState([])

    const [error, setError] = useState("");

    function handleChange(event) {
        const value = event.target.value;

        setExpression(value);
    }

    function handleSubmit(event) {
        event.preventDefault();

        if (expression === "") {
            setError("Invalid input! The expression can't be empty")
            return 
        }

        // console.log(expression);
        
        const body = { "expression": expression }

        postExpression(body)
        .then(msg => {
            const result = msg.data.result;
            const expressionWithResult = `${expression} = ${result}`;

            setExpressionHistory(values => [expressionWithResult, ...values]);
            setExpression("");
            setError("");
        })
        .catch(err => {
            setError("Invalid input! The expression can only include numbers, parentheses, and the following operations: +, -, *, /, ^, and !")
        })
    }

    return (
        <div className="flex-center">
            <div className="outlet-container" >
                <div className="header-section-line">
                    <h1>Calculator</h1>
                </div>
                <div className="flex-center" >
                    <div className="calculator" >
                        <form className="calculator-form" onSubmit={handleSubmit} >
                                <input 
                                    className="calculator-input"
                                    type="text"
                                    name="expression"
                                    placeholder="Enter your expression"
                                    value={expression}
                                    onChange={handleChange}
                                />
                                <button type="submit" ><FontAwesomeIcon icon="fa-solid fa-equals" /></button>
                        </form>
                                <div className="input-error" >{ error }</div>
                        <div className="result-table" >
                            <div>{ expressionHistory[0] ?? ". . ." }</div>
                            {
                                expressionHistory.length > 1 && expressionHistory.slice(1).map(ele => <div class="calculator-history" >{ele}</div>)
                            }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default CalculatorPage;