-- База данни за онлайн магазин - Supabase версия
-- Подходяща за един час в училище
-- Включва: създаване на таблици, seed данни и примери за сортиране

-- Enable UUID extension
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ==============================================
-- 1. СЪЗДАВАНЕ НА ТАБЛИЦИ
-- ==============================================

-- Таблица за категории продукти
CREATE TABLE IF NOT EXISTS categories (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    category_name TEXT NOT NULL UNIQUE,
    description TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW()
);

-- Таблица за продукти
CREATE TABLE IF NOT EXISTS products (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    product_name TEXT NOT NULL,
    description TEXT,
    price DECIMAL(10,2) NOT NULL CHECK (price > 0),
    stock_quantity INTEGER DEFAULT 0 CHECK (stock_quantity >= 0),
    category_id UUID,
    is_active BOOLEAN DEFAULT true,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    FOREIGN KEY (category_id) REFERENCES categories(id) ON DELETE SET NULL
);

-- Таблица за клиенти
CREATE TABLE IF NOT EXISTS customers (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email TEXT UNIQUE NOT NULL,
    phone TEXT,
    address TEXT,
    city TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW()
);

-- Таблица за поръчки
CREATE TABLE IF NOT EXISTS orders (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    customer_id UUID NOT NULL,
    order_date TIMESTAMPTZ DEFAULT NOW(),
    total_amount DECIMAL(10,2) NOT NULL,
    status TEXT DEFAULT 'pending' CHECK (status IN ('pending', 'processing', 'shipped', 'delivered', 'cancelled')),
    shipping_address TEXT,
    notes TEXT,
    FOREIGN KEY (customer_id) REFERENCES customers(id) ON DELETE CASCADE
);

-- Таблица за детайли на поръчките
CREATE TABLE IF NOT EXISTS order_items (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    order_id UUID NOT NULL,
    product_id UUID NOT NULL,
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    unit_price DECIMAL(10,2) NOT NULL,
    total_price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id) ON DELETE CASCADE
);

-- Enable Row Level Security
ALTER TABLE categories ENABLE ROW LEVEL SECURITY;
ALTER TABLE products ENABLE ROW LEVEL SECURITY;
ALTER TABLE customers ENABLE ROW LEVEL SECURITY;
ALTER TABLE orders ENABLE ROW LEVEL SECURITY;
ALTER TABLE order_items ENABLE ROW LEVEL SECURITY;

-- Create RLS policies
CREATE POLICY "Allow all operations on categories" ON categories FOR ALL USING (true);
CREATE POLICY "Allow all operations on products" ON products FOR ALL USING (true);
CREATE POLICY "Allow all operations on customers" ON customers FOR ALL USING (true);
CREATE POLICY "Allow all operations on orders" ON orders FOR ALL USING (true);
CREATE POLICY "Allow all operations on order_items" ON order_items FOR ALL USING (true);

-- ==============================================
-- 2. SEED ДАННИ
-- ==============================================

-- Вмъкване на категории
INSERT INTO categories (category_name, description) VALUES
('Електроника', 'Смартфони, лаптопи, таблети'),
('Дрехи', 'Мъжки, дамски и детски дрехи'),
('Книги', 'Художествена и учебна литература'),
('Спорт', 'Спортни стоки и оборудване'),
('Дом и градина', 'Мебели и декорация');

-- Вмъкване на продукти
INSERT INTO products (product_name, description, price, stock_quantity, category_id) VALUES
('iPhone 15', 'Apple смартфон 128GB', 1299.99, 15, (SELECT id FROM categories WHERE category_name = 'Електроника')),
('Samsung Galaxy S24', 'Android смартфон 256GB', 999.99, 12, (SELECT id FROM categories WHERE category_name = 'Електроника')),
('MacBook Air', 'Apple лаптоп 13" M2', 1199.99, 8, (SELECT id FROM categories WHERE category_name = 'Електроника')),
('Дънки Levis', 'Мъжки дънки 501', 89.99, 25, (SELECT id FROM categories WHERE category_name = 'Дрехи')),
('Рокля Zara', 'Дамска рокля лято', 45.99, 30, (SELECT id FROM categories WHERE category_name = 'Дрехи')),
('Възглавница Nike', 'Спортна възглавница', 25.99, 50, (SELECT id FROM categories WHERE category_name = 'Спорт')),
('Тениски Adidas', 'Мъжки тениски 3 бр.', 39.99, 40, (SELECT id FROM categories WHERE category_name = 'Спорт')),
('Книга "1984"', 'Джордж Оруел', 12.99, 20, (SELECT id FROM categories WHERE category_name = 'Книги')),
('Учебник по математика', 'За 11 клас', 24.99, 15, (SELECT id FROM categories WHERE category_name = 'Книги')),
('Диван IKEA', '3-местен диван сив', 599.99, 5, (SELECT id FROM categories WHERE category_name = 'Дом и градина'));

-- Вмъкване на клиенти
INSERT INTO customers (first_name, last_name, email, phone, city) VALUES
('Иван', 'Петров', 'ivan.petrov@email.com', '0888123456', 'София'),
('Мария', 'Георгиева', 'maria.georgieva@email.com', '0888123457', 'Пловдив'),
('Петър', 'Димитров', 'petar.dimitrov@email.com', '0888123458', 'Варна'),
('Анна', 'Стоянова', 'anna.stoyanova@email.com', '0888123459', 'София'),
('Георги', 'Иванов', 'georgi.ivanov@email.com', '0888123460', 'Бургас');

