using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopeeFake.Domain.Entities
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string StoreName { get; set; }
        public int UserId { get; set; }
        public string StoreAvatar { get; set; }
        public int StoreState { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
