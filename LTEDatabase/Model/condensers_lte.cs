namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.condensers_lte")]
    public partial class condensers_lte
    {
        [Key]
        [Column(TypeName = "usmallint")]
        public int idCondensersLTE { get; set; }

        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

        public float? kvar { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string type { get; set; }

        [StringLength(25)]
        public string notes { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime dateChange { get; set; }

        public virtual objects objects { get; set; }
    }
}
