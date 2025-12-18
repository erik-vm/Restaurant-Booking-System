# Restaurant Booking System - Detailed Build Plan

## Overview
This plan builds a Restaurant Booking System using ASP.NET Core Razor Pages with Entity Framework Core, covering all exam topics in a structured, phase-by-phase approach.

---

## Phase 0: Project Setup & Configuration

### Step 0.1: Create Solution Structure
1. Create ASP.NET Core Web App (Razor Pages) solution
2. Enable nullable reference types in project file
3. Configure warnings as errors in `.csproj`
4. Verify git repository initialization

### Step 0.2: Install Required NuGet Packages
Install the following packages:
- `Microsoft.EntityFrameworkCore.Sqlite`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.VisualStudio.Web.CodeGeneration.Design`

### Step 0.3: Install EF Core Tools
```bash
dotnet tool install --global dotnet-ef
```

### Exam Topics Covered:
- Project structure basics
- Configuration management

---

## Phase 1: Domain Model Design (OOP Part 1)

### Step 1.1: Create Base Entity Class
**Location**: `Domain/BaseEntity.cs`

Create abstract base class with:
- `Id` property (primary key)
- `CreatedAt`, `UpdatedAt` timestamps
- Virtual methods for audit trail

**Exam Topics**:
- Class fundamentals
- Access modifiers (protected, public)
- Inheritance
- Virtual/abstract keywords

### Step 1.2: Create Restaurant Entity
**Location**: `Domain/Restaurant.cs`

Properties:
- `Name` (string, required, max 128 chars)
- `Location` (string, required, max 256 chars)
- `Description` (string?, optional)
- `Capacity` (int, required)
- `PhoneNumber` (string?, optional)
- `Email` (string?, optional)
- Navigation property: `ICollection<Table> Tables`
- Navigation property: `ICollection<Booking> Bookings`

**Exam Topics**:
- Properties with data annotations
- Nullable reference types
- Collection navigation properties
- MaxLength constraints

### Step 1.3: Create Table Entity
**Location**: `Domain/Table.cs`

Properties:
- `TableNumber` (string, required)
- `SeatingCapacity` (int, required)
- `IsActive` (bool, default true)
- Foreign key: `RestaurantId`
- Navigation property: `Restaurant`
- Navigation property: `ICollection<Booking> Bookings`

**Exam Topics**:
- Foreign key relationships
- Navigation properties (many-to-one)

### Step 1.4: Create Customer Entity
**Location**: `Domain/Customer.cs`

Properties:
- `FirstName` (string, required, max 64 chars)
- `LastName` (string, required, max 64 chars)
- `Email` (string, required, max 128 chars, unique)
- `PhoneNumber` (string, required, max 20 chars)
- Navigation property: `ICollection<Booking> Bookings`

Add method:
- `string GetFullName()` - returns formatted full name

**Exam Topics**:
- Methods in classes
- String manipulation

### Step 1.5: Create Booking Entity
**Location**: `Domain/Booking.cs`

Properties:
- `BookingDate` (DateTime, required)
- `BookingTime` (TimeSpan, required)
- `NumberOfGuests` (int, required)
- `Status` (BookingStatus enum)
- `SpecialRequests` (string?, optional)
- Foreign keys: `CustomerId`, `RestaurantId`, `TableId?`
- Navigation properties: `Customer`, `Restaurant`, `Table?`

Add methods:
- `bool IsUpcoming()` - check if booking is in future
- `bool CanBeCancelled()` - business logic for cancellation

**Exam Topics**:
- DateTime handling
- Nullable types (TableId?)
- Method implementation
- Business logic

### Step 1.6: Create BookingStatus Enum
**Location**: `Domain/BookingStatus.cs`

Values:
- Pending
- Confirmed
- Cancelled
- Completed
- NoShow

**Exam Topics**:
- Enums

### Step 1.7: Create Interfaces
**Location**: `Domain/Interfaces/`

Create interfaces:
- `IEntity` - contract for Id property
- `IAuditable` - contract for audit fields
- `IBookingService` - contract for booking operations
- `IAvailabilityChecker` - contract for availability logic

**Exam Topics**:
- Interface design
- Contracts and abstraction

---

## Phase 2: Data Access Layer (EF Core Introduction)

### Step 2.1: Create DbContext
**Location**: `Data/ApplicationDbContext.cs`

- Inherit from `DbContext`
- Define `DbSet<T>` properties for all entities
- Constructor with `DbContextOptions<ApplicationDbContext>`
- Override `OnModelCreating` for configuration

**Exam Topics**:
- DbContext configuration
- Constructor injection preparation

### Step 2.2: Configure Entity Relationships (Fluent API)
**Location**: `Data/ApplicationDbContext.cs` - in `OnModelCreating`

Configure:
- Restaurant → Tables (one-to-many)
- Restaurant → Bookings (one-to-many)
- Customer → Bookings (one-to-many)
- Table → Bookings (one-to-many)
- Unique index on Customer.Email
- Required fields
- Max length constraints
- Delete behaviors (cascade vs restrict)

**Exam Topics**:
- Fluent API configuration
- Relationship configuration
- Indexes
- Constraints

### Step 2.3: Configure Connection String
**Location**: `appsettings.json`

Add ConnectionStrings section with SQLite database path

**Exam Topics**:
- Configuration management

### Step 2.4: Register DbContext in Program.cs
**Location**: `Program.cs`

Register `ApplicationDbContext` with dependency injection using SQLite

**Exam Topics**:
- Dependency injection basics
- Service registration

### Step 2.5: Create Initial Migration
```bash
dotnet ef migrations add InitialCreate
```

Review generated migration code

**Exam Topics**:
- EF Core migrations
- Migration commands

### Step 2.6: Create Database
```bash
dotnet ef database update
```

**Exam Topics**:
- Database creation from migrations

---

## Phase 3: Seed Data & Testing (Collections & JSON)

### Step 3.1: Create Seed Data Class
**Location**: `Data/SeedData.cs`

Static method to:
- Check if data exists
- Create sample restaurants
- Create sample tables for each restaurant
- Create sample customers
- Create sample bookings

Use:
- List<T> for collections
- Dictionary<TKey, TValue> for quick lookups
- LINQ for data queries

**Exam Topics**:
- List<T>, Dictionary<TKey, TValue>
- Static methods
- Data initialization

### Step 3.2: Call Seed Data on Application Start
**Location**: `Program.cs`

After `app.Build()`, create scope and call seed data

**Exam Topics**:
- Application lifecycle
- Dependency injection scopes

### Step 3.3: Create JSON Export/Import Service (Optional)
**Location**: `Services/JsonDataService.cs`

Implement:
- `ExportToJson<T>(List<T> data, string filePath)`
- `ImportFromJson<T>(string filePath)`

Use `System.Text.Json` with options:
- WriteIndented = true
- AllowTrailingCommas = true

**Exam Topics**:
- JSON serialization
- Generics
- File operations
- Path.Combine, Environment.GetFolderPath

### Step 3.4: Test Database Operations
Create simple test to verify:
- Adding data
- Querying data
- Updating data
- Deleting data

**Exam Topics**:
- CRUD operations
- SaveChanges()

---

## Phase 4: Business Logic Layer (OOP Part 2 & Advanced)

### Step 4.1: Create DTO Records
**Location**: `DTOs/`

Create record types:
- `RestaurantSummaryDto(string Name, string Location, int Capacity)`
- `BookingRequestDto` - for creating bookings
- `AvailabilityDto` - for availability checks

**Exam Topics**:
- Records (C# 9.0)
- Immutable data models
- DTOs pattern

### Step 4.2: Create Availability Checker Service
**Location**: `Services/AvailabilityChecker.cs`

Implement:
- Check table availability for date/time
- Calculate occupied capacity
- Return available tables
- Use HashSet<int> for tracking occupied table IDs

Methods using delegates:
- `List<Table> FindAvailableTables(DateTime date, TimeSpan time, Predicate<Table> filter)`
- Use `Func<Table, bool>` for filtering logic

**Exam Topics**:
- Service layer pattern
- HashSet<T> for fast lookups
- Delegates (Func, Action, Predicate)
- LINQ queries with filters

### Step 4.3: Create Booking Service
**Location**: `Services/BookingService.cs`

Implement methods:
- `CreateBooking(BookingRequestDto request)`
- `CancelBooking(int bookingId)`
- `GetUpcomingBookings(int customerId)`
- `ValidateBooking(Booking booking)` - returns tuple `(bool IsValid, string? ErrorMessage)`

Use:
- Nullable types for error messages
- Null-coalescing operators (`??`, `??=`)
- Tuples for multiple return values

**Exam Topics**:
- Service pattern
- Tuples
- Nullable types
- Null operators (?, ??, ??=)
- Business validation logic

### Step 4.4: Create Restaurant Service
**Location**: `Services/RestaurantService.cs`

Implement:
- `SearchRestaurants(string? name, int? minCapacity, string? location)`
- `GetRestaurantWithTables(int restaurantId)` - use eager loading
- Use `Func<string, string>` delegate for text transformation

**Exam Topics**:
- LINQ queries
- Eager loading (.Include())
- Optional parameters
- Delegates

### Step 4.5: Register Services in DI Container
**Location**: `Program.cs`

Register all services with appropriate lifetime (Scoped/Transient)

**Exam Topics**:
- Dependency injection
- Service lifetimes

---

## Phase 5: Razor Pages Scaffolding & Generation

### Step 5.1: Scaffold CRUD Pages for Restaurant
```bash
dotnet aspnet-codegenerator razorpage -m Restaurant -dc ApplicationDbContext -udl -outDir Pages/Restaurants --referenceScriptLibraries
```

**Exam Topics**:
- Scaffolding tool
- CRUD generation

### Step 5.2: Scaffold CRUD Pages for Customer
```bash
dotnet aspnet-codegenerator razorpage -m Customer -dc ApplicationDbContext -udl -outDir Pages/Customers --referenceScriptLibraries
```

### Step 5.3: Scaffold CRUD Pages for Booking
```bash
dotnet aspnet-codegenerator razorpage -m Booking -dc ApplicationDbContext -udl -outDir Pages/Bookings --referenceScriptLibraries
```

### Step 5.4: Review Generated Code
Inspect scaffolded pages:
- `Index.cshtml` / `Index.cshtml.cs`
- `Create.cshtml` / `Create.cshtml.cs`
- `Edit.cshtml` / `Edit.cshtml.cs`
- `Delete.cshtml` / `Delete.cshtml.cs`
- `Details.cshtml` / `Details.cshtml.cs`

Understand:
- `async Task<IActionResult> OnGetAsync()`
- `async Task<IActionResult> OnPostAsync()`
- Model binding with `[BindProperty]`
- ModelState validation

**Exam Topics**:
- Async/await patterns
- Model binding
- Razor page structure
- HTTP GET/POST handlers

---

## Phase 6: Customizing Razor Pages (Views & Tag Helpers)

### Step 6.1: Enhance Restaurant Index Page
**Location**: `Pages/Restaurants/Index.cshtml`

Add:
- Search form with filters (name, location, capacity)
- Display results in table
- Links to details, edit, delete
- Tag helpers: `asp-page`, `asp-route-id`

**C# Code Behind**:
- Add search properties with `[BindProperty(SupportsGet = true)]`
- Filter query based on search params

**Exam Topics**:
- Tag helpers (asp-page, asp-route, asp-for)
- Model binding with GET requests
- LINQ filtering

### Step 6.2: Create Custom Restaurant Details Page
**Location**: `Pages/Restaurants/Details.cshtml`

Display:
- Restaurant information
- List of tables with availability status
- Recent bookings
- Use partial view for table list

**Exam Topics**:
- Eager loading (.Include(), .ThenInclude())
- Partial views
- Data display

### Step 6.3: Create Booking Creation Page
**Location**: `Pages/Bookings/Create.cshtml`

Features:
- Select restaurant (dropdown with `asp-items`)
- Select date and time
- Enter number of guests
- Show available tables dynamically
- Customer information form
- Use ViewData for passing data

Steps in PageModel:
- `OnGetAsync()` - load restaurants for dropdown
- `OnPostAsync()` - validate and create booking
- Check availability before confirming
- Redirect to confirmation page

**Exam Topics**:
- Select tag helper (asp-items)
- SelectList
- ViewData
- Form validation
- Async operations
- Business logic integration

### Step 6.4: Create Layout and Navigation
**Location**: `Pages/Shared/_Layout.cshtml`

Add:
- Navigation menu with links to all sections
- Bootstrap styling
- Sections for scripts and styles
- Use `RenderBody()` and `RenderSection()`

**Exam Topics**:
- Layout pages
- RenderBody(), RenderSection()
- Shared views

### Step 6.5: Create Partial View for Booking List
**Location**: `Pages/Shared/_BookingListPartial.cshtml`

Display bookings in formatted list with:
- Date/time
- Restaurant name
- Guest count
- Status badge
- Actions (cancel, view details)

Use in multiple pages

**Exam Topics**:
- Partial views
- Code reusability
- HTML helpers

### Step 6.6: Add Client-Side Validation
**Location**: Various `.cshtml` files

Add:
- Validation scripts
- `asp-validation-for` tag helpers
- `asp-validation-summary` tag helper

**Exam Topics**:
- Validation tag helpers
- ModelState validation

---

## Phase 7: Advanced Features & Search

### Step 7.1: Create Availability Search Page
**Location**: `Pages/Search/Availability.cshtml`

Features:
- Date picker
- Time picker
- Guest count input
- Location filter
- Submit search
- Display available restaurants with available tables

**PageModel**:
- Accept search parameters
- Query using AvailabilityChecker service
- Use LINQ with Include/ThenInclude
- Return grouped results

**Exam Topics**:
- Complex LINQ queries
- Eager loading multiple levels
- Service integration
- Form handling

### Step 7.2: Create Customer Bookings Page
**Location**: `Pages/MyBookings/Index.cshtml`

Features:
- Display customer's bookings
- Filter by status (upcoming, past, cancelled)
- Cancel booking functionality
- Use Queue<T> or Stack<T> if implementing undo functionality

**Exam Topics**:
- LINQ filtering and ordering
- Async database operations
- Status management
- Queue<T> / Stack<T> (if implementing undo)

### Step 7.3: Add Booking Confirmation Page
**Location**: `Pages/Bookings/Confirmation.cshtml`

Display:
- Booking details
- QR code or booking reference
- Print/email options
- Use ViewData for passing success messages

**Exam Topics**:
- ViewData usage
- Post-Redirect-Get pattern
- User experience

---

## Phase 8: Advanced EF Core Features

### Step 8.1: Enable EF Core Logging
**Location**: `Program.cs` or `ApplicationDbContext.cs`

Add logging to console:
```csharp
optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information)
```

**Exam Topics**:
- EF Core logging
- Debugging SQL queries

### Step 8.2: Add Indexes for Performance
**Location**: `Data/ApplicationDbContext.cs`

Add indexes using Fluent API:
- Customer.Email (unique)
- Booking.BookingDate
- Restaurant.Location
- Composite index on Booking(RestaurantId, BookingDate)

**Exam Topics**:
- Index configuration
- Performance optimization
- Fluent API

### Step 8.3: Implement Explicit Loading Example
**Location**: `Services/RestaurantService.cs`

Add method demonstrating explicit loading:
```csharp
_context.Entry(restaurant).Collection(r => r.Tables).Load();
```

**Exam Topics**:
- Explicit loading
- Different loading strategies

### Step 8.4: Add Complex Query with Multiple Includes
**Location**: `Services/BookingService.cs`

Create method to get booking with all related data:
```csharp
.Include(b => b.Restaurant)
    .ThenInclude(r => r.Tables)
