namespace KaminiCenter.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IEnumParseService
    {
        string GetEnumDescription(string name, Type typeOfEnum);
    }
}
