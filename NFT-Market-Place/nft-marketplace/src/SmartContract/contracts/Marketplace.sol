// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import "@openzeppelin/contracts/token/ERC721/IERC721.sol";

contract Marketplace {

    // Variables
    address payable public immutable ownerAddress; // the account that receives fees
    uint public immutable tax; // the fee percentage on sales 
    uint public itemCount; 
    
    IERC721 nft;
    
    struct Item {
        uint256 tokenId;
        uint256 price;
        address payable seller;
        bool sold;
    }

    // tokenId -> Item
    mapping(uint256 => Item) public items;

    event Offered(uint256 tokenId, uint256 price, address indexed seller);
    event Bought(uint256 tokenId, uint256 price, address indexed seller, address indexed buyer);
    event CancelOffer(address indexed seller, uint256 tokenId);
    event UpdatePrice(address indexed seller, uint256 tokenId, uint256 newPrice);

    constructor(IERC721 _nft, uint256 _feePercent) {
        ownerAddress = payable(msg.sender);
        nft = _nft;
        tax = _feePercent;
    }

    // Make item to offer on the marketplace
    function makeItem(uint256 _tokenId, uint256 _price) external{
        require(_price > 0, "Price must be greater than zero");
        // transfer nft from seller account to this smart contract
        nft.transferFrom(msg.sender, address(this), _tokenId);
        // add new item to items mapping
        items[_tokenId] = Item (_tokenId, _price, payable(msg.sender), false);
        // emit Offered event
        emit Offered(_tokenId, _price, msg.sender);
    }

    // Cancel make item to offer on the marketplace
    function cancelMakeItem(uint256 _tokenId) external {
        require(items[_tokenId].price > 0, "Item does not exist on marketplace");
        // transfer nft from this smart contract to seller account
        nft.transferFrom(address(this), msg.sender, _tokenId);
        // delete nft in items mapping
        delete(items[_tokenId]);
        // emit Cancel Offer event
        emit CancelOffer(msg.sender, _tokenId);
    }

    // Update new price for item
    function updateNewPrice(uint256 _tokenId, uint256 _newPrice) external {
        require(items[_tokenId].price > 0, "Item does not exist on marketplace");
        require(_newPrice > 0 && _newPrice != items[_tokenId].price, "New price must greater than 0 and different from old price");

        items[_tokenId].price = _newPrice;
        emit UpdatePrice(msg.sender, _tokenId, _newPrice);
    }

    // Buy item
    function purchaseItem(uint256 _tokenId) external payable{
        uint256 _totalPrice = getTotalPrice(_tokenId);
        Item storage item = items[_tokenId];
        require(item.price > 0, "Item does not exist on marketplace");
        require(msg.value >= _totalPrice, "Not enough ether to cover item price and market fee");
        require(!item.sold, "Item already sold");
        // pay seller and feeAccount
        item.seller.transfer(item.price);
        ownerAddress.transfer(_totalPrice - item.price);
        // update item to sold
        item.sold = true;
        // transfer nft to buyer
        nft.transferFrom(address(this), msg.sender, item.tokenId);
        // emit Bought event
        emit Bought(item.tokenId, item.price, item.seller, msg.sender);
    }

    // Get price include tax
    function getTotalPrice(uint256 _tokenId) view public returns(uint256){
        return((items[_tokenId].price*(100 + tax))/100);
    }

    // Get information of item have token ID: _tokenId   - Next to change - 18/12/2023
    function getItems(uint256 _tokenId)external view returns (Item memory){
        return items[_tokenId];
    }
}