.Include(b => b.Customer)
.Include(b => b.Table)
```

**Exam Topics**:
- Eager loading
- ThenInclude
- Complex queries

---

## Phase 9: Advanced OOP & Patterns

### Step 9.1: Create Value Objects (Structs)
**Location**: `Domain/ValueObjects/`

Create structs:
- `TimeSlot` (StartTime, EndTime, Duration)
- `ContactInfo` (Email, Phone)

Use in entities where appropriate

**Exam Topics**:
- Structs vs classes
- Value types
- Immutability

### Step 9.2: Implement Extension Methods
**Location**: `Extensions/`

Create extension methods:
- `StringExtensions.ToTitleCase()`
- `DateTimeExtensions.IsWeekend()`
- `BookingExtensions.IsWithinCancellationWindow()`

**Exam Topics**:
- Extension methods
- Code reusability

### Step 9.3: Add Generic Repository Pattern (Optional)
**Location**: `Data/Repositories/`

Create:
- `IRepository<T>` interface
- `Repository<T>` generic implementation
- Specific repositories inheriting from base

**Exam Topics**:
- Generics
- Repository pattern
- Interface implementation
- Inheritance

### Step 9.4: Implement Strategy Pattern for Pricing (Optional)
**Location**: `Services/Pricing/`

Create:
- `IPricingStrategy` interface
- `WeekdayPricingStrategy`
- `WeekendPricingStrategy`
- `HolidayPricingStrategy`

**Exam Topics**:
- Strategy pattern
- Polymorphism
- Interface implementation

---

## Phase 10: Testing & Quality Assurance

### Step 10.1: Manual Testing Checklist
Test all functionality:
- Create, read, update, delete restaurants
- Create, read, update, delete customers
- Create bookings with availability checking
- Search restaurants by various criteria
- View customer bookings
- Cancel bookings
- Validate form inputs
- Test error scenarios

### Step 10.2: Database Integrity Testing
Verify:
- Foreign key constraints work
- Cascade deletes work correctly
- Unique constraints are enforced
- Required fields are enforced

**Exam Topics**:
- Database constraints
- Data integrity

### Step 10.3: Test EF Core Migrations
Test migration commands:
```bash
dotnet ef migrations add TestMigration
dotnet ef database update
dotnet ef database drop --force
dotnet ef database update
```

**Exam Topics**:
- Migration workflow
- Database management

---

## Phase 11: Advanced Features (Optional Enhancements)

### Step 11.1: Implement JSON Export/Import
**Location**: `Pages/Admin/DataManagement.cshtml`

Create admin page with:
- Export all data to JSON files
- Import data from JSON files
- Use `System.Text.Json`
- Save to user's documents folder

**Exam Topics**:
- JSON serialization/deserialization
- File I/O operations
- Path manipulation
- Environment.GetFolderPath

### Step 11.2: Add Statistics Dashboard
**Location**: `Pages/Dashboard/Index.cshtml`

Display:
- Total bookings (use Count)
- Most popular restaurant (use GroupBy)
- Booking trends (use GroupBy with date)
- Capacity utilization
- Use Dictionary<string, int> for aggregated data

**Exam Topics**:
- LINQ aggregation
- GroupBy, Count, Sum
- Dictionary for statistics
- Data visualization

### Step 11.3: Implement Notification System
Create service using delegates:
- Define event handlers using `Action<Booking>`
- Send email notification on booking creation
- Send SMS notification on booking cancellation
- Use event pattern

**Exam Topics**:
- Delegates
- Events
- Action<T>
- Observer pattern

---

## Phase 12: Polish & Documentation

### Step 12.1: Enhance UI/UX
- Add Bootstrap styling consistently
- Add icons for actions
- Improve form layouts
- Add loading indicators
- Add confirmation dialogs for delete operations

### Step 12.2: Error Handling
- Add try-catch blocks in critical sections
- Create custom error page
- Log errors appropriately
- Show user-friendly error messages

### Step 12.3: Add XML Documentation Comments
Document:
- Public classes
- Public methods
- Complex logic
- Business rules

### Step 12.4: Clean Up Code
- Remove unused using statements
- Ensure consistent naming conventions
- Remove commented-out code
- Verify SOLID principles are followed
- Verify DRY principle is followed
- Verify KISS principle is followed

---

## Testing Phases (To be done after each phase)

After each major phase, perform:

1. **Build verification**
   ```bash
   dotnet build
   ```

2. **Run application**
   ```bash
   dotnet run
   ```

3. **Test new features manually**

4. **Verify existing features still work**

5. **Check for warnings** (remember: warnings are errors!)

6. **Kill port 8080 if used**
   ```bash
   netstat -ano | findstr :8080
   taskkill /PID <PID> /F
   ```

---

## Exam Topics Coverage Summary

### ✅ OOP Part 1
- Classes, objects, properties, methods, constructors
- Access modifiers
- Inheritance (BaseEntity)
- Virtual/override/abstract
- Interfaces (IEntity, IBookingService, etc.)
- Generics (Repository<T>, List<T>, etc.)

### ✅ OOP Part 2
- Nullable types (Table?, string?)
- Null handling operators (?., ??, ??=)
- Structs (TimeSlot, ContactInfo)
- Records (DTOs)
- Delegates (Func, Action, Predicate)
- Tuples (validation results)
- Boxing (minimal, but understood)

### ✅ Arrays & Collections
- List<T> (for entity collections)
- Dictionary<TKey, TValue> (for lookups, statistics)
- HashSet<T> (for tracking occupied tables)
- Stack<T> / Queue<T> (optional undo feature)
- Array operations and properties

### ✅ JSON Serialization
- System.Text.Json
- Serialization/Deserialization
- JsonSerializerOptions
- File operations (Directory, File, Path, Environment.GetFolderPath)

### ✅ EF Core Introduction
- DbContext configuration
- DbSet<T> properties
- Connection strings
- Migrations (add, update, drop)
- CRUD operations
- SaveChanges()

### ✅ EF Core Advanced
- Logging
- Eager loading (.Include(), .ThenInclude())
- Explicit loading
- Lazy loading concepts
- Primary keys (conventions, annotations, fluent API)
- Property constraints (Required, MaxLength)
- Indexes (unique, composite)
- Relationships (one-to-many, many-to-one)

### ✅ Razor Pages
- HTTP basics (GET, POST, status codes)
- Razor syntax (@, @page, @model)
- Project structure
- Model binding ([BindProperty])
- Form handling
- OnGet, OnPost handlers
- ModelState validation
- RedirectToPage

### ✅ Razor Pages Scaffolding
- Scaffolding commands
- Code generation
- Async/await patterns
- Task<T> and Task
- Database operations with async

### ✅ Razor Pages Views
- Razor directives
- ViewData
- Layout pages (RenderBody, RenderSection)
- Tag helpers (asp-for, asp-page, asp-route, asp-items)
- HTML helpers
- Partial views

### ✅ SOLID Principles
- Enforced throughout design
- Single Responsibility (separate services)
- Open/Closed (interfaces, extension methods)
- Liskov Substitution (proper inheritance)
- Interface Segregation (specific interfaces)
- Dependency Inversion (DI container)

---

## Key Reminders Throughout Development

1. **SOLID principles must be followed** - refactor if violated
2. **DRY principle** - avoid code duplication
3. **KISS principle** - keep it simple
4. **Nullable references enabled** - handle nulls properly
5. **Warnings as errors** - fix all warnings immediately
6. **Test after each modification** - don't accumulate issues
7. **Look for existing code** - reuse before creating new
8. **No unnecessary comments** - unless instructed
9. **Step by step, phase by phase** - verify before moving forward
10. **Kill port 8080 after testing** - clean up resources

---

## Final Deliverable Checklist

Before considering the project complete:

- [ ] All CRUD operations working
- [ ] Search functionality implemented
- [ ] Availability checking working correctly
- [ ] Customer can view their bookings
- [ ] Data validation working
- [ ] Nullable references handled properly
- [ ] No warnings (all treated as errors)
- [ ] UI is clean and user-friendly
- [ ] All exam topics demonstrably covered
- [ ] Code follows SOLID, DRY, KISS principles
- [ ] Database migrations clean and working
- [ ] Application runs without errors
- [ ] Documentation adequate

---

## Estimated Time per Phase

- Phase 0: 30 minutes
- Phase 1: 2 hours
- Phase 2: 1.5 hours
- Phase 3: 1 hour
- Phase 4: 2 hours
- Phase 5: 30 minutes
- Phase 6: 3 hours
- Phase 7: 2 hours
- Phase 8: 1 hour
- Phase 9: 2 hours (optional features)
- Phase 10: 1 hour
- Phase 11: 2 hours (optional)
- Phase 12: 1 hour

**Total: ~18-20 hours** for complete implementation

---

This plan ensures comprehensive coverage of all exam topics while building a production-quality application that demonstrates best practices in C# and ASP.NET Core development.
