-- Full e-commerce schema and complete seed for Supabase (PostgreSQL)
-- Uses the same studentNN@example.bg customers (NN = 01..19)
-- Adapted for Supabase with UUID PKs, proper PostgreSQL types, and RLS

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Drop in dependency order
DROP TABLE IF EXISTS Payments CASCADE;
DROP TABLE IF EXISTS OrderItems CASCADE;
DROP TABLE IF EXISTS Orders CASCADE;
DROP TABLE IF EXISTS ProductCategories CASCADE;
DROP TABLE IF EXISTS Products CASCADE;
DROP TABLE IF EXISTS Categories CASCADE;
DROP TABLE IF EXISTS Customers CASCADE;

-- Core entities
CREATE TABLE Customers (
  CustomerId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  Email TEXT NOT NULL UNIQUE,
  Phone TEXT,
  FirstName TEXT NOT NULL,
  LastName TEXT NOT NULL,
  BirthDate DATE,
  Gender TEXT CHECK (Gender IN ('female','male','other')),
  Instagram TEXT,
  CreatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE Categories (
  CategoryId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  Name TEXT NOT NULL,
  Slug TEXT UNIQUE
);

CREATE TABLE Products (
  ProductId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  Sku TEXT NOT NULL UNIQUE,
  Name TEXT NOT NULL,
  Description TEXT,
  Price NUMERIC(10,2) NOT NULL,
  Currency CHAR(3) NOT NULL DEFAULT 'BGN',
  Stock INTEGER NOT NULL DEFAULT 0,
  ImageUrl TEXT,
  Active BOOLEAN NOT NULL DEFAULT TRUE,
  CreatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE ProductCategories (
  ProductId UUID NOT NULL,
  CategoryId UUID NOT NULL,
  PRIMARY KEY (ProductId, CategoryId),
  CONSTRAINT fk_pc_product FOREIGN KEY (ProductId) REFERENCES Products(ProductId) ON DELETE CASCADE,
  CONSTRAINT fk_pc_category FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE
);

CREATE TABLE Orders (
  OrderId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  CustomerId UUID NOT NULL,
  Status TEXT NOT NULL DEFAULT 'pending' CHECK (Status IN ('pending','paid','shipped','delivered','cancelled','refunded')),
  Subtotal NUMERIC(10,2) NOT NULL DEFAULT 0.00,
  Shipping NUMERIC(10,2) NOT NULL DEFAULT 0.00,
  Discount NUMERIC(10,2) NOT NULL DEFAULT 0.00,
  Total NUMERIC(10,2) NOT NULL DEFAULT 0.00,
  Currency CHAR(3) NOT NULL DEFAULT 'BGN',
  CreatedAt TIMESTAMPTZ NOT NULL DEFAULT NOW(),
  CONSTRAINT fk_order_customer FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId) ON DELETE CASCADE
);

CREATE TABLE OrderItems (
  OrderItemId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  OrderId UUID NOT NULL,
  ProductId UUID NOT NULL,
  Sku TEXT NOT NULL,
  Name TEXT NOT NULL,
  UnitPrice NUMERIC(10,2) NOT NULL,
  Quantity INTEGER NOT NULL,
  LineTotal NUMERIC(10,2) NOT NULL,
  CONSTRAINT fk_oi_order FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
  CONSTRAINT fk_oi_product FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);

CREATE TABLE Payments (
  PaymentId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  OrderId UUID NOT NULL,
  Provider TEXT NOT NULL CHECK (Provider IN ('card','cod','paypal','bank')),
  Amount NUMERIC(10,2) NOT NULL,
  Currency CHAR(3) NOT NULL DEFAULT 'BGN',
  Status TEXT NOT NULL DEFAULT 'pending' CHECK (Status IN ('pending','authorized','captured','failed','refunded')),
  PaidAt TIMESTAMPTZ,
  CONSTRAINT fk_payment_order FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE
);

-- Enable Row Level Security (RLS)
ALTER TABLE Customers ENABLE ROW LEVEL SECURITY;
ALTER TABLE Categories ENABLE ROW LEVEL SECURITY;
ALTER TABLE Products ENABLE ROW LEVEL SECURITY;
ALTER TABLE Orders ENABLE ROW LEVEL SECURITY;
ALTER TABLE OrderItems ENABLE ROW LEVEL SECURITY;
ALTER TABLE Payments ENABLE ROW LEVEL SECURITY;

-- Create RLS policies (allow all for now - adjust as needed)
CREATE POLICY "Allow all operations on customers" ON Customers FOR ALL USING (true);
CREATE POLICY "Allow all operations on categories" ON Categories FOR ALL USING (true);
CREATE POLICY "Allow all operations on products" ON Products FOR ALL USING (true);
CREATE POLICY "Allow all operations on orders" ON Orders FOR ALL USING (true);
CREATE POLICY "Allow all operations on order_items" ON OrderItems FOR ALL USING (true);
CREATE POLICY "Allow all operations on payments" ON Payments FOR ALL USING (true);

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
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-MUG-004'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student01@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'cod', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student01@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- O2: student09 -> headphones (card)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student09@example.bg'), 'paid', 129.90, 0.00, 0.00, 129.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-HEAD-001'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student09@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'card', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student09@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- O3: student07 -> shoes (pending, no payment yet)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student07@example.bg'), 'pending', 89.00, 0.00, 0.00, 89.00);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-SHOE-002'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student07@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- O4: student13 -> 2x book (PayPal)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student13@example.bg'), 'paid', 33.80, 0.00, 0.00, 33.80);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 2, p.Price*2
FROM Orders o JOIN Products p ON p.Sku='SKU-BOOK-003'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student13@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'paypal', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student13@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- O5: student17 -> mug (bank)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student17@example.bg'), 'paid', 24.50, 0.00, 0.00, 24.50);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-MUG-004'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student17@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'bank', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student17@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- O6: student02 -> book (card)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student02@example.bg'), 'paid', 16.90, 0.00, 0.00, 16.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-BOOK-003'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student02@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'card', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student02@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- O7: student05 -> shoes + shipping (COD)
INSERT INTO Orders (CustomerId, Status, Subtotal, Shipping, Discount, Total)
VALUES ((SELECT CustomerId FROM Customers WHERE Email='student05@example.bg'), 'paid', 89.00, 4.90, 0.00, 93.90);

