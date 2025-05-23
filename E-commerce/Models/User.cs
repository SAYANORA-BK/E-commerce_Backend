﻿using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }="user";
        public bool IsBlocked { get; set; }=false ;
        public virtual Cart? Cart { get; set; }
        public virtual List<WishList>? WishList { get; set; }
        public virtual List<Order>? Orders { get; set; }
        public ICollection<Address>? Addresses { get; set; }


    }

}
