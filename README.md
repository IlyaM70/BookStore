<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">

</head>
<body>

  <h1>BookStore</h1>
  <p>
    A web-based bookstore application built with ASP.NET Core MVC. It provides role-based access control:<br>
    <strong>Administrator</strong> – manage products and categories<br>
    <strong>Buyer</strong> – browse products and add them to a shopping cart
  </p>

  <h2>🚀 Features</h2>
  <ul>
    <li><strong>Authentication &amp; Authorization</strong>
      <ul>
        <li>Login / Registration</li>
        <li>Role-based access control (Admin / Buyer)</li>
      </ul>
    </li>
    <li><strong>Administrator</strong>
      <ul>
        <li>Create, Read, Update, Delete (CRUD) operations for books and categories</li>
      </ul>
    </li>
    <li><strong>Buyer</strong>
      <ul>
        <li>Browse catalog of books</li>
        <li>View book details</li>
        <li>Add / remove books from shopping cart</li>
        <li>View cart summary</li>
      </ul>
    </li>
    <li><strong>Data Persistence</strong>
      <ul>
        <li>Entity Framework Core with SQL Server (or LocalDB)</li>
      </ul>
    </li>
    <li><strong>Responsive UI</strong>
      <ul>
        <li>Razor Views with Bootstrap</li>
      </ul>
    </li>
  </ul>

  <h2>🛠️ Prerequisites</h2>
  <ul>
    <li>.NET 6.0 SDK (or later)</li>
    <li>Visual Studio 2022 (or VS Code + C# extension)</li>
    <li>SQL Server (or LocalDB)</li>
  </ul>

  <h2>📥 Installation</h2>
  <ol>
    <li>
      <strong>Clone the repository</strong>
      <pre><code>git clone https://github.com/IlyaM70/BookStore.git  
cd BookStore</code></pre>
    </li>
    <li>
      <strong>Configure the database</strong>
      <ul>
        <li>Open <code>BookStoreWeb/appsettings.json</code></li>
        <li>Update the <code>DefaultConnection</code> string to point to your SQL Server / LocalDB instance.</li>
      </ul>
    </li>
    <li>
      <strong>Apply migrations</strong>
      <pre><code>cd BookStore.DataAccess  
dotnet ef database update</code></pre>
    </li>
    <li>
      <strong>Open the solution</strong>
      <ul>
        <li>Launch Visual Studio</li>
        <li>Open <code>BookStore.sln</code></li>
      </ul>
    </li>
    <li>
      <strong>Build &amp; Run</strong>
      <ul>
        <li>Restore NuGet packages if prompted</li>
        <li>Build the solution</li>
        <li>Press <code>F5</code> to launch</li>
      </ul>
    </li>
  </ol>

  <h2>💻 Usage</h2>
  <ul>
    <li><strong>Home Page</strong><br>Browse all available books by category or search term.</li>
    <li><strong>Book Details</strong><br>Click on any book to view title, author, description, price, and “Add to Cart” button.</li>
    <li><strong>Shopping Cart</strong><br>View all books you’ve added, adjust quantities, or remove items.</li>
    <li>
      <strong>Admin Panel</strong> <em>(requires Admin role)</em>
      <ul>
        <li>Manage Books: add new books, edit existing ones, delete obsolete entries.</li>
        <li>Manage Categories: add/edit/delete book categories.</li>
      </ul>
    </li>
  </ul>

  <h2>📂 Project Structure</h2>
  <pre><code>BookStore/
│
├─ BookStore.DataAccess/   # EF Core DbContext &amp; Migrations
├─ BookStore.Models/       # POCO domain models
├─ BookStore.Utility/      # Helper &amp; extension classes
├─ BookStoreWeb/           # ASP.NET Core MVC project
│   ├─ Controllers/
│   ├─ Views/
│   ├─ wwwroot/            # CSS, JS, images
│   └─ appsettings.json
│
└─ BookStore.sln
</code></pre>

  

</body>
</html>
