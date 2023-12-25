import {
    Link
} from "react-router-dom";
import { Navbar, Nav, Button, Container } from 'react-bootstrap'
//import icon from '../../src/images/Marketplace-icon.png';
import '../css/Navbar.css';

const Navigation = ({ web3Handler, account }) => {
    return (
        <Navbar className="bg-img" expand="lg" bg="secondary" variant="dark">
            <Container className="font-face-changa-one">
                <Navbar.Collapse id="responsive-navbar-nav" >
                    <Nav className="me-auto center-navbar">
                        <Nav.Link as={Link} to="/HomePage">Home</Nav.Link>
                        <Nav.Link as={Link} to="/Marketplace">Marketplace</Nav.Link>
                        <Nav.Link as={Link} to="/Collection">Collection</Nav.Link>
                        <Nav.Link as={Link} to="/OnShelf">OnShelf</Nav.Link>
                        {account ? (
                            <Nav.Link
                                href={`https://etherscan.io/address/${account}`}
                                target="_blank"
                                rel="noopener noreferrer"
                                className="button nav-button btn-sm mx-4">
                                <Button variant="outline-light">
                                    {account.slice(0, 5) + '...' + account.slice(38, 42)}
                                </Button>

                            </Nav.Link>
                        ) : (
                            <Button onClick={web3Handler} variant="outline-light">Connect Wallet</Button>
                        )}
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}
export default Navigation;