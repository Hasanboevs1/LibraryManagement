---

# Library Management System API

## Overview
This is a RESTful API built for managing a library system. The system allows for managing books, authors, users, loans, and categories. The API provides endpoints to borrow and return books, search for books, and manage library data.

The API is designed to provide a backend solution for a library, where users can borrow books, view available books by category, and manage their loan status.

---

## Features

- **Books**: Manage book records, including details like title, author, ISBN, and category.
- **Authors**: Manage authors of the books.
- **Users**: Track users and their associated loans.
- **Loans**: Borrow and return books.
- **Categories**: Categorize books and filter them by category.

---

## Getting Started

### Prerequisites

Before running the project, you need to have the following software installed:

- [.NET 8 or higher](https://dotnet.microsoft.com/download/dotnet)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Hasanboevs1/LibraryManagement.git
   ```

2. Navigate into the project directory:

   ```bash
   cd library-management-api
   ```

3. Install dependencies:

   ```bash
   dotnet restore
   ```

4. Set up the database (using SQLite):

5. Run the migrations to set up the database schema:

   ```bash
   dotnet ef database update
   ```

6. Start the API:

   ```bash
   dotnet run
   ```

   The API will be available at `https://localhost:7226`.

---

## API Endpoints

### 1. **Books**

#### Get all books

```http
GET /api/books
```

Returns a list of all books in the library.

#### Get book by ID

```http
GET /api/books/{id}
```

Returns a single book by its ID.

#### Add a new book

```http
POST /api/books
```

Request Body:

```json
{
  "title": "Book Title",
  "isbn": "123456789",
  "authorId": 1,
  "category": "Fiction",
  "bookCount": 10
}
```

Adds a new book to the library.

#### Update a book

```http
PUT /api/books/{id}
```

Request Body:

```json
{
  "title": "Updated Title",
  "isbn": "987654321",
  "authorId": 2,
  "category": "Science",
  "bookCount": 5
}
```

Updates an existing book.

#### Delete a book

```http
DELETE /api/books/{id}
```

Deletes a book by its ID.

---

### 2. **Authors**

#### Get all authors

```http
GET /api/authors
```

Returns a list of all authors.

#### Get author by ID

```http
GET /api/authors/{id}
```

Returns a single author by their ID.

---

### 3. **Users**

#### Get all users

```http
GET /api/users
```

Returns a list of all users in the system.

#### Register a new user

```http
POST /api/users
```

Request Body:

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "password123"
}
```

Registers a new user.

#### Get user by ID

```http
GET /api/users/{id}
```

Returns a single user by their ID.

---

### 4. **Loans**

#### Borrow a book

```http
POST /api/loans/borrow
```

Request Body:

```json
{
  "bookId": 1,
  "userId": 1,
  "loanDate": "2024-01-01"
}
```

Creates a new loan for a user and a book. The book is marked as unavailable.

#### Return a book

```http
POST /api/loans/return/{code:Guid}
```

Request Body (optional):

```json
{
  "returnDate": "2024-01-08"
}
```

Marks a book as returned, making it available for others.

#### Get all loans

```http
GET /api/loans
```

Returns a list of all active loans.

---

### 5. **Categories**

#### Get books by category

```http
GET /api/categories?category=Fiction
```

Returns a list of books in a specific category.

---

## Data Models

### Book

```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public Category Category { get; set; }
    public int BookCount { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public ICollection<Loan> Loans { get; set; }
    public bool IsAvailable { get; set; }
}
```

### Loan

```csharp
public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
```

---

## Technologies Used

- **.NET 8**: Framework for building the API.
- **Entity Framework Core**: ORM for interacting with the database.
- **SQLite**: Database used to store library data.
- **Swagger**: API documentation for easy testing and exploration of endpoints.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---
