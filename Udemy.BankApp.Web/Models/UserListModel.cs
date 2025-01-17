﻿namespace Udemy.BankApp.Web.Models
{   //biz son kullanıcıya direkt entity göndermek yerine modeller ile hareket etmeliyiz.
    public class UserListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
    public class AccountListModel
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }    
        public int AccountNumber { get; set; }
        public int ApplicationUserId { get; set; }
    }
}
