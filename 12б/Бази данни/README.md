# Supabase: PostgreSQL и модерна база данни

## Инсталация
- Създайте безплатен акаунт в Supabase: https://supabase.com
- Създайте нов проект в Supabase Dashboard

## Supabase Dashboard
- Отворете: https://supabase.com/dashboard
- Вход: с вашия Supabase акаунт
- Изберете проекта си

## База и настройки
- Базата се създава автоматично с PostgreSQL
- Кодиране: UTF-8 (по подразбиране)
- Row Level Security (RLS) е включен за сигурност

## Бързи действия
- Създаване на таблица: Table Editor → "New table"
- SQL заявки: SQL Editor → напишете заявката → Run
- Импорт на .sql: SQL Editor → копирайте и поставете кода → Run

## Примерни заявки (Supabase/PostgreSQL)
```sql
CREATE TABLE Students (
  StudentId UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
  FullName  TEXT NOT NULL,
  CreatedAt TIMESTAMPTZ DEFAULT NOW()
);

INSERT INTO Students (FullName) VALUES ('Ivan Petrov'), ('Maria Ivanova');

SELECT * FROM Students ORDER BY FullName;
```

## Връзка с приложения
- Host: намира се в Settings → Database
- Port: 5432 (PostgreSQL)
- User: postgres
- Password: намира се в Settings → Database
- Database: postgres

## Зареждане на примерни данни (seed)
- В SQL Editor: копирайте и поставете съдържанието от `04_commerce_full_seed_supabase.sql` → Run
- Или използвайте `complete_school_database_supabase.sql` за училищна база

## Supabase предимства
- ✅ Безплатен план с 500MB база данни
- ✅ Автоматични backup-и
- ✅ Real-time поддръжка
- ✅ REST API автоматично генериран
- ✅ Row Level Security за сигурност
- ✅ Full Text Search
- ✅ JSON/JSONB поддръжка
- ✅ Модерен PostgreSQL

## Полезни функции
- **Table Editor**: Визуално редактиране на данни
- **SQL Editor**: Изпълнение на SQL заявки
- **API Docs**: Автоматична документация за API
- **Auth**: Вградена система за потребители
- **Storage**: Файлово хранилище
