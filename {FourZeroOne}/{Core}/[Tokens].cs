using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Perfection;
using ControlledFlows;
using FourZeroOne;
using MorseCode.ITask;
using PROTO_ZeroxFour_1.Util;

#nullable enable
namespace FourZeroOne.Core.Tokens
{
    using Token;
    using ResObj = Resolution.IResolution;
    using r = Resolutions;
    using ro = Resolutions.Objects;
    using Runtime;

    namespace Board
    {
        using rb = r.Objects.Board;
        namespace Coordinates
        {
            public sealed record Of : PureFunction<Resolution.Board.IPositioned, rb.Coordinates>
            {
                public Of(IToken<Resolution.Board.IPositioned> of) : base(of) { }
                protected override rb.Coordinates EvaluatePure(Resolution.Board.IPositioned in1)
                {
                    return in1.Position;
                }
            }
            public sealed record OffsetArea : PureFunction<rb.Coordinates, Resolution.IMulti<rb.Coordinates>, r.Multi<rb.Coordinates>>
            {
                public OffsetArea(IToken<rb.Coordinates> offset, IToken<Resolution.IMulti<rb.Coordinates>> area) : base(offset, area) { }
                protected override r.Multi<rb.Coordinates> EvaluatePure(rb.Coordinates in1, Resolution.IMulti<rb.Coordinates> in2)
                {
                    return new() { Values = in2.Values.Map(x => x.Add(in1)) };
                }
            }
        }
        namespace Hex
        {
            public sealed record AllHexes : Value<r.Multi<rb.Hex>>
            {
                protected override ITask<IOption<r.Multi<rb.Hex>>> Evaluate(IRuntime runtime)
                {
                    return Task.FromResult(new r.Multi<rb.Hex>() { Values = runtime.GetState().Board.Hexes }.AsSome()).AsITask();
                }
            }
            public sealed record AtPresent : PresentStateGetter<rb.Hex>
            {
                public AtPresent(IToken<rb.Hex> source) : base(source) { }
                protected override PIndexedSet<int, rb.Hex> GetStatePSet(IRuntime runtime) { return runtime.GetState().Board.Hexes; }
            }
            namespace Get
            {

            }
        }
        namespace Unit
        {
            namespace Get
            {
                public sealed record HP : PureFunction<rb.Unit, ro.Number>
                {
                    public HP(IToken<rb.Unit> of) : base(of) { }
                    protected override ro.Number EvaluatePure(rb.Unit in1) { return in1.HP; }
                }
                public sealed record Effects : PureFunction<rb.Unit, r.Multi<rb.UnitEffect>>
                {
                    public Effects(IToken<rb.Unit> of) : base(of) { }
                    protected override r.Multi<rb.UnitEffect> EvaluatePure(rb.Unit in1) { return in1.Effects; }

                }
                public sealed record Owner : PureFunction<rb.Unit, rb.Player>
                {
                    public Owner(IToken<rb.Unit> source) : base(source) { }
                    protected override rb.Player EvaluatePure(rb.Unit in1) { return in1.Owner; }
                }
            }
            namespace Set
            {
                using Resolutions.Actions.Board.Unit;
                public sealed record Position : PureFunction<rb.Unit, rb.Coordinates, PositionChange>
                {

                    public Position(IToken<rb.Unit> in1, IToken<rb.Coordinates> in2) : base(in1, in2) { }

                    protected override PositionChange EvaluatePure(rb.Unit in1, rb.Coordinates in2)
                    {
                        return new() { Subject = in1, SetTo = in2 };
                    }
                }
                public sealed record HP : PureFunction<rb.Unit, ro.Number, HPChange>
                {

                    public HP(IToken<rb.Unit> in1, IToken<ro.Number> in2) : base(in1, in2) { }

                    protected override HPChange EvaluatePure(rb.Unit in1, ro.Number in2)
                    {
                        return new() { Subject = in1, SetTo = in2 };
                    }
                }
                public sealed record Owner : PureFunction<rb.Unit, rb.Player, OwnerChange>
                {

                    public Owner(IToken<rb.Unit> in1, IToken<rb.Player> in2) : base(in1, in2) { }

