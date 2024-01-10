﻿using MainTz.Application.Models.UserEntities;
using MainTz.Database.Entities;

namespace MainTz.Application.Models.CarEntities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public ICollection<int> ImagesId { get; set; }

        public ICollection<int> UsersId { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();

        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}