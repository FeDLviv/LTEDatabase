namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.meters_lte")]
    public partial class meters_lte
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idMLTE { get; set; }

        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

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

        public bool mustTesting { get; set; }

        [Column(TypeName = "usmallint")]
        public int? page { get; set; }

        public virtual meters meters { get; set; }

        public virtual objects objects { get; set; }
    }
}