                    protected override OwnerChange EvaluatePure(rb.Unit in1, rb.Player in2)
                    {
                        return new() { Subject = in1, SetTo = in2 };
                    }
                }
                public sealed record Effects : PureFunction<rb.Unit, r.Multi<rb.UnitEffect>, EffectsChange>
                {

                    public Effects(IToken<rb.Unit> in1, IToken<r.Multi<rb.UnitEffect>> in2) : base(in1, in2) { }

                    protected override EffectsChange EvaluatePure(rb.Unit in1, r.Multi<rb.UnitEffect> in2)
                    {
                        return new() { Subject = in1, SetTo = in2 };
                    }
                }
            }
            public sealed record AllUnits : Value<r.Multi<rb.Unit>>
            {
                protected override ITask<IOption<r.Multi<rb.Unit>>> Evaluate(IRuntime runtime)
                {
                    return Task.FromResult(new r.Multi<rb.Unit>() { Values = runtime.GetState().Board.Units }.AsSome()).AsITask();
                }
            }
            public sealed record AtPresent : PresentStateGetter<rb.Unit>
            {
                public AtPresent(IToken<rb.Unit> source) : base(source) { }
                protected override PIndexedSet<int, rb.Unit> GetStatePSet(IRuntime runtime) { return runtime.GetState().Board.Units; }
            }
        }
        namespace Player
        {
            public sealed record AllPlayers : Value<r.Multi<rb.Player>>
            {
                protected override ITask<IOption<r.Multi<rb.Player>>> Evaluate(IRuntime runtime)
                {
                    return Task.FromResult(new r.Multi<rb.Player>() { Values = runtime.GetState().Board.Players }.AsSome()).AsITask();
                }
            }
            public sealed record AtPresent : PresentStateGetter<rb.Player>
            {
                public AtPresent(IToken<rb.Player> source) : base(source) { }
                protected override PIndexedSet<int, rb.Player> GetStatePSet(IRuntime runtime) { return runtime.GetState().Board.Players; }
            }
        }
    }
    namespace IO
    {
        namespace Select
        {
            public sealed record One<R> : Function<Resolution.IMulti<R>, R> where R : class, ResObj
            {
                public One(IToken<Resolution.IMulti<R>> from) : base(from) { }

                protected async override ITask<IOption<R>> Evaluate(IRuntime runtime, IOption<Resolution.IMulti<R>> fromOpt)
                {
                    return fromOpt.Check(out var from)
                        ? (await runtime.ReadSelection(from.Values, 1)).RemapAs(x => x.First().NullToNone()).Press()
                        : new None<R>();
                }
                protected override IOption<string> CustomToString() => $"Select({Arg1})".AsSome();
            }

            //make range instead of single int count
            public sealed record Multiple<R> : Function<Resolution.IMulti<R>, ro.Number, r.Multi<R>> where R : class, ResObj
            {
                public Multiple(IToken<Resolution.IMulti<R>> from, IToken<ro.Number> count) : base(from, count) { }

                protected override async ITask<IOption<r.Multi<R>>> Evaluate(IRuntime runtime, IOption<Resolution.IMulti<R>> fromOpt, IOption<ro.Number> countOpt)
                {
                    return (fromOpt.Check(out var from) && countOpt.Check(out var count))
                        ? (await runtime.ReadSelection(from.Values, count.Value)).RemapAs(v => new r.Multi<R>() { Values = v })
                        : new None<r.Multi<R>>();
                }
                protected override IOption<string> CustomToString() => $"SelectMulti({Arg1}, {Arg2})".AsSome();

            }
        }
    }
    namespace Number
    {
        public sealed record Add : PureFunction<ro.Number, ro.Number, ro.Number>
        {
            public Add(IToken<ro.Number> operand1, IToken<ro.Number> operand2) : base(operand1, operand2) { }
            protected override ro.Number EvaluatePure(ro.Number a, ro.Number b) { return new() { Value = a.Value + b.Value }; }
            protected override IOption<string> CustomToString() => $"({Arg1} + {Arg2})".AsSome();
        }

