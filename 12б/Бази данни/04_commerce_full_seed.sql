-- Full e-commerce schema and complete seed (all tables populated)
-- Uses the same studentNN@example.bg customers (NN = 01..19)

CREATE DATABASE IF NOT EXISTS commerce_db CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE commerce_db;

-- Drop in dependency order
DROP TABLE IF EXISTS Payments;
DROP TABLE IF EXISTS OrderItems;
DROP TABLE IF EXISTS Orders;
DROP TABLE IF EXISTS ProductCategories;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS Categories;
DROP TABLE IF EXISTS Customers;

-- Core entities
CREATE TABLE Customers (
  CustomerId INT AUTO_INCREMENT PRIMARY KEY,
  Email VARCHAR(120) NOT NULL UNIQUE,
  Phone VARCHAR(20),
  FirstName VARCHAR(60) NOT NULL,
  LastName VARCHAR(60) NOT NULL,
  BirthDate DATE,
  Gender ENUM('female','male','other') DEFAULT NULL,
  Instagram VARCHAR(100),
  CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Categories (
  CategoryId INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(80) NOT NULL,
  Slug VARCHAR(100) UNIQUE
);

CREATE TABLE Products (
  ProductId INT AUTO_INCREMENT PRIMARY KEY,
  Sku VARCHAR(40) NOT NULL UNIQUE,
  Name VARCHAR(150) NOT NULL,
  Description TEXT,
  Price DECIMAL(10,2) NOT NULL,
  Currency CHAR(3) NOT NULL DEFAULT 'BGN',
  Stock INT NOT NULL DEFAULT 0,
  ImageUrl VARCHAR(255),
  Active TINYINT(1) NOT NULL DEFAULT 1,
  CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE ProductCategories (
  ProductId INT NOT NULL,
  CategoryId INT NOT NULL,
  PRIMARY KEY (ProductId, CategoryId),
  CONSTRAINT fk_pc_product2 FOREIGN KEY (ProductId) REFERENCES Products(ProductId) ON DELETE CASCADE,
  CONSTRAINT fk_pc_category2 FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE
);

CREATE TABLE Orders (
  OrderId INT AUTO_INCREMENT PRIMARY KEY,
  CustomerId INT NOT NULL,
  Status ENUM('pending','paid','shipped','delivered','cancelled','refunded') NOT NULL DEFAULT 'pending',
  Subtotal DECIMAL(10,2) NOT NULL DEFAULT 0.00,
  Shipping DECIMAL(10,2) NOT NULL DEFAULT 0.00,
  Discount DECIMAL(10,2) NOT NULL DEFAULT 0.00,
  Total DECIMAL(10,2) NOT NULL DEFAULT 0.00,
  Currency CHAR(3) NOT NULL DEFAULT 'BGN',
  CreatedAt TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT fk_order_customer2 FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId) ON DELETE CASCADE
);

CREATE TABLE OrderItems (
  OrderItemId INT AUTO_INCREMENT PRIMARY KEY,
  OrderId INT NOT NULL,
  ProductId INT NOT NULL,
  Sku VARCHAR(40) NOT NULL,
  Name VARCHAR(150) NOT NULL,
  UnitPrice DECIMAL(10,2) NOT NULL,
  Quantity INT NOT NULL,
  LineTotal DECIMAL(10,2) NOT NULL,
  CONSTRAINT fk_oi_order2 FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
  CONSTRAINT fk_oi_product2 FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Payments (
  PaymentId INT AUTO_INCREMENT PRIMARY KEY,
  OrderId INT NOT NULL,
  Provider ENUM('card','cod','paypal','bank') NOT NULL,
  Amount DECIMAL(10,2) NOT NULL,
  Currency CHAR(3) NOT NULL DEFAULT 'BGN',
  Status ENUM('pending','authorized','captured','failed','refunded') NOT NULL DEFAULT 'pending',
  PaidAt DATETIME,
  CONSTRAINT fk_payment_order2 FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE
);

-- Seed customers (same students 01..19)
INSERT INTO Customers (Email, Phone, FirstName, LastName, BirthDate, Gender, Instagram) VALUES
('student01@example.bg', NULL, 'Айше', 'Дагон', NULL, NULL, NULL),
('student02@example.bg', NULL, 'Айше', 'Пастрамарска', NULL, NULL, NULL),
('student03@example.bg', NULL, 'Анифе', 'Кусарова', NULL, NULL, NULL),
('student04@example.bg', NULL, 'Атидже', 'Ходжова', NULL, NULL, NULL),
('student05@example.bg', NULL, 'Атидже', 'Тупева', NULL, NULL, NULL),
('student06@example.bg', NULL, 'Джемиле', 'Хасан', NULL, NULL, NULL),
('student07@example.bg', NULL, 'Джемиле', 'Али', NULL, NULL, NULL),
('student08@example.bg', NULL, 'Зарифка', 'Хахньова', NULL, NULL, NULL),
('student09@example.bg', NULL, 'Муртаза', 'Клечов', NULL, NULL, NULL),
('student10@example.bg', NULL, 'Муса', 'Шехов', NULL, NULL, NULL),
('student11@example.bg', NULL, 'Муса', 'Кимов', NULL, NULL, NULL),
('student12@example.bg', NULL, 'Муса', 'Аянски', NULL, NULL, NULL),
('student13@example.bg', NULL, 'Муса', 'Абдула', NULL, NULL, NULL),
('student14@example.bg', NULL, 'Мустафа', 'Аянски', NULL, NULL, NULL),
('student15@example.bg', NULL, 'Найме', 'Бекир', NULL, NULL, NULL),
('student16@example.bg', NULL, 'Сабит', 'Али', NULL, NULL, NULL),
('student17@example.bg', NULL, 'Сайде', 'Зекрия', NULL, NULL, NULL),
('student18@example.bg', NULL, 'Хасан', 'Кавунски', NULL, NULL, NULL),
('student19@example.bg', NULL, 'Юсуф', 'Звездьов', NULL, NULL, NULL);

-- Seed categories
INSERT INTO Categories (Name, Slug) VALUES
('Електроника', 'elektronika'),
('Мода', 'moda'),
('Книги', 'knigi'),
('Дом и градина', 'dom-i-gradina');

-- Seed products
INSERT INTO Products (Sku, Name, Description, Price, Stock, ImageUrl) VALUES
('SKU-HEAD-001', 'Безжични слушалки WaveX', 'Bluetooth 5.3, шумопотискане, 30ч батерия', 129.90, 50, 'https://picsum.photos/seed/headphones/400'),
('SKU-SHOE-002', 'Маратонки UrbanFit', 'Леки, удобни, дишащи', 89.00, 120, 'https://picsum.photos/seed/shoes/400'),
('SKU-BOOK-003', 'Книга: "Човекът в търсене на смисъл"', 'Виктор Франкъл', 16.90, 200, 'https://picsum.photos/seed/book/400'),
('SKU-MUG-004', 'Термочаша KeepWarm', 'Неръждаема стомана, 500ml', 24.50, 80, 'https://picsum.photos/seed/mug/400');

-- Map products to categories
INSERT INTO ProductCategories (ProductId, CategoryId)
VALUES
((SELECT ProductId FROM Products WHERE Sku='SKU-HEAD-001'), (SELECT CategoryId FROM Categories WHERE Slug='elektronika')),
((SELECT ProductId FROM Products WHERE Sku='SKU-SHOE-002'), (SELECT CategoryId FROM Categories WHERE Slug='moda')),
((SELECT ProductId FROM Products WHERE Sku='SKU-BOOK-003'), (SELECT CategoryId FROM Categories WHERE Slug='knigi')),
((SELECT ProductId FROM Products WHERE Sku='SKU-MUG-004'), (SELECT CategoryId FROM Categories WHERE Slug='dom-i-gradina'));

-- Seed orders (cover multiple customers)
-- O1: student01 -> book + mug (COD)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student01@example.bg'), 'paid', 41.40, 3.50, 0.00, 44.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-BOOK-003'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student01@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-MUG-004'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student01@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'cod', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student01@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

-- O2: student09 -> headphones (card)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student09@example.bg'), 'paid', 129.90, 0.00, 0.00, 129.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-HEAD-001'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student09@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'card', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student09@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

-- O3: student07 -> shoes (pending, no payment yet)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student07@example.bg'), 'pending', 89.00, 0.00, 0.00, 89.00);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-SHOE-002'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student07@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

-- O4: student13 -> 2x book (PayPal)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student13@example.bg'), 'paid', 33.80, 0.00, 0.00, 33.80);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 2, p.Price*2
FROM Orders o JOIN Products p ON p.Sku='SKU-BOOK-003'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student13@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'paypal', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student13@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

-- O5: student17 -> mug (bank)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student17@example.bg'), 'paid', 24.50, 0.00, 0.00, 24.50);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-MUG-004'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student17@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'bank', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student17@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

-- O6: student02 -> book (card)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student02@example.bg'), 'paid', 16.90, 0.00, 0.00, 16.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-BOOK-003'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student02@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'card', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student02@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

-- O7: student05 -> shoes + shipping (COD)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student05@example.bg'), 'paid', 89.00, 4.90, 0.00, 93.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-SHOE-002'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student05@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'cod', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student05@example.bg')
ORDER BY o.OrderId DESC LIMIT 1;


