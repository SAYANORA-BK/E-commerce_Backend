﻿namespace E_commerce.Dto
{
    public class ProductViewDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int stock { get; set; }
        public string? Image { get; set; }
        public string Category {  get; set; }
    }
}
