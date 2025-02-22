﻿#nullable enable
namespace SixShaded.FourZeroOne.Rule.Defined.Matchers
{
    public record MacroMatcher<RVal> : IRuleMatcher<IMacroValue<RVal>>
        where RVal : Res
    {
        public required MacroLabel Label { get; init; }
        public bool MatchesToken(Tok token) => token is Macro<RVal> macro && macro.Label.Equals(Label);
    }
    public record MacroMatcher<RArg1, ROut> : IRuleMatcher<IMacroFunction<RArg1, ROut>>
        where RArg1 : Res
        where ROut : Res
    {
        public required MacroLabel Label { get; init; }
        public bool MatchesToken(Tok token) => token is Macro<RArg1, ROut> macro && macro.Label.Equals(Label);
    }
    public record MacroMatcher<RArg1, RArg2, ROut> : IRuleMatcher<IMacroFunction<RArg1, RArg2, ROut>>
        where RArg1 : Res
        where RArg2 : Res
        where ROut : Res
    {
        public required MacroLabel Label { get; init; }
        public bool MatchesToken(Tok token) => token is Macro<RArg1, RArg2, ROut> macro && macro.Label.Equals(Label);
    }
    public record MacroMatcher<RArg1, RArg2, RArg3, ROut> : IRuleMatcher<IMacroFunction<RArg1, RArg2, RArg3, ROut>>
        where RArg1 : Res
        where RArg2 : Res
        where RArg3 : Res
        where ROut : Res
    {
        public required MacroLabel Label { get; init; }
        public bool MatchesToken(Tok token) => token is Macro<RArg1, RArg2, RArg3, ROut> macro && macro.Label.Equals(Label);
    }
}
