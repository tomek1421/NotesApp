import { Outlet, Link } from "react-router-dom";
import logo from '../assets/icons/bird.png';

function Layout() {
    return (
        <div>
            <div className="navbar">
                <ul>
                    <Link to="/" ><li>
                        <img className="logo" src={logo} alt="Logo" /></li>
                    </Link>
                    <Link to="subjects" style={{ color: 'inherit', textDecoration: 'none'}}>
                        <li>My Subjects</li>
                    </Link>
                    <Link to="tasks" style={{ color: 'inherit', textDecoration: 'none'}}>
                        <li>Task Manager</li>
                    </Link>
                    <Link to="plan" style={{ color: 'inherit', textDecoration: 'none'}}>
                        <li>Semester Plan</li>
                    </Link>   
                </ul>
            </div>
            <Outlet />
        </div>
    )
}

export default Layout;