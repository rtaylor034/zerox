using System.Collections.Generic;
using System.Collections;
using System;

#nullable disable
namespace Perfection
{

    public interface ITiple<out Ta, out Tb>
    {
        Ta A { get; }
        Tb B { get; }
    }
    public readonly record struct Tiple<Ta, Tb>(Ta a, Tb b) : ITiple<Ta, Tb>
    {
        public Ta A { get; private init; } = a;
        public Tb B { get; private init; } = b;
        public static implicit operator Tiple<Ta, Tb>(ValueTuple<Ta, Tb> tup) => new(tup.Item1, tup.Item2);
        public static implicit operator ValueTuple<Ta, Tb>(Tiple<Ta, Tb> tup) => new(tup.A, tup.B);
    }
    public static class TipleExtensions
    {
        public static Tiple<Ta, Tb> Concrete<Ta, Tb>(this ITiple<Ta, Tb> tup) => tup is Tiple<Ta, Tb> o ? o : new(tup.A, tup.B);
        public static ITiple<Ta, Tb> Tiple<Ta, Tb>(this ITiple<Ta, Tb> tiple) => tiple;
        public static ITiple<Ta, Tb> Tiple<Ta, Tb>(this ValueTuple<Ta, Tb> tup) => new Tiple<Ta, Tb>(tup.Item1, tup.Item2);
    }

}