        public sealed record Subtract : PureFunction<ro.Number, ro.Number, ro.Number>
        {
            public Subtract(IToken<ro.Number> operand1, IToken<ro.Number> operand2) : base(operand1, operand2) { }
            protected override ro.Number EvaluatePure(ro.Number a, ro.Number b) { return new() { Value = a.Value - b.Value }; }
            protected override IOption<string> CustomToString() => $"({Arg1} - {Arg2})".AsSome();
        }

        public sealed record Multiply : PureFunction<ro.Number, ro.Number, ro.Number>
        {
            public Multiply(IToken<ro.Number> operand1, IToken<ro.Number> operand2) : base(operand1, operand2) { }
            protected override ro.Number EvaluatePure(ro.Number a, ro.Number b) { return new() { Value = a.Value * b.Value }; }
            protected override IOption<string> CustomToString() => $"({Arg1} * {Arg2})".AsSome();
        }

        public sealed record Negate : PureFunction<ro.Number, ro.Number>
        {
            public Negate(IToken<ro.Number> operand) : base(operand) { }
            protected override ro.Number EvaluatePure(ro.Number operand) { return new() { Value = -operand.Value }; }
            protected override IOption<string> CustomToString() => $"(-{Arg1})".AsSome();
        }
        namespace Compare
        {
            public sealed record GreaterThan : PureFunction<ro.Number, ro.Number, ro.Bool>
            {
                public GreaterThan(IToken<ro.Number> a, IToken<ro.Number> b) : base(a, b) { }
                protected override ro.Bool EvaluatePure(ro.Number in1, ro.Number in2)
                {
                    return new() { IsTrue = in1.Value > in2.Value };
                }
                protected override IOption<string> CustomToString() => $"({Arg1} > {Arg2})".AsSome();
            }
        }
    }
    namespace Multi
    {
        
        public sealed record Union<R> : PureCombiner<Resolution.IMulti<R>, r.Multi<R>> where R : class, ResObj
        {
            public Union(IEnumerable<IToken<Resolution.IMulti<R>>> elements) : base(elements) { }
            public Union(params IToken<Resolution.IMulti<R>>[] elements) : base(elements) { }
            protected override r.Multi<R> EvaluatePure(IEnumerable<Resolution.IMulti<R>> inputs)
            {
                return new() { Values = inputs.Map(x => x.Values).Flatten() };
            }
            protected override IOption<string> CustomToString()
            {
                List<IToken<Resolution.IMulti<R>>> argList = [.. Args];
                return $"[{argList[0]}{argList[1..].AccumulateInto("", (msg, v) => $"{msg}, {v}")}]".AsSome();
            }
        }

        public sealed record Intersection<R> : PureCombiner<Resolution.IMulti<R>, r.Multi<R>> where R : class, ResObj
        {
            public Intersection(IEnumerable<IToken<Resolution.IMulti<R>>> sets) : base(sets) { }
            public Intersection(params IToken<Resolution.IMulti<R>>[] sets) : base(sets) { }
            protected override r.Multi<R> EvaluatePure(IEnumerable<Resolution.IMulti<R>> inputs)
            {
                var iter = inputs.GetEnumerator();
                if (!iter.MoveNext()) return new() { Values = [] };
                var o = iter.Current.Values;
                while (iter.MoveNext())
                {
                    o = o.Where(x => iter.Current.Values.HasMatch(y => x.Equals(y)));
                }
                return new() { Values = o };
            }
            protected override IOption<string> CustomToString()
            {
                List<IToken<Resolution.IMulti<R>>> argList = [.. Args];
                return $"{argList[0]}{argList[1..].AccumulateInto("", (msg, v) => $"{msg}I{v}")}".AsSome();
            }
        }

        public sealed record Exclusion<R> : PureFunction<Resolution.IMulti<R>, Resolution.IMulti<R>, r.Multi<R>> where R : class, ResObj
        {
            public Exclusion(IToken<Resolution.IMulti<R>> from, IToken<Resolution.IMulti<R>> exclude) : base(from, exclude) { }
            protected override r.Multi<R> EvaluatePure(Resolution.IMulti<R> in1, Resolution.IMulti<R> in2)
            {
                return new() { Values = in1.Values.Where(x => !in2.Values.HasMatch(y => y.ResEqual(x))) };
            }
            protected override IOption<string> CustomToString() => $"{Arg1} - {Arg2}".AsSome();

        }

