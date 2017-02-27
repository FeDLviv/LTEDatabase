namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.wiloCharacteristics")]
    public partial class wiloCharacteristics
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public wiloCharacteristics()
        {
            motors_lte = new HashSet<motors_lte>();
        }

        [Key]
        [Column(TypeName = "uint")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long idWiloArt { get; set; }

        [Column(TypeName = "uint")]
        public long? oldIdWiloArt { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [Column(TypeName = "usmallint")]
        public int? height { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string diametr { get; set; }

        [Column(TypeName = "umediumint")]
        public int? idRotor { get; set; }

        [Column(TypeName = "umediumint")]
        public int? idWheel { get; set; }

        [Column(TypeName = "usmallint")]
        public int? capacitor { get; set; }

        [Column(TypeName = "usmallint")]
        public int? windingResistance1 { get; set; }

        [Column(TypeName = "usmallint")]
        public int? windingResistance2 { get; set; }

        [Column(TypeName = "usmallint")]
        public int? windingResistance3 { get; set; }

        [Column(TypeName = "usmallint")]
        public int? windingResistance4 { get; set; }

        [Column(TypeName = "usmallint")]
        public int? windingResistance5 { get; set; }

        [Column(TypeName = "usmallint")]
        public int? windingResistance6 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<motors_lte> motors_lte { get; set; }

        public virtual wiloWheels wiloWheels { get; set; }

        public virtual wiloRotors wiloRotors { get; set; }
    }
}
