﻿#nullable enable
namespace SixShaded.FourZeroOne.Rule.Defined
{
    public record RuledToken<R> : RuntimeHandledValue<R>, IRuledToken<R>
        where R : class, Res
    {
        public required IRule<R> AppliedRule { get; init; }

        // [0] is always self/original proxy, rest are arg proxies in-order.
        public required IProxy<Res>[] Proxies { get; init; }
        protected override EStateImplemented MakeData()
        {
            var definition = AppliedRule.DefinitionUnsafe;
            return new EStateImplemented.MetaExecute()
            {
                Token = definition.Token,
                ObjectWrites =
                    definition.SelfIdentifier.IsA<Resolution.IMemoryAddress<Res>>().Yield()
                    .Concat(definition.ArgAddresses)
                    .ZipShort(
                        definition.IsA<Res>().Yield()
                        .Concat(Proxies)
                        .Map(x => x.AsSome()))
                    .Tipled(),
                RuleMutes = [AppliedRule.ID]
            };
        }
    }
}
