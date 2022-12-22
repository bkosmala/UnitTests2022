namespace Calculator
{
    public class StringCalculator
    {

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var parameters = ConvertParametersToNumbers(numbers);
            return SumElements(parameters);
        }

        private int SumElements(List<int> values)
        {
            var result = 0;
            values.ForEach(x =>
            {
                result += x;
            });

            return result;
        }
        /*
         ShouldThrowException_WhenNegativeNumbersPresent("//,\n-10,5,2","negatives not allowed:-10")
   Source: StringCalculatorTests.cs line 75
   Duration: 108 ms

  Message: 
  Expected: <System.ArgumentException> and property Message equal to "negatives not allowed:-10"
  But was:  "Parameter // is not a number"
         */
        private List<int> ConvertParametersToNumbers(string input)
        {
            var separators = new HashSet<string>() { ",", "\n" };

            if (input.StartsWith("//"))
            {
                var endIndex = input.IndexOf('\n');
                var delimiterPart = input.Substring(2, endIndex-2);
                var delimiters = delimiterPart.Replace("[", "").Split(']');  
                foreach(var delimiter in delimiters )
                {
                    separators.Add(delimiter);
                }
                
                input = input.Replace($"//{delimiterPart}\n","");
            }
            var negativeNumbers = new List<int>();
            var outputNumbers = input.Split(separators.ToArray(), StringSplitOptions.TrimEntries).Select(x =>
            {
                if (!Int32.TryParse(x, out int result))
                {
                    throw new ArgumentException($"Parameter {x} is not a number");
                }

                if (result<0)
                    negativeNumbers.Add(result);                

                return result >1000 ? 0 : result;
            }).ToList();

            return negativeNumbers.Any() ? throw new ArgumentException($"negatives not allowed:{String.Join(',',negativeNumbers)}") :
                outputNumbers;
        }
    }
}