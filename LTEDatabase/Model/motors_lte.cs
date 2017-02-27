namespace LTEDatabase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("soliton_lte.motors_lte")]
    public partial class motors_lte
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public motors_lte()
        {
            motorRepairs = new HashSet<motorRepairs>();
            motorRepairsPhoto = new HashSet<motorRepairsPhoto>();
            motors_lte_history = new HashSet<motors_lte_history>();
        }

        [Key]
        [Column(TypeName = "usmallint")]
        public int idMotorsLTE { get; set; }

        [Column(TypeName = "usmallint")]
        public int idObject { get; set; }

        public byte idMission { get; set; }

        [Required]
        [StringLength(45)]
        public string series { get; set; }

        [Required]
        [StringLength(45)]
        public string type { get; set; }

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
        public DateTime dateChange { get; set; }

        [Column(TypeName = "uint")]
        public long? idWiloArt { get; set; }

        public virtual missions missions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<motorRepairs> motorRepairs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<motorRepairsPhoto> motorRepairsPhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<motors_lte_history> motors_lte_history { get; set; }

        public virtual objects objects { get; set; }

        public virtual wiloCharacteristics wiloCharacteristics { get; set; }
    }
}
