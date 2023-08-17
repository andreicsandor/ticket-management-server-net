using Microsoft.EntityFrameworkCore;

namespace ticket_management.Models;

public partial class TicketManagementContext : DbContext
{
    public TicketManagementContext()
    {
    }

    public TicketManagementContext(DbContextOptions<TicketManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ticket_management;User Id=sa;Password=password-12345;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB8558513BA3");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Event__2370F727944ACB18");

            entity.ToTable("Event");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("event_description");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("event_name");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.EventImage)
                .HasMaxLength(255)
                .HasColumnName("event_image");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.VenueId).HasColumnName("venue_id");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .HasConstraintName("FK__Event__event_typ__44FF419A");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Event__venue_id__440B1D61");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__EventTyp__BB84C6F3A1FE0D97");

            entity.ToTable("EventType");

            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("event_type_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__4659622904289F2F");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.NumberOfTickets).HasColumnName("number_of_tickets");
            entity.Property(e => e.OrderedAt)
                .HasColumnType("datetime")
                .HasColumnName("ordered_at");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("total_price");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__customer__412EB0B6");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .HasConstraintName("FK__Orders__ticket_c__4222D4EF");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__TicketCa__3FC8DEA290354714");

            entity.ToTable("TicketCategory");

            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("price");
            entity.Property(e => e.TicketCategoryDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ticket_category_description");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__TicketCat__event__4316F928");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__Venue__82A8BE8D6E0F26DD");

            entity.ToTable("Venue");

            entity.Property(e => e.VenueId).HasColumnName("venue_id");
            entity.Property(e => e.VenueName).HasColumnName("venue_name");
            entity.Property(e => e.VenueCapacity).HasColumnName("venue_capacity");
            entity.Property(e => e.VenueLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("venue_location");
            entity.Property(e => e.VenueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("venue_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