        public sealed record Yield<R> : PureFunction<R, r.Multi<R>> where R : class, ResObj
        {
            public Yield(IToken<R> value) : base(value) { }
            protected override r.Multi<R> EvaluatePure(R in1)
            {
                return new() { Values = in1.Yield() };
            }
            protected override IOption<string> CustomToString() => $"^{Arg1}".AsSome();
        }

        public sealed record Count : PureFunction<Resolution.IMulti<ResObj>, ro.Number>
        {
            public Count(IToken<Resolution.IMulti<ResObj>> of) : base(of) { }
            protected override ro.Number EvaluatePure(Resolution.IMulti<ResObj> in1)
            {
                return new() { Value = in1.Count };
            }
        }
    }

    public record Unbox<R> : Function<ro.BoxedToken<R>, R> where R : class, ResObj
    {
        public Unbox(IToken<ro.BoxedToken<R>> a) : base(a) { }

        protected override ITask<IOption<R>> Evaluate(IRuntime runtime, IOption<ro.BoxedToken<R>> in1)
        {
            return in1.Check(out var action) ? runtime.PerformAction(action.Token) : Task.FromResult(new None<R>()).AsITask();
        }
        protected override IOption<string> CustomToString() => $"!{Arg1}".AsSome();
    }
    public record SubEnvironment<ROut> : PureFunction<Resolution.IMulti<ResObj>, ROut, ROut>
        where ROut : class, ResObj
    {
        public SubEnvironment(IToken<Resolution.IMulti<ResObj>> envModifiers, IToken<ROut> evalToken) : base(envModifiers, evalToken) { }
        protected override ROut EvaluatePure(Resolution.IMulti<ResObj> _, ROut in2)
        {
            return in2;
        }
        protected override IOption<string> CustomToString() => $"let {Arg1} in {{{Arg2}}}".AsSome();

    }
    public record Recursive<RArg1, ROut> : Macro.OneArg<RArg1, ROut>
        where RArg1 : class, ResObj
        where ROut : class, ResObj
    {
        public readonly Proxy.IProxy<Recursive<RArg1, ROut>, ROut> RecursiveProxy;
        public Recursive(IToken<RArg1> arg1, Proxy.IProxy<Recursive<RArg1, ROut>, ROut> recursiveProxy) : base(arg1, recursiveProxy)
        {
            RecursiveProxy = recursiveProxy;
        }
        protected override IOption<string> CustomToString() => $"@\"{(RecursiveProxy.GetHashCode()%7777).ToBase("vwmbkjqzsnthdiueoalrcgpfy", "")}\"({Arg1})".AsSome();

    }
    public record Recursive<RArg1, RArg2, ROut> : Macro.TwoArg<RArg1, RArg2, ROut>
        where RArg1 : class, ResObj
        where RArg2 : class, ResObj
        where ROut : class, ResObj
    {
        public readonly Proxy.IProxy<Recursive<RArg1, RArg2, ROut>, ROut> RecursiveProxy;
        public Recursive(IToken<RArg1> arg1, IToken<RArg2> arg2, Proxy.IProxy<Recursive<RArg1, RArg2, ROut>, ROut> recursiveProxy) : base(arg1, arg2, recursiveProxy)
        {
            RecursiveProxy = recursiveProxy;
        }
        protected override IOption<string> CustomToString() => $"@\"{(RecursiveProxy.GetHashCode() % 7777).ToBase("vwmbkjqzsnthdiueoalrcgpfy", "")}\"({Arg1}, {Arg2})".AsSome();

    }
    public record Recursive<RArg1, RArg2, RArg3, ROut> : Macro.ThreeArg<RArg1, RArg2, RArg3, ROut>
        where RArg1 : class, ResObj
        where RArg2 : class, ResObj
        where RArg3 : class, ResObj
        where ROut : class, ResObj
    {
        public readonly Proxy.IProxy<Recursive<RArg1, RArg2, RArg3, ROut>, ROut> RecursiveProxy;
        public Recursive(IToken<RArg1> arg1, IToken<RArg2> arg2, IToken<RArg3> arg3, Proxy.IProxy<Recursive<RArg1, RArg2, RArg3, ROut>, ROut> recursiveProxy) : base(arg1, arg2, arg3, recursiveProxy)
        {
            RecursiveProxy = recursiveProxy;
        }
        protected override IOption<string> CustomToString() => $"@\"{(RecursiveProxy.GetHashCode() % 7777).ToBase("vwmbkjqzsnthdiueoalrcgpfy", "")}\"({Arg1}, {Arg2}, {Arg3})".AsSome();

    }
    public record IfElse<R> : Function<ro.Bool, ro.BoxedToken<R>, ro.BoxedToken<R>, ro.BoxedToken<R>> where R : class, ResObj
    {
        public IfElse(IToken<ro.Bool> condition, IToken<ro.BoxedToken<R>> positive, IToken<ro.BoxedToken<R>> negative) : base(condition, positive, negative) { }
        protected override ITask<IOption<ro.BoxedToken<R>>> Evaluate(IRuntime runtime, IOption<ro.Bool> in1, IOption<ro.BoxedToken<R>> in2, IOption<ro.BoxedToken<R>> in3)
        {
            return Task.FromResult( in1.RemapAs(x => x.IsTrue ? in2 : in3).Press() ).AsITask();
        }
        protected override IOption<string> CustomToString() => $"if {Arg1} then {Arg2} else {Arg3}".AsSome();
    }
    public sealed record Variable<R> : Token<r.Actions.VariableAssign<R>> where R : class, ResObj
    {
        public Variable(VariableIdentifier<R> identifier, IToken<R> token) : base(token)
        {
            _identifier = identifier;
        }
        public override ITask<IOption<r.Actions.VariableAssign<R>>> Resolve(IRuntime runtime, IOption<ResObj>[] args)
        {
            var refObject = (IOption<R>)args[0];
            return Task.FromResult(refObject.RemapAs(x => new r.Actions.VariableAssign<R>(_identifier) { Object = refObject })).AsITask();
        }
        protected override IOption<string> CustomToString() => $"{_identifier}={ArgTokens[0]}".AsSome();

