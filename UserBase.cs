﻿namespace Fear_and_Pain
{
    internal class UserBase
    {
        public int id;
        public string login;
        public string password;
        public Roles roles;

        public UserBase(int id,string login, string password, Roles roles) {
            this.id = id;
            this.login = login;
            this.password = login;
            this.roles = roles;
        }

    }   
    
}   
