﻿namespace SixShaded.FourZeroOne.Rule.Defined;

public record RuledToken<R> : Token.Defined.RuntimeHandledValue<R>, Unsafe.IRuledToken<R>
    where R : class, Res
{
    // [0] is always self/original proxy, rest are arg proxies in-order.
    public required IProxy<Res>[] Proxies { get; init; }
    public required Unsafe.IRule<R> AppliedRule { get; init; }

    protected override FZOSpec.EStateImplemented MakeData()
    {
        var definition = AppliedRule.DefinitionUnsafe;
        return new FZOSpec.EStateImplemented.MetaExecute
        {
            Token = definition.Token,
            ObjectWrites =
                definition.SelfIdentifier.IsA<IMemoryAddress<Res>>().Yield()
                    .Concat(definition.ArgAddresses)
                    .ZipShort(
                        definition.IsA<Res>().Yield()
                            .Concat(Proxies)
                            .Map(x => x.AsSome()))
                    .Tipled(),
            RuleMutes = [AppliedRule.ID],
        };
    }
}