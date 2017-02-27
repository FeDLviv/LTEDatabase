namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.motorRepairs")]
    public partial class motorRepairs
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idMotorRepairs { get; set; }

        [Column(TypeName = "usmallint")]
        public int idMotorsLTE { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string typeRepair { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateRepair { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string notes { get; set; }

        public virtual motors_lte motors_lte { get; set; }
    }
}
