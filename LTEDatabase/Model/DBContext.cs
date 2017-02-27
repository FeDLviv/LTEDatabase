namespace LTEDatabase.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<condensers_lte> condensers_lte { get; set; }
        public virtual DbSet<meters> meters { get; set; }
        public virtual DbSet<meters_lte> meters_lte { get; set; }
        public virtual DbSet<meters_lte_history> meters_lte_history { get; set; }
        public virtual DbSet<meters_subabonents> meters_subabonents { get; set; }
        public virtual DbSet<missions> missions { get; set; }
        public virtual DbSet<motorRepairs> motorRepairs { get; set; }
        public virtual DbSet<motorRepairsPhoto> motorRepairsPhoto { get; set; }
        public virtual DbSet<motors> motors { get; set; }
        public virtual DbSet<motors_lte> motors_lte { get; set; }
        public virtual DbSet<motors_lte_history> motors_lte_history { get; set; }
        public virtual DbSet<objects> objects { get; set; }
        public virtual DbSet<subabonents> subabonents { get; set; }
        public virtual DbSet<subabonents_documents> subabonents_documents { get; set; }
        public virtual DbSet<subabonents_lte> subabonents_lte { get; set; }
        public virtual DbSet<tc> tc { get; set; }
        public virtual DbSet<tc_lte> tc_lte { get; set; }
        public virtual DbSet<tc_lte_history> tc_lte_history { get; set; }
        public virtual DbSet<tc_subabonents> tc_subabonents { get; set; }
        public virtual DbSet<trash_motors_lte> trash_motors_lte { get; set; }
        public virtual DbSet<ts> ts { get; set; }
        public virtual DbSet<ts_lte> ts_lte { get; set; }
        public virtual DbSet<wiloCharacteristics> wiloCharacteristics { get; set; }
        public virtual DbSet<wiloRotors> wiloRotors { get; set; }
        public virtual DbSet<wiloWheels> wiloWheels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<condensers_lte>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<condensers_lte>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<meters>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<meters>()
                .HasMany(e => e.meters_lte)
                .WithRequired(e => e.meters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<meters>()
                .HasMany(e => e.meters_lte_history)
                .WithRequired(e => e.meters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<meters>()
                .HasMany(e => e.meters_subabonents)
                .WithRequired(e => e.meters)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<meters_lte>()
                .Property(e => e.numberMeter)
                .IsUnicode(false);

            modelBuilder.Entity<meters_lte>()
                .Property(e => e.testQuarter)
                .IsUnicode(false);

            modelBuilder.Entity<meters_lte_history>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<meters_lte_history>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<meters_lte_history>()
                .Property(e => e.numberMeter)
                .IsUnicode(false);

            modelBuilder.Entity<meters_subabonents>()
                .Property(e => e.numberMeter)
                .IsUnicode(false);

            modelBuilder.Entity<meters_subabonents>()
                .Property(e => e.testQuarter)
                .IsUnicode(false);

            modelBuilder.Entity<missions>()
                .Property(e => e.mission)
                .IsUnicode(false);

            modelBuilder.Entity<missions>()
                .HasMany(e => e.motors_lte)
                .WithRequired(e => e.missions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<motorRepairs>()
                .Property(e => e.typeRepair)
                .IsUnicode(false);

            modelBuilder.Entity<motorRepairs>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<motors>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<motors>()
                .Property(e => e.pathDir)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte>()
                .Property(e => e.series)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte>()
                .Property(e => e.inventory)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte>()
                .Property(e => e.bearing1)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte>()
                .Property(e => e.bearing2)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte>()
                .HasMany(e => e.motorRepairs)
                .WithRequired(e => e.motors_lte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<motors_lte>()
                .HasMany(e => e.motorRepairsPhoto)
                .WithRequired(e => e.motors_lte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<motors_lte>()
                .HasMany(e => e.motors_lte_history)
                .WithRequired(e => e.motors_lte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<motors_lte_history>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<motors_lte_history>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.category)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.organization)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.contract)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.pathDir)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.pathAct)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.pathLoss)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.pathScheme)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.pathProject)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.pathProtocol)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<objects>()
                .HasMany(e => e.condensers_lte)
                .WithRequired(e => e.objects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<objects>()
                .HasMany(e => e.meters_lte)
                .WithRequired(e => e.objects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<objects>()
                .HasMany(e => e.motors_lte)
                .WithRequired(e => e.objects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<objects>()
                .HasMany(e => e.ts_lte)
                .WithRequired(e => e.objects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<objects>()
                .HasMany(e => e.tc_lte)
                .WithRequired(e => e.objects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<objects>()
                .HasMany(e => e.subabonents_lte)
                .WithRequired(e => e.objects)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subabonents>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<subabonents>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<subabonents>()
                .HasMany(e => e.subabonents_lte)
                .WithRequired(e => e.subabonents)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subabonents_documents>()
                .Property(e => e.pathTech)
                .IsUnicode(false);

            modelBuilder.Entity<subabonents_documents>()
                .Property(e => e.pathAct)
                .IsUnicode(false);

            modelBuilder.Entity<subabonents_documents>()
                .Property(e => e.pathScheme)
                .IsUnicode(false);

            modelBuilder.Entity<subabonents_lte>()
                .HasMany(e => e.meters_subabonents)
                .WithRequired(e => e.subabonents_lte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subabonents_lte>()
                .HasMany(e => e.subabonents_documents)
                .WithRequired(e => e.subabonents_lte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<subabonents_lte>()
                .HasMany(e => e.tc_subabonents)
                .WithRequired(e => e.subabonents_lte)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tc>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<tc>()
                .Property(e => e.coefficient)
                .IsUnicode(false);

            modelBuilder.Entity<tc>()
                .HasMany(e => e.tc_lte)
                .WithRequired(e => e.tc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tc>()
                .HasMany(e => e.tc_lte_history)
                .WithRequired(e => e.tc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tc>()
                .HasMany(e => e.tc_subabonents)
                .WithRequired(e => e.tc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tc_lte>()
                .Property(e => e.numberTC)
                .IsUnicode(false);

            modelBuilder.Entity<tc_lte>()
                .Property(e => e.testQuarter)
                .IsUnicode(false);

            modelBuilder.Entity<tc_lte_history>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<tc_lte_history>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<tc_lte_history>()
                .Property(e => e.numberTC)
                .IsUnicode(false);

            modelBuilder.Entity<tc_subabonents>()
                .Property(e => e.numberTC)
                .IsUnicode(false);

            modelBuilder.Entity<tc_subabonents>()
                .Property(e => e.testQuarter)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.region)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.mission)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.series)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.typeMotor)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.inventory)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.bearing1)
                .IsUnicode(false);

            modelBuilder.Entity<trash_motors_lte>()
                .Property(e => e.bearing2)
                .IsUnicode(false);

            modelBuilder.Entity<ts>()
                .Property(e => e.number)
                .IsUnicode(false);

            modelBuilder.Entity<ts>()
                .Property(e => e.path)
                .IsUnicode(false);

            modelBuilder.Entity<ts>()
                .HasMany(e => e.ts_lte)
                .WithRequired(e => e.ts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ts_lte>()
                .Property(e => e.notes)
                .IsUnicode(false);

            modelBuilder.Entity<wiloCharacteristics>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<wiloCharacteristics>()
                .Property(e => e.diametr)
                .IsUnicode(false);

            modelBuilder.Entity<wiloRotors>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<wiloRotors>()
                .Property(e => e.typeWheel)
                .IsUnicode(false);

            modelBuilder.Entity<wiloWheels>()
                .Property(e => e.type)
                .IsUnicode(false);
        }
    }
}
