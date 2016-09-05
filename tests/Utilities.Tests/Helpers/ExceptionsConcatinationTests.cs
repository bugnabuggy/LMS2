using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using LMS.Utilities.Exceptions;


namespace Utilities.Tests.Helpers
{
    [TestFixture]
    public class ExceptionsConcatinationTests
    {
        [Test]
        public void Should_concat_exceprion_messages()
        {
            var exp = new Exception("Line 1");
            exp = new Exception("Line 2", exp);
            exp = new Exception("Line 3", exp);

            var list = exp.GetExceptionMessages();
            var msg = string.Join(", ", list.ToArray());

            Assert.AreEqual(list.Count, 3);
            Assert.IsTrue(msg.Equals("Line 3, Line 2, Line 1"));

        }
    }
}
