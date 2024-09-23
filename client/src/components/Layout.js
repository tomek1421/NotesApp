import { Outlet, Link } from "react-router-dom";
import logo from '../assets/icons/bird.png';

function Layout() {
    return (
        <div>
            <div className="navbar">
                <ul>
                    <li>
                        <Link to="/" ><img className="logo" src={logo} alt="Logo" /></Link>
                    </li>
                    <li>
                        <Link to="subjects" >My Subjects</Link>
                    </li>
                    <li>
                        <Link to="tasks" >Task Manager</Link>

                    </li>
                    <li>
                        <Link to="plan" >Semester Plan</Link>    
                    </li>
                </ul>
            </div>
            <Outlet />
        </div>
    )
}

export default Layout;