using Microsoft.EntityFrameworkCore;

namespace TruekeCuba.Infrastructure.Models;

public class Offers
{
    
    /// <summary>///
    /// RelationShip///
    /// </summary>///
    
    
    ICollection<Transaction> Transactions { get; set; }
    User User { get; set; }
    string UserId { get; set; }
    
    
    /// <summary>///
    /// Properties///
    /// </summary>///
    Guid Id { get; set; }
    
    /// <summary>
    ///Buy or Sell
    /// </summary>
    Enum Type { get; set; } 
    /// <summary>
    ///If is Crypto,Saldo,Cup,Mlc...
    /// </summary>
    Enum AssetType { get; set; }
    /// <summary>
    /// USDT, BTC, ETH, MLC,etc
    /// </summary>
    string AssetSymbol { get; set; }
    
    /// <summary>
    /// Offered Currency
    /// </summary>
    decimal Amount { get; set; }
    /// <summary>
    /// Amount By Unity
    /// </summary>
    
    decimal PriceInCUP { get; set; }
    /// <summary>
    /// Pending,Active,Paused,Completed,etc
    /// </summary>
    Enum Status { get; set; }
    
    /// <summary>
    ///Max Limit for transaction
    /// </summary>
    decimal MinLimit { get; set; }
    /// <summary>
    ///Min Limit for transaction
    /// </summary>
    decimal MaxLimit { get; set; }
    /// <summary>
    ///Offer Create Date 
    /// </summary>
    DateTime CreatedAt { get; set; }
    /// <summary>
    ///Date Last Update
    /// </summary>
    DateTime UpdatedAt { get; set; }


    public static void SetModel(ModelBuilder modelBuilder)
    {
        
    }
    
    
}