        private readonly VariableIdentifier<R> _identifier;
    }
    public sealed record Rule<R> : PureValue<r.Actions.RuleAdd> where R : class, ResObj
    {
        public Rule(Rule.IRule rule)
        {
            _rule = rule;
        }

        protected override r.Actions.RuleAdd EvaluatePure()
        {
            return new r.Actions.RuleAdd() { Rule = _rule };
        }

        private readonly Rule.IRule _rule;
    }
    public sealed record Fixed<R> : PureValue<R> where R : class, ResObj
    {
        public readonly R Resolution;
        public Fixed(R resolution)
        {
            Resolution = resolution;
        }
        protected override R EvaluatePure()
        {
            return Resolution;
        }
        protected override IOption<string> CustomToString() => $"|{Resolution}|".AsSome();
    }
    public sealed record Nolla<R> : Value<R> where R : class, ResObj
    {
        public Nolla() { }
        protected override ITask<IOption<R>> Evaluate(IRuntime _) { return Task.FromResult(new None<R>()).AsITask(); }
        protected override IOption<string> CustomToString() => "nolla".AsSome();
    }
    public sealed record Reference<R> : Value<R> where R : class, ResObj
    {
        public Reference(VariableIdentifier<R> toIdentifier) => _toIdentifier = toIdentifier;

        protected override ITask<IOption<R>> Evaluate(IRuntime runtime)
        {
            var o = (runtime.GetState().Variables[_toIdentifier] is IOption<R> val) ? val :
                throw new Exception($"Reference token resolved to non-existent or wrongly-typed object.\n" +
                $"Identifier: {_toIdentifier}\n" +
                $"Expected: {typeof(R).Name}\n" +
                $"Recieved: {runtime.GetState().Variables[_toIdentifier]}\n" +
                $"Current Scope:\n" +
                $"{runtime.GetState().Variables.Elements.AccumulateInto("", (msg, x) => msg + $"> '{x.key}' : {x.val}\n")}");
            return Task.FromResult(o).AsITask();
        }
        protected override IOption<string> CustomToString() => $"&{_toIdentifier}".AsSome();

        private readonly VariableIdentifier<R> _toIdentifier;
    }
}