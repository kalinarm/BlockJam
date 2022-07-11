// SPDX-License-Identifier: MIT
pragma solidity ^0.8.4;

import "@openzeppelin/contracts/token/ERC20/ERC20.sol";
import "@openzeppelin/contracts/token/ERC20/extensions/ERC20Burnable.sol";
import "@openzeppelin/contracts/access/Ownable.sol";

contract JUICE is ERC20, Ownable, ERC20Burnable {

    mapping (address => bool) private tokenClaimers;
    uint256 _jackpot;

    constructor() ERC20("JUICE", "JCE") 
    {
        _mint(msg.sender, 10000 * 10 ** decimals());
        _jackpot = 0;
    }

    //limit tje claim of tokens to each account just once.
    function claimTokens() public {
        require(!tokenClaimers[msg.sender], "you can only claimTokens once per account");

        //remember token claimers to prevent them to claim another time
        tokenClaimers[msg.sender] = true;
        _mint(msg.sender, 100 * 10**18);
    }
    //can check if someone has already claimed the tokens
    function hasClaimed(address account) public view returns (bool) {
        return tokenClaimers[account];
    }

    function transferToJackpot(address account, uint256 amount) public {
        uint256 fromBalance = balanceOf(account);
        require (fromBalance >= amount, "ERC20: transfer amount exceeds balance");

        //_beforeTokenTransfer(account, address(0), amount);

        //todo find a way to prevent burning tokens to send them to jackpot
         //_balances[account] = fromBalance - amount;
         burnFrom(account, amount);

         _jackpot = _jackpot + amount;

        //emit Transfer(account, address(0), amount);
        //_afterTokenTransfer(account, address(0), amount);
    }

    function transferFromJackpot(uint256 amount) public {
        require (_jackpot >= amount, "ERC20: transfer amount exceeds jackpot balance");

        //_beforeTokenTransfer(address(0), msg.sender, amount);

        _mint(msg.sender, amount);
        _jackpot = _jackpot - amount;

        //emit Transfer(address(0), msg.sender, amount);
        //_afterTokenTransfer(address(0), msg.sender, amount);
    }

        function JackpotAmount() public view returns (uint256) {
        return _jackpot;
    }
}
