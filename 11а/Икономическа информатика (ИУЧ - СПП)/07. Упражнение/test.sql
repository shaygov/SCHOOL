SELECT
    Books.Title AS BookTitle,
    Readers.Name AS ReaderName,
    Readers.Lastname AS ReaderLastname,
    Borrowing.DateBorrowing,
    Borrowing.ReturnDate,
    Borrowing.Returned
FROM
    (Books
    INNER JOIN Borrowing ON Books.ID = Borrowing.BooksID)
    INNER JOIN Readers ON Readers.ID = Borrowing.ReadersID
WHERE
    Borrowing.Returned = True;
