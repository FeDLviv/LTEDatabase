namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.tc_lte")]
    public partial class tc_lte
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idTCLTE { get; set; }

        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

        [StringLength(10)]
        public string numberTC { get; set; }

        [Column(TypeName = "usmallint")]
        public int idTC { get; set; }

        [Column(TypeName = "usmallint")]
        public int? testYear { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string testQuarter { get; set; }

        public bool mustTesting { get; set; }

        public virtual objects objects { get; set; }

        public virtual tc tc { get; set; }
    }
}
