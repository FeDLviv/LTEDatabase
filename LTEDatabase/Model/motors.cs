namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.motors")]
    public partial class motors
    {
        [Key]
        [StringLength(45)]
        public string type { get; set; }

        [StringLength(100)]
        public string pathDir { get; set; }
    }
}