-- Вмъкване на поръчки
INSERT INTO orders (customer_id, total_amount, status, shipping_address) VALUES
((SELECT id FROM customers WHERE email = 'ivan.petrov@email.com'), 1339.98, 'delivered', 'ул. Витоша 15, София'),
((SELECT id FROM customers WHERE email = 'maria.georgieva@email.com'), 135.97, 'shipped', 'ул. Главна 22, Пловдив'),
((SELECT id FROM customers WHERE email = 'petar.dimitrov@email.com'), 89.99, 'processing', 'ул. Морска 8, Варна'),
((SELECT id FROM customers WHERE email = 'anna.stoyanova@email.com'), 37.98, 'delivered', 'бул. Цариградско шосе 45, София'),
((SELECT id FROM customers WHERE email = 'georgi.ivanov@email.com'), 1224.98, 'pending', 'ул. Славянска 12, Бургас');

-- Вмъкване на детайли на поръчките
INSERT INTO order_items (order_id, product_id, quantity, unit_price, total_price) VALUES
-- Поръчка 1: Иван Петров
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'ivan.petrov@email.com')), 
 (SELECT id FROM products WHERE product_name = 'iPhone 15'), 1, 1299.99, 1299.99),
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'ivan.petrov@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Дънки Levis'), 1, 89.99, 89.99),

-- Поръчка 2: Мария Георгиева
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'maria.georgieva@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Рокля Zara'), 1, 45.99, 45.99),
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'maria.georgieva@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Възглавница Nike'), 1, 25.99, 25.99),
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'maria.georgieva@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Тениски Adidas'), 1, 39.99, 39.99),

-- Поръчка 3: Петър Димитров
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'petar.dimitrov@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Дънки Levis'), 1, 89.99, 89.99),

-- Поръчка 4: Анна Стоянова
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'anna.stoyanova@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Книга "1984"'), 1, 12.99, 12.99),
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'anna.stoyanova@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Учебник по математика'), 1, 24.99, 24.99),

-- Поръчка 5: Георги Иванов
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'georgi.ivanov@email.com')), 
 (SELECT id FROM products WHERE product_name = 'MacBook Air'), 1, 1199.99, 1199.99),
((SELECT id FROM orders WHERE customer_id = (SELECT id FROM customers WHERE email = 'georgi.ivanov@email.com')), 
 (SELECT id FROM products WHERE product_name = 'Диван IKEA'), 1, 599.99, 599.99);

-- ==============================================
-- 3. ПРИМЕРИ ЗА СОРТИРАНЕ
-- ==============================================

-- Сортиране на продукти по цена (най-евтини първи)
SELECT product_name, price, stock_quantity
FROM products 
ORDER BY price ASC;

-- Сортиране на продукти по име (азбучен ред)
SELECT product_name, price, category_id
FROM products 
ORDER BY product_name ASC;

-- Сортиране на клиенти по фамилия
SELECT first_name, last_name, email, city
FROM customers 
ORDER BY last_name ASC, first_name ASC;

-- Сортиране на поръчки по дата (най-нови първи)
SELECT 
    c.first_name,
    c.last_name,
    o.order_date,
    o.total_amount,
    o.status
FROM orders o
JOIN customers c ON o.customer_id = c.id
ORDER BY o.order_date DESC;

-- Сортиране на продукти по категория и цена
SELECT 
    cat.category_name,
    p.product_name,
    p.price
FROM products p
JOIN categories cat ON p.category_id = cat.id
ORDER BY cat.category_name ASC, p.price DESC;

-- Най-скъпи продукти (топ 5)
SELECT product_name, price, stock_quantity
FROM products 
ORDER BY price DESC
LIMIT 5;

-- Сортиране на поръчки по сума (най-големи първи)
SELECT 
    CONCAT(c.first_name, ' ', c.last_name) AS customer_name,
    o.total_amount,
    o.status,
    o.order_date
FROM orders o
JOIN customers c ON o.customer_id = c.id
ORDER BY o.total_amount DESC;

-- ==============================================
-- 4. ДОПЪЛНИТЕЛНИ ПОЛЕЗНИ ЗАЯВКИ
-- ==============================================

-- Общ брой записи в таблиците
SELECT 'categories' AS table_name, COUNT(*) AS record_count FROM categories
UNION ALL
SELECT 'products', COUNT(*) FROM products
UNION ALL
SELECT 'customers', COUNT(*) FROM customers
UNION ALL
SELECT 'orders', COUNT(*) FROM orders
UNION ALL
SELECT 'order_items', COUNT(*) FROM order_items;

-- Продукти с малко наличност (под 10 броя)
SELECT product_name, stock_quantity, price
FROM products 
WHERE stock_quantity < 10
ORDER BY stock_quantity ASC;

-- Статистика по категории
SELECT 
    c.category_name,
    COUNT(p.id) AS product_count,
    AVG(p.price) AS average_price,
    SUM(p.stock_quantity) AS total_stock
FROM categories c
LEFT JOIN products p ON c.id = p.category_id
GROUP BY c.id, c.category_name
ORDER BY product_count DESC;

-- ==============================================
-- 5. ИНДЕКСИ ЗА ПОДОБРЯВАНЕ НА ПРОИЗВОДИТЕЛНОСТТА
-- ==============================================

CREATE INDEX idx_products_category_id ON products(category_id);
CREATE INDEX idx_products_price ON products(price);
CREATE INDEX idx_products_name ON products(product_name);
CREATE INDEX idx_customers_email ON customers(email);
CREATE INDEX idx_customers_last_name ON customers(last_name);
CREATE INDEX idx_orders_customer_id ON orders(customer_id);
CREATE INDEX idx_orders_order_date ON orders(order_date);
CREATE INDEX idx_orders_status ON orders(status);
CREATE INDEX idx_order_items_order_id ON order_items(order_id);
CREATE INDEX idx_order_items_product_id ON order_items(product_id);
