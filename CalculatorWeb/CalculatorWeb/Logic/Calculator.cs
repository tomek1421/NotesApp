namespace CalculatorWeb.Logic;

public class Calculator
{
    // This method evaluates a mathematical expression string and returns a result (obeying PEMDAS)
    public double EvaluateExpression(string input)
    {
        try
        {
            // Step 1: Tokenize the input into numbers and operators
            var tokens = Tokenize(input);// should return a list of tokens

            // Step 2: Convert tokens to Reverse Polish Notation (RPN) (I hate you sooo mucchhhh but you skip pemdas so i will throtle you later)
            var rpn = ConvertToRPN(tokens);

            // Step 3: Evaluate the RPN expression
            double result = EvaluateRPN(rpn);

            return result;
        }
        catch (DivideByZeroException) // catches a divide by 0 error
        {
            // If a division by zero occurs, return NaN
            //Console.WriteLine("Division by zero detected. UNDEFINED.");
            return double.NaN;
        }
        catch (Exception ex)
        {
            // Handle other errors (e.g., invalid input)
            //Console.WriteLine($"Error: {ex.Message}. Returning NaN.");
            return double.NaN;
        }
    }
    public string ConvertExpressionToRPN(string input)
    {
        var tokens = Tokenize(input);// should return a list of tokens
        var rpn = ConvertToRPN(tokens);
        return string.Join(" ", rpn);
    }

    // Tokenizer: Breaks the input into tokens (numbers and operators and hopes and dreams)
    private List<string> Tokenize(string input)
    {
        var tokens = new List<string>(); // Creates an output list of strings. Lists are more flexible than arrays, mutable size
        int index = 0;
        bool lastWasOperator = true; //if the previous token was an operator or parenthesis

        while (index < input.Length) // for every item in input
        {
            char current = input[index]; // current character

            // Handle numbers (including negative numbers)
            if (char.IsDigit(current) || current == ',' || (current == '-' && lastWasOperator))  // if current char is a number (or comma, in case of decimals) or is it a - and the previous char was an operator
            {
                string number = "";

                // If it's a quirky minus (part of a number, or comes right before a parenthesis) (based on lastWasOperator), include the minus sign
                if (current == '-' && lastWasOperator)
                {
                    number += "-";
                    index++;
                }

                // Continue extracting digits for the number
                while (index < input.Length && (char.IsDigit(input[index]) || input[index] == ',')) // // add to number abd increase index if the current character is a digit, or a comma.
                {
                    number += input[index];
                    index++;
                }

                tokens.Add(number);
                lastWasOperator = false; // Number is not an operator
            }
            else if (IsOperator(Convert.ToString(current)) || current == '(' || current == ')') // if you find a valid operator or a parenthesis
            {
                tokens.Add(current.ToString());// Detect operators and parentheses and add them to the token list 
                lastWasOperator = (current != ')'); // Set true if it's an operator or (, false if it's )
                index++;
            }
            else if (char.IsWhiteSpace(current)) // Ignore whitespaces, including tabs and linebreaks
            {
                index++;
            }
            else
            {
                throw new Exception($"Invalid character detected: {current}"); // invalid character. this is caught in the EvaluateExpression method (1st method of Calculator)
            }
        }
        return tokens;
    }
    // Helper bool to check if sth is a valid operator, because I can't be bothered to spam in if statements
    private bool IsOperator(string token)
    {
        return token == "+" || token == "-" || token == "*" || token == "/" || token == "^" || token == "!";
    }
    private int Factorial(int x)
    {
        int output = 1;
        for (int i = 1; i<=x; i++)
        {
            output *= i;
        }
        return output;
    }

    // Convert the list of tokens into Reverse Polish Notation (RPN)
    private List<string> ConvertToRPN(List<string> tokens)
    {
        var output_list = new List<string>(); // output list that stores the output in RPN order
        var operatorStack = new Stack<string>(); // Stack that stores operators temporarily

        // Operator priority (PEMDAS)
        var priority = new Dictionary<string, int> {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "^", 3 },
            { "!", 4 }
        };

        for (int i = 0; i < tokens.Count; i++) // for each token in tokens
        {
            string token = tokens[i]; // current token

            if (double.TryParse(token, out _)) // If the token is a number, add it to output_list. Normally, you would do something like: double.TryParse(token, out double result); This creates a new double that stores the result of the conversion if sucessful. But I don't care about the number, and _ is a placeholder in c# that tells the code "I don't care". I love it.
            {
                output_list.Add(token);
            }
            else if (token == "(") // If the token is a left parenthesis, push it onto the stack
            {
                operatorStack.Push(token);
            }
            else if (token == ")") // If the token is a right parenthesis (at the end of the while loop, the stack should be empty of what happens in the paranthesis)
            {
                // Keep popping from the stack to output_list until a left parenthesis is found
                while (operatorStack.Peek() != "(")
                {
                    output_list.Add(operatorStack.Pop());
                }
                operatorStack.Pop(); // Pop the left parenthesis and discard it
            }
            else if (IsOperator(token)) // The token is an operator
            {
                // Detect quirky minus: If "-" is the first token or follows an operator or left parenthesis 2 + - 2; (-3 - 8)
                if (token == "-" && (i == 0 || tokens[i - 1] == "(" || IsOperator(tokens[i - 1])))
                {
                    // Convert the quirky parenthesis minus to "-1 *"
                    output_list.Add("-1");
                    operatorStack.Push("*");
                }
                else
                {
                    // Handle normal operators based on priority
                    while (operatorStack.Count > 0 && IsOperator(operatorStack.Peek()) && priority[operatorStack.Peek()] >= priority[token])  // If the operator is less important than the one already on top of the stack, add it to output_list
                    {
                        output_list.Add(operatorStack.Pop());
                    }
                    operatorStack.Push(token);  // Push the current operator onto the stack
                }
            }
        }

        // Pop remaining operators from the stack to output_list
        while (operatorStack.Count > 0)
        {
            output_list.Add(operatorStack.Pop());
        }

        return output_list; // Return the stupid RPN expression as a list of tokens
    }

    // Evaluate the RPN expression
    private double EvaluateRPN(List<string> rpn)
    {
        var stack = new Stack<double>(); // Stack to evaluate the RPN expression

        foreach (var token in rpn) // for each characer of rpn
        {
            if (double.TryParse(token, out double number)) // If the token is a number, push it onto the stack
            {
                stack.Push(number);
            }
            else // If the token is an operator, pop two numbers from the stack and apply the operator
            {
                if (token == "!")
                {
                    int num = (int)stack.Pop(); // only intigers can be factorials
                    stack.Push(Factorial(num));
                }
                else {
                    double num2 = stack.Pop();
                    double num1 = stack.Pop();
    
                    double result;
    
                    switch (token) // mathematics is my passion
                    {
                        case "+":
                            result = num1 + num2;
                            break;
                        case "-":
                            result = num1 - num2;
                            break;
                        case "*":
                            result = num1 * num2;
                            break;
                        case "/":
                            // Check for division by zero
                            if (num2 == 0)
                            {
                                throw new DivideByZeroException(); // Detect division by zero
                            }
                            result = num1 / num2;
                            break;
                        case "^":
                            result = Math.Pow(num1, num2);
                            break;
                        default:
                            throw new Exception($"Unknown operator: {token}");
                    }
    
                    stack.Push(result); // Push the result back onto the stack
                }
                
            }
        }

        // The result should be the only remaining item on the stack
        return stack.Pop();
    }
}