INSERT INTO OrderItems (OrderId, ProductId, Sku, Name, UnitPrice, Quantity, LineTotal)
SELECT o.OrderId, p.ProductId, p.Sku, p.Name, p.Price, 1, p.Price
FROM Orders o JOIN Products p ON p.Sku='SKU-SHOE-002'
WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student05@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

INSERT INTO Payments (OrderId, Provider, Amount, Status, PaidAt)
SELECT o.OrderId, 'cod', o.Total, 'captured', NOW()
FROM Orders o WHERE o.CustomerId=(SELECT CustomerId FROM Customers WHERE Email='student05@example.bg')
ORDER BY o.CreatedAt DESC LIMIT 1;

-- Create useful indexes for sorting
CREATE INDEX idx_products_price ON Products(Price);
CREATE INDEX idx_products_created_at ON Products(CreatedAt DESC);
CREATE INDEX idx_orders_created_at ON Orders(CreatedAt DESC);
CREATE INDEX idx_orders_customer_id ON Orders(CustomerId);
CREATE INDEX idx_orders_status ON Orders(Status);
CREATE INDEX idx_customers_email ON Customers(Email);
CREATE INDEX idx_customers_last_name ON Customers(LastName);

-- Create a view for easy sorting examples
CREATE VIEW OrderSummary AS
SELECT 
  o.OrderId,
  o.CreatedAt,
  c.FirstName || ' ' || c.LastName AS CustomerName,
  o.Status,
  o.Total,
  COUNT(oi.OrderItemId) AS ItemCount
FROM Orders o
JOIN Customers c ON c.CustomerId = o.CustomerId
LEFT JOIN OrderItems oi ON oi.OrderId = o.OrderId
GROUP BY o.OrderId, o.CreatedAt, c.FirstName, c.LastName, o.Status, o.Total;

-- ==============================================
-- ПРИМЕРИ ЗА СОРТИРАНЕ НА ДАННИ (за урока)
-- ==============================================

-- 1. Сортиране по цена (възходящо)
SELECT ProductId, Name, Price, Category
FROM Products 
ORDER BY Price ASC;

-- 2. Сортиране по цена (низходящо)
SELECT ProductId, Name, Price, Category
FROM Products 
ORDER BY Price DESC;

-- 3. Сортиране по категория, после по цена
SELECT Name, Category, Price
FROM Products 
ORDER BY Category ASC, Price DESC;

-- 4. Сортиране по дата на създаване (най-нови първи)
SELECT Name, Price, CreatedAt
FROM Products 
ORDER BY CreatedAt DESC;

-- 5. Сортиране с NULLS LAST
SELECT FirstName, LastName, Instagram
FROM Customers 
ORDER BY Instagram DESC NULLS LAST, LastName ASC;

