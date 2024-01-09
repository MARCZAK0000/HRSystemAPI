﻿namespace HumanResources.Domain.Entities
{
    public class Departments
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }  

        public List<UserInfo>? Users { get; set; }

    }
}
