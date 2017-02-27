namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.tc_lte_history")]
    public partial class tc_lte_history
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idTCLTEH { get; set; }

        [Column(TypeName = "enum")]
        [Required]
        [StringLength(65532)]
        public string type { get; set; }

        [Required]
        [StringLength(35)]
        public string address { get; set; }

        [Column(TypeName = "usmallint")]
        public int idTC { get; set; }

        [StringLength(10)]
        public string numberTC { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime dateTrash { get; set; }

        public virtual tc tc { get; set; }
    }
}