-- 6. Сортиране по алиас (изчислена колона)
SELECT 
  Name, 
  Price, 
  Stock,
  Price * Stock AS TotalValue
FROM Products 
ORDER BY TotalValue DESC;

-- 7. Сортиране по позиция в SELECT
SELECT Name, Price, Stock
FROM Products 
ORDER BY 2 DESC;  -- сортиране по 2-ра колона (Price)

-- 8. Сортиране на агрегати (топ категории)
SELECT 
  Category, 
  COUNT(*) AS ProductCount,
  AVG(Price) AS AvgPrice,
  SUM(Price) AS TotalValue
FROM Products 
GROUP BY Category
ORDER BY TotalValue DESC;

-- 9. Сортиране с JOIN (поръчки с клиенти)
SELECT 
  o.OrderId,
  c.FirstName || ' ' || c.LastName AS CustomerName,
  o.Total,
  o.CreatedAt
FROM Orders o
JOIN Customers c ON c.CustomerId = o.CustomerId
ORDER BY o.CreatedAt DESC, c.LastName ASC;

-- 10. Сортиране с българска колация
SELECT FirstName, LastName, Email
FROM Customers 
ORDER BY LastName COLLATE "bg_BG.utf8" ASC;

-- 11. Сортиране с Full Text Search (по релевантност)
SELECT 
  Name, 
  Description,
  ts_rank(to_tsvector('english', Name || ' ' || COALESCE(Description, '')), 
          plainto_tsquery('english', 'headphones')) AS rank
FROM Products 
WHERE to_tsvector('english', Name || ' ' || COALESCE(Description, '')) 
      @@ plainto_tsquery('english', 'headphones')
ORDER BY rank DESC;

-- 12. Сортиране с Window функции (топ продукти в категория)
SELECT 
  Name,
  Category,
  Price,
  RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS RankInCategory
FROM Products 
ORDER BY Category ASC, RankInCategory ASC;

-- 13. Сортиране с LIMIT (топ 5 най-скъпи продукта)
SELECT Name, Price, Category
FROM Products 
ORDER BY Price DESC 
LIMIT 5;

-- 14. Сортиране с OFFSET (страница 2, 5 продукта на страница)
SELECT Name, Price, Category
FROM Products 
ORDER BY CreatedAt DESC 
LIMIT 5 OFFSET 5;

-- 15. Сортиране с множествени ключове (детерминизъм)
SELECT 
  p.Name,
  oi.Quantity,
  oi.UnitPrice,
  o.CreatedAt
FROM OrderItems oi
JOIN Products p ON p.ProductId = oi.ProductId
JOIN Orders o ON o.OrderId = oi.OrderId
ORDER BY o.CreatedAt DESC, p.Name ASC, oi.Quantity DESC;

-- 16. Сортиране по JSON поле (ако имаме metadata колона)
-- ALTER TABLE Products ADD COLUMN metadata JSONB;
-- SELECT Name, metadata->>'brand' AS Brand
-- FROM Products 
-- ORDER BY (metadata->>'priority')::integer DESC NULLS LAST;

-- 17. Сортиране с CASE (приоритетизиране)
SELECT 
  Name,
  Status,
  Total,
  CreatedAt
FROM Orders 
ORDER BY 
  CASE Status
    WHEN 'pending' THEN 1
    WHEN 'paid' THEN 2
    WHEN 'shipped' THEN 3
    WHEN 'delivered' THEN 4
    ELSE 5
  END,
  CreatedAt DESC;

-- 18. Сортиране с подзаявка
SELECT 
  c.FirstName,
  c.LastName,
  COUNT(o.OrderId) AS OrderCount
FROM Customers c
LEFT JOIN Orders o ON o.CustomerId = c.CustomerId
GROUP BY c.CustomerId, c.FirstName, c.LastName
ORDER BY OrderCount DESC, c.LastName ASC;

-- 19. Сортиране с HAVING (филтриране на групи)
SELECT 
  Category,
  COUNT(*) AS ProductCount,
  AVG(Price) AS AvgPrice
FROM Products 
GROUP BY Category
HAVING COUNT(*) >= 2
ORDER BY AvgPrice DESC;

-- 20. Сортиране с UNION (комбиниране на резултати)
SELECT 'Product' AS Type, Name, Price AS Amount, CreatedAt
FROM Products
UNION ALL
SELECT 'Order' AS Type, 'Order #' || OrderId::text, Total AS Amount, CreatedAt
FROM Orders
ORDER BY CreatedAt DESC, Type ASC;
