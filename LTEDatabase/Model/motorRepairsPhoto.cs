namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.motorRepairsPhoto")]
    public partial class motorRepairsPhoto
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idMotorRepairsPhoto { get; set; }

        [Column(TypeName = "usmallint")]
        public int idMotorsLTE { get; set; }

        [Column(TypeName = "mediumblob")]
        [Required]
        public byte[] photo { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime dateRepairPhoto { get; set; }

        public virtual motors_lte motors_lte { get; set; }
    }
}
