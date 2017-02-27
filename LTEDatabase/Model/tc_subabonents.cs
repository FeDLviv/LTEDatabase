namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.tc_subabonents")]
    public partial class tc_subabonents
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idTCSubabonents { get; set; }

        [Column(TypeName = "usmallint")]
        public int idSubabonentsLTE { get; set; }

        [StringLength(10)]
        public string numberTC { get; set; }

        [Column(TypeName = "usmallint")]
        public int idTC { get; set; }

        [Column(TypeName = "usmallint")]
        public int? testYear { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string testQuarter { get; set; }

        [Column(TypeName = "usmallint")]
        public int? limitYear { get; set; }

        public virtual subabonents_lte subabonents_lte { get; set; }

        public virtual tc tc { get; set; }
    }
}
