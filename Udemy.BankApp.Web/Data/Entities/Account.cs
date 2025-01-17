﻿namespace Udemy.BankApp.Web.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int AccountNumber { get; set; }
        //ilişki için
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } //bir account bir application user'a ihtiyaç duyar;
    }
}
