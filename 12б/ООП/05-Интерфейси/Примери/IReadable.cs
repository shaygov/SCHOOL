using System;

interface IReadable
{
    void Read();
    string GetContent();
    bool IsAvailable();
}

interface IWritable
{
    void Write(string content);
    void Edit(string newContent);
    void Clear();
}

interface IPrintable
{
    void Print();
    int GetPageCount();
}
