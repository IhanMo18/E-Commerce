using Microsoft.EntityFrameworkCore;

namespace TruekeCuba.Infrastructure.Models;

public class User
{
   
    /// <summary>///
    ///RelationShips///
    /// </summary>////

    ICollection<Offers> Offers { get; set; }
    
    ICollection<Transaction> Transactions { get; set; }
    
    ICollection<Review> Reviews { get; set; }
    
    
   
    /// <summary>///
    /// Properties///
    /// </summary>///
    
    Guid Id { get; set; }
    
    
    /// <summary>
    ///The Username
    /// </summary>
    string Username { get; set; }
    /// <summary>
    ///The email(unique for user)
    /// </summary>
    string Email { get; set; }
    /// <summary>
    ///The user Password
    /// </summary>
    string PasswordHash{ get; set; }
    /// <summary>
    ///User Phone
    /// </summary>
    string PhoneNumber { get; set; }
    /// <summary>
    ///Dni image
    /// </summary>
    private string IdentificationUrl { get; set; }
    /// <summary>
    ///Created Profile Date
    /// </summary>
    DateTime CreateAt { get; set; }
    /// <summary>
    ///Update Profile Date
    /// </summary>
    DateTime UpdateAt { get; set; }
    /// <summary>
    ///Is user account active?
    /// </summary>
    bool IsActive { get; set; }
    /// <summary>
    ///Is user account deleted?
    /// </summary>
    bool IsDeleted { get; set; }
    /// <summary>
    ///Is user locked?
    /// </summary>
    bool IsLocked { get; set; }
    /// <summary>
    ///Is user verified?
    /// </summary>
    bool IsVerified { get; set; }
    /// <summary>
    ///Is user phone verified?
    /// </summary>
    bool IsPhoneVerified { get; set; }



    public static void SetModel(ModelBuilder builder)
    {
        
        
        
    }
    
}