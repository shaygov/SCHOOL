-- Library Database SQL Implementation
-- Task: Create ER diagram for library with Readers, Books, Loans (N:M relationship)

-- Drop tables if they exist (for clean recreation)
DROP TABLE IF EXISTS Loans;
DROP TABLE IF EXISTS Books;
DROP TABLE IF EXISTS Readers;

-- Create Readers table
CREATE TABLE Readers (
    reader_id INT PRIMARY KEY AUTO_INCREMENT,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone VARCHAR(20),
    address VARCHAR(200),
    registration_date DATE NOT NULL,
    membership_status ENUM('active', 'inactive', 'suspended') DEFAULT 'active'
);

-- Create Books table
CREATE TABLE Books (
    book_id INT PRIMARY KEY AUTO_INCREMENT,
    isbn VARCHAR(20) UNIQUE NOT NULL,
    title VARCHAR(200) NOT NULL,
    author VARCHAR(100) NOT NULL,
    publisher VARCHAR(100),
    publication_year YEAR,
    genre VARCHAR(50),
    pages INT,
    availability_status ENUM('available', 'borrowed', 'maintenance') DEFAULT 'available',
    shelf_location VARCHAR(20)
);

-- Create Loans table (Junction table for N:M relationship)
CREATE TABLE Loans (
    loan_id INT PRIMARY KEY AUTO_INCREMENT,
    reader_id INT NOT NULL,
    book_id INT NOT NULL,
    loan_date DATE NOT NULL,
    due_date DATE NOT NULL,
    return_date DATE NULL,
    status ENUM('active', 'returned', 'overdue') DEFAULT 'active',
    fine_amount DECIMAL(8,2) DEFAULT 0.00,
    
    -- Foreign key constraints
    FOREIGN KEY (reader_id) REFERENCES Readers(reader_id) ON DELETE CASCADE,
    FOREIGN KEY (book_id) REFERENCES Books(book_id) ON DELETE CASCADE,
    
    -- Ensure a reader can't borrow the same book multiple times simultaneously
    UNIQUE KEY unique_active_loan (reader_id, book_id, status)
);

-- Create indexes for better performance
CREATE INDEX idx_reader_email ON Readers(email);
CREATE INDEX idx_reader_name ON Readers(last_name, first_name);
CREATE INDEX idx_book_title ON Books(title);
CREATE INDEX idx_book_author ON Books(author);
CREATE INDEX idx_book_isbn ON Books(isbn);
CREATE INDEX idx_loan_reader ON Loans(reader_id);
CREATE INDEX idx_loan_book ON Loans(book_id);
CREATE INDEX idx_loan_dates ON Loans(loan_date, due_date, return_date);

-- Insert sample data for testing
INSERT INTO Readers (first_name, last_name, email, phone, address, registration_date, membership_status) VALUES
('Иван', 'Петров', 'ivan.petrov@email.com', '0888123456', 'ул. Витоша 15, София', '2023-01-15', 'active'),
('Мария', 'Георгиева', 'maria.georgieva@email.com', '0888765432', 'ул. Цариградско шосе 25, София', '2023-02-20', 'active'),
('Петър', 'Стоянов', 'petar.stoyanov@email.com', '0888111222', 'ул. България 10, Пловдив', '2023-03-10', 'active'),
('Анна', 'Димитрова', 'anna.dimitrova@email.com', '0888333444', 'ул. Марица 5, Варна', '2023-01-25', 'inactive');

INSERT INTO Books (isbn, title, author, publisher, publication_year, genre, pages, availability_status, shelf_location) VALUES
('978-954-07-1234-5', 'Програмиране на C#', 'Иван Иванов', 'Техника', 2022, 'Информатика', 450, 'available', 'A1-001'),
('978-954-07-1235-2', 'Бази данни и SQL', 'Мария Петрова', 'Софтпрес', 2021, 'Информатика', 380, 'available', 'A1-002'),
('978-954-07-1236-9', 'Въведение в икономиката', 'Петър Георгиев', 'Икономика', 2023, 'Икономика', 320, 'available', 'B2-001'),
('978-954-07-1237-6', 'История на България', 'Анна Стоянова', 'История', 2020, 'История', 280, 'borrowed', 'C3-001'),
('978-954-07-1238-3', 'Математика за 12 клас', 'Георги Димитров', 'Просвета', 2023, 'Математика', 400, 'available', 'D4-001');

INSERT INTO Loans (reader_id, book_id, loan_date, due_date, return_date, status, fine_amount) VALUES
(1, 1, '2024-01-15', '2024-02-15', NULL, 'active', 0.00),
(1, 2, '2024-01-20', '2024-02-20', '2024-02-18', 'returned', 0.00),
(2, 3, '2024-01-25', '2024-02-25', NULL, 'active', 0.00),
(3, 4, '2024-01-10', '2024-02-10', NULL, 'overdue', 15.50),
(2, 5, '2024-02-01', '2024-03-01', NULL, 'active', 0.00);

-- Useful queries for the library system

-- Query 1: Show all active loans with reader and book information
SELECT 
    r.first_name,
    r.last_name,
    b.title,
    b.author,
    l.loan_date,
    l.due_date,
    l.status
FROM Loans l
JOIN Readers r ON l.reader_id = r.reader_id
JOIN Books b ON l.book_id = b.book_id
WHERE l.status = 'active';

-- Query 2: Show overdue books
SELECT 
    r.first_name,
    r.last_name,
    r.email,
    b.title,
    l.loan_date,
    l.due_date,
    DATEDIFF(CURDATE(), l.due_date) AS days_overdue,
    l.fine_amount
FROM Loans l
JOIN Readers r ON l.reader_id = r.reader_id
JOIN Books b ON l.book_id = b.book_id
WHERE l.status = 'overdue' OR (l.status = 'active' AND l.due_date < CURDATE());

-- Query 3: Show book availability
SELECT 
    b.title,
    b.author,
    b.availability_status,
    b.shelf_location,
    CASE 
        WHEN b.availability_status = 'available' THEN 'Достъпна'
        WHEN b.availability_status = 'borrowed' THEN 'Заета'
        ELSE 'В поддръжка'
    END AS status_bg
FROM Books b
ORDER BY b.title;

-- Query 4: Show reader borrowing history
SELECT 
    r.first_name,
    r.last_name,
    b.title,
    l.loan_date,
    l.return_date,
    l.status
FROM Readers r
JOIN Loans l ON r.reader_id = l.reader_id
JOIN Books b ON l.book_id = b.book_id
WHERE r.reader_id = 1  -- Replace with specific reader ID
ORDER BY l.loan_date DESC;
