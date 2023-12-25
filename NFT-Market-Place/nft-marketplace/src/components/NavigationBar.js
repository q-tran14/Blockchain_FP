import {
    Link
} from "react-router-dom";
import {Navbar, Nav, Button, Container } from 'react-bootstrap'
import icon from '../../src/images/Marketplace-icon.png';
import '../css/Navbar.css';

const Navigation = ({ web3Handler, account }) => {
    return (
        <Navbar expand="lg" className="bg-body-tertiary">
            <Container className="font-face-changa-one">
                    {/* < img src={icon} width="40" height="40" className="" alt="" /> */}
                <Navbar.Brand href="#home">NFT Marketplacep</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav" >
                    <Nav className="me-auto">
                        <Nav.Link href = "#action/3.1" as={Link} to="/HomePage">Home</Nav.Link>
                        <Nav.Link href = "#action/3.1" as={Link} to="/Marketplace">Marketplace</Nav.Link>
                        <Nav.Link href = "#action/3.1" as={Link} to="/Collection">Collection</Nav.Link>
                        <Nav.Link href = "#action/3.1" as={Link} to="/OnShelf">OnShelf</Nav.Link>
                    </Nav>
                    <Nav>
                        {account ? (
                            <Nav.Link
                                href={`https://etherscan.io/address/${account}`}
                                target="_blank"
                                rel="noopener noreferrer"
                                className="button nav-button btn-sm mx-4">
                                <Button variant="success">
                                    {account.slice(0, 5) + '...' + account.slice(38, 42)}
                                </Button>

                            </Nav.Link>
                        ) : (
                            <Button onClick={web3Handler} variant="success">Connect Wallet</Button>
                        )}
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}
export default Navigation;