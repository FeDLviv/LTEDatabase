namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.meters_subabonents")]
    public partial class meters_subabonents
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idMetersSubabonents { get; set; }

        [Column(TypeName = "usmallint")]
        public int idSubabonentsLTE { get; set; }

        [StringLength(20)]
        public string numberMeter { get; set; }

        [Column(TypeName = "usmallint")]
        public int? makeYear { get; set; }

        [Column(TypeName = "usmallint")]
        public int idMeter { get; set; }

        public byte? coefficient { get; set; }

        [Column(TypeName = "usmallint")]
        public int? testYear { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string testQuarter { get; set; }

        [Column(TypeName = "usmallint")]
        public int? limitYear { get; set; }

        public virtual meters meters { get; set; }

        public virtual subabonents_lte subabonents_lte { get; set; }
    }
}
