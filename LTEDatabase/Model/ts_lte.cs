namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.ts_lte")]
    public partial class ts_lte
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idTSLTE { get; set; }

        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

        [Column(TypeName = "usmallint")]
        public int idTS { get; set; }

        [StringLength(55)]
        public string notes { get; set; }

        public virtual objects objects { get; set; }

        public virtual ts ts { get; set; }
    }
}
