﻿namespace MainTz.Database.Entities
{
    public class CarEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public bool IsVisible { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public IEnumerable<ImageEntity> Images { get; set; }
        public IEnumerable<UserEntity> Users { get; set; }
        public int ModelId { get; set; }
        public ModelEntity Model { get; set; }
    }
}