using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ankor.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ankor.Model.DAL
{
    public class ApplicationDbContext : DbContext//IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public virtual DbSet<Account> Accounts { get; set; }
        //public virtual DbSet<Address> Addresses { get; set; }
        //public virtual DbSet<Device> Devices { get; set; }
        //public virtual DbSet<DeviceModification> DeviceModifications { get; set; }
        //public virtual DbSet<DeviceRegHistory> DeviceRegHistories { get; set; }
        //public virtual DbSet<DeviceSetting> DeviceSettings { get; set; }
        //public virtual DbSet<Event> Events { get; set; }
        //public virtual DbSet<EventType> EventTypes { get; set; }
        //public virtual DbSet<Measure> Measures { get; set; }
        //public virtual DbSet<Person> People { get; set; }
        //public virtual DbSet<SPODE> SPODES { get; set; }
        //public virtual DbSet<GetLastMeasure> GetLastMeasures { get; set; }
        //public virtual DbSet<GetMeasure> GetMeasures { get; set; }
        //public virtual DbSet<GetSpodesInfo> GetSpodesInfoes { get; set; }
        //public virtual DbSet<v_GetAccountDevices> v_GetAccountDevices { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountAction> AccountActions { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Consumer> Consumers { get; set; }
        public virtual DbSet<ClientInfo> ClientInfos { get; set; }
        
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceDriver> DeviceDrivers { get; set; }
        public virtual DbSet<DeviceDriverSpode> DeviceDriverSpodes { get; set; }
        
        public virtual DbSet<DeviceModification> DeviceModifications { get; set; }
        public virtual DbSet<DeviceRegHistory> DeviceRegHistories { get; set; }
        public virtual DbSet<DeviceSetting> DeviceSettings { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Measure> Measures { get; set; }
        public virtual DbSet<OKEI> OKEIs { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<SPODE> SPODES { get; set; }
        public virtual DbSet<UnitType> UnitTypes { get; set; }
        public virtual DbSet<GetLastMeasure> GetLastMeasures { get; set; }
        public virtual DbSet<GetMeasure> GetMeasures { get; set; }
        public virtual DbSet<GetSpodesInfo> GetSpodesInfoes { get; set; }
        public virtual DbSet<v_GetAccountDevices> v_GetAccountDevices { get; set; }

        public virtual DbSet<udf_GetMeterSpodes_Result> GetMeterSpodes_Results { get; set; }
        public virtual DbSet<usp_GetDeviceMeasure_Result> GetDeviceMeasure_Results { get; set; }
        public virtual DbSet<usp_GetLastDeviceMeasures_Result> GetLastDeviceMeasures_Results { get; set; }

        public virtual DbSet<SpodesEvent> SpodesEvents { get; set; }

        public virtual DbSet<SpodesEventType> SpodesEventTypes { get; set; }

        //[DbFunction("udf_GetMeterSpodes")]
        //public virtual IQueryable<udf_GetMeterSpodes_Result> udf_GetMeterSpodes(Nullable<int> serialNum)
        //{
        //    var serialNumParameter = serialNum.HasValue ?
        //        new ObjectParameter("SerialNum", serialNum) :
        //        new ObjectParameter("SerialNum", typeof(int));

        //    return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<udf_GetMeterSpodes_Result>("[udf_GetMeterSpodes](@SerialNum)", serialNumParameter);
        //}

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<ClientInfo>().ToTable("ClientInfo");
            
            modelBuilder.Entity<Account>()
                .HasOne<Person>(e => e.Person)
                .WithMany()
                .HasForeignKey(e => e.person_id);
            modelBuilder.Entity<Account>()
                .HasOne<Address>(e => e.Address)
                .WithMany()
                .HasForeignKey(e => e.address_id);

            modelBuilder.Entity<Device>().ToTable("Device");
            modelBuilder.Entity<DeviceModification>().ToTable("DeviceModification");
            modelBuilder.Entity<DeviceSetting>()
                .HasOne<DeviceModification>(e => e.DeviceModification)
                .WithMany()//e => e.DeviceRegHistories)
                .HasForeignKey(e => e.id_modification);

            modelBuilder.Entity<DeviceSetting>()
               .HasOne<Device>(e => e.Device)
               .WithOne(e => e.DeviceSetting)
               .HasForeignKey<Device>(e => e.id); 

            modelBuilder.Entity<DeviceRegHistory>().ToTable("DeviceRegHistory");            
            modelBuilder.Entity<DeviceRegHistory>().HasKey(e => new { e.id_account, e.id_device });
            modelBuilder.Entity<DeviceRegHistory>()
                .HasOne<Account>(e => e.Account)
                .WithMany(e => e.DeviceRegHistories)
                .HasForeignKey(e => e.id_account);
            modelBuilder.Entity<DeviceRegHistory>()
                .HasOne<Device>(e => e.Device)
                .WithMany()//e => e.DeviceRegHistories)
                .HasForeignKey(e => e.id_device);

            //modelBuilder.Entity<EventType>().HasNoKey();
            //modelBuilder.Entity<Event>().Ignore(u => u.EventType);
            //modelBuilder.Entity<Event>().HasKey(e => new { e.deviceID, e.eventCode });
            modelBuilder.Entity<EventType>().HasKey(e => e.code);
            modelBuilder.Entity<Event>()
                .HasOne<Device>(e => e.Device)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.deviceID);
            modelBuilder.Entity<Event>()
               .HasOne<SpodesEvent>(e => e.SpodesEvent)
               .WithMany(e => e.Events)
               .HasForeignKey(e => e.eventCode);
            modelBuilder.Entity<Event>()
                .HasOne<EventType>(e => e.EventType)
                .WithMany(e => e.Events)
                .HasForeignKey(e => e.nativeCode);

            modelBuilder.Entity<GetLastMeasure>().HasNoKey();
            modelBuilder.Entity<GetMeasure>().HasNoKey();

            modelBuilder.Entity<Measure>().ToTable("Measure");
            modelBuilder.Entity<SPODE>().Ignore(e => e.ParentSPODE);
            modelBuilder.Entity<Measure>().HasKey(e => new { e.deviceID, e.measureTypeID, e.measureDT });
            modelBuilder.Entity<Measure>()
                .HasOne<Device>(e => e.Device)
                .WithMany(e => e.Measures)
                .HasForeignKey(e => e.deviceID);
            modelBuilder.Entity<Measure>()
                .HasOne<SPODE>(e => e.SPODE)
                .WithMany(e => e.Measures)
                .HasForeignKey(e => e.measureTypeID);

            modelBuilder.Entity<DeviceDriverSpode>().ToTable("DeviceDriverSpode");
            modelBuilder.Entity<DeviceDriverSpode>().HasKey(e => new { e.deviceDriverID, e.spodesID });
            modelBuilder.Entity<DeviceDriverSpode>()
                .HasOne<DeviceDriver>(e => e.DeviceDriver)
                .WithMany(e => e.DeviceDriverSpodes)
                .HasForeignKey(e => e.deviceDriverID);
            modelBuilder.Entity<DeviceDriverSpode>()
                .HasOne<SPODE>(e => e.SPODE)
                .WithMany(e => e.DeviceDriverSpodes)
                .HasForeignKey(e => e.spodesID);

            modelBuilder.Entity<v_GetAccountDevices>().HasNoKey();
            modelBuilder.Entity<udf_GetMeterSpodes_Result>().HasNoKey();
            modelBuilder.Entity<usp_GetDeviceMeasure_Result>().HasNoKey();
            modelBuilder.Entity<usp_GetLastDeviceMeasures_Result>().HasNoKey();
        }
    }
}
