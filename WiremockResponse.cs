using System;
using System.Collections;
using System.Collections.Generic;

public class WiremockResponse
{

    private readonly static WiremockResponse _instance = new WiremockResponse();

    private readonly static IDictionary<int, string> _responses = new Dictionary<int, string()>
    {
        { 1, "Content response 1" },
        { 2, "Content response 2" },
        { 3, "Content response 3" },
        { 4, "Content response 4" },
    };

    private WiremockResponse()
    {
    }

    public static WiremockResponse Instance
    {
        get
        {
            return _instance;
        }
    }

    public string GetRandomMessage()
    {
        return _responses.ElementAt(rand.Next(0, dict.Count)).Value;
    }
}
