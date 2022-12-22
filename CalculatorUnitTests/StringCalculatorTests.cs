using Calculator;

namespace StringCalculatorTests
{
    [Parallelizable(ParallelScope.Children)]
    public class Tests
    {
        private readonly StringCalculator _subject = new StringCalculator();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnZero_WhenEmptyStringPassed()
        {
            var res = _subject.Add("");
            Assert.That(res, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnSingleValue_WhenOnlyOneParameterPassed()
        {
            var res = _subject.Add("10");
            Assert.That(res, Is.EqualTo(10));
        }

        [TestCase("10,5",15)]
        [TestCase("10,5,2",17)]
        [TestCase("10,5,2,8",25)]
        [TestCase("10,5,3,8",26)]
        [TestCase("10,5,1000,8", 1023)]
        [TestCase("10,5,1001,8", 23)]
        public void ShouldReturnSumOfTwoOrMoreParametersPassed(string input, int result)
        {
            var res = _subject.Add(input);
            Assert.That(res, Is.EqualTo(result));
        }

        [TestCase("10\n5", 15)]
        [TestCase("10\n5,2", 17)]
        [TestCase("10,5,2\n8", 25)]
        [TestCase("10\n5\n3\n8",26)]
        [TestCase("10,5,1000,8", 1023)]
        [TestCase("10,5\n1001,8", 23)]
        public void ShouldReturnSumOfTwoOrMoreParametersPassed_WhenNewLineSeparatorUsed(string input, int result)
        {
            var res = _subject.Add(input);
            Assert.That(res, Is.EqualTo(result));
        }

        [TestCase("//;\n10;5",15)]
        [TestCase("//,\n10,5,2", 17)]
        [TestCase("//k\n10k5,2\n8", 25)]
        [TestCase("10\n5\n3\n8", 26)]
        [TestCase("//;\n10,5,1000;8", 1023)]
        [TestCase("//!\n10!5!1001!8", 23)]
        [TestCase("//[kkk]\n10kkk5kkk3kkk8", 26)]
        [TestCase("//[??]\n10??5??3??8", 26)]
        [TestCase("//[??][*]\n10??5*3??8", 26)]
        public void ShouldReturnSumOfTwoOrMoreParametersPassed_WhenDiffrentDelimetersUsed(string input, int result)
        {
            var res = _subject.Add(input);
            Assert.That(res, Is.EqualTo(result));
        }

        [TestCase("-1", "negatives not allowed:-1")]
        [TestCase("0,-1", "negatives not allowed:-1")]
        [TestCase("//;\n10;-5", "negatives not allowed:-5")]
        [TestCase("//,\n-10,5,2", "negatives not allowed:-10")]
        [TestCase("//k\n10k-5,-2\n8", "negatives not allowed:-5,-2")]
        [TestCase("10\n-5\n-3\n-8", "negatives not allowed:-5,-3,-8")]
        [TestCase("10,5,1000,-8","negatives not allowed:-8")]
        [TestCase("10,-5,1001,-8", "negatives not allowed:-5,-8")]
        public void ShouldThrowException_WhenNegativeNumbersPresent(string input, string exceptionMessage)
        {
            Assert.That( () => _subject.Add(input), Throws.TypeOf<ArgumentException>()
                .With
                .Message
                .EqualTo(exceptionMessage));
        }

    }
}