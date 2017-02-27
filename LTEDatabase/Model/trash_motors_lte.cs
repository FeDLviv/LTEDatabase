namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.trash_motors_lte")]
    public partial class trash_motors_lte
    {
        [Column(TypeName = "usmallint")]
        public int id { get; set; }

        [Column(TypeName = "usmallint")]
        public int idMotorsLTE { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string region { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string type { get; set; }

        [Required]
        [StringLength(35)]
        public string address { get; set; }

        [Required]
        [StringLength(45)]
        public string mission { get; set; }

        [Required]
        [StringLength(45)]
        public string series { get; set; }

        [Required]
        [StringLength(45)]
        public string typeMotor { get; set; }

        public float? power { get; set; }

        [Column(TypeName = "usmallint")]
        public int? speed { get; set; }

        public bool? threePhase { get; set; }

        [StringLength(8)]
        public string inventory { get; set; }

        [StringLength(15)]
        public string bearing1 { get; set; }

        [StringLength(15)]
        public string bearing2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? lastRepair { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime dateTrash { get; set; }
    }
}
