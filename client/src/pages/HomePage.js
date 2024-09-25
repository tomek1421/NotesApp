import bird from "../assets/photos/bird.jpg";
import HomeTile from "../components/HomeTile";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import homeTiles from "../json/homeTiles.json";
import Footer from "../components/Footer";
import { Link, Element } from 'react-scroll'

function HomePage() {
    return (
        <div className="home-page" >
            <div className="home-label" >
                <div>Organize Your Life,</div>
                <div>One Note at a Time</div>
            </div>
            <img className="bird-img" src={bird} alt="bird" />
            <Link activeClass="active" to="element1" spy={true} smooth={true} duration={500}>
                <button><span>Explore</span><FontAwesomeIcon icon="fa-solid fa-arrow-down" /></button>
            </Link>
            <Element name="element1" >
                <div className="flex-center align-center" >
                    <div className="home-section" >
                        { homeTiles.map(tile => <HomeTile {...tile} />) }
                    </div>
                </div>
            </Element>
            <Footer />
        </div>
    )
}

export default HomePage;