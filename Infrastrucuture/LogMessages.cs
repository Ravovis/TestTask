using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastrucuture
{
    public static class LogMessages
    {
        public static string ModelWasInvalid => "Log.Error.Model was invalid";
        public static string GotFromMocky => "Log.Info. Got all products from mocky";
        public static string DidntGotFromMocky => "Log.Info. Couln't connect to mocky. Returning empty result";
        public static string GotMostCommonWords => "Log.Info. Got most common words from all products.";
        public static string WordsWereHighlighted => "Log.Info. Words were highlighted.";

    }
}
