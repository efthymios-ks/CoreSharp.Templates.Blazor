﻿namespace Application.Messaging;

public static class Results
{
    // Methods 
    public static Result<TValue> Ok<TValue>(TValue result)
        => new(result, default);

    public static Result<TValue> Fail<TValue>(string error)
        => new(default, error);
}