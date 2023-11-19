﻿using System.ComponentModel.DataAnnotations;

namespace PGCELL.Shared.Entites
{
    public class TemporalOrder
    {
        public int Id { get; set; }

        public User User { get; set; } = null!;

        public string? UserId { get; set; }

        public Product Product { get; set; } = null!;

        public int ProductId { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Comentarios")]
        public string? Remarks { get; set; }

        public decimal Value => Product == null ? 0 : Product.Price * (decimal)Quantity;
    }
}