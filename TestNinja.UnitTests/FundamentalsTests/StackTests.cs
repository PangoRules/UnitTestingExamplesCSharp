using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class StackTests
    {
        //Test for the count property
        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        #region[Push method tests]
        [Test]
        public void Push_ArgIsNull_ThrowArgumentNullException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArg_AddObjectToStack()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("New string");

            Assert.That(stack.Count, Is.EqualTo(1));
        }
        #endregion

        #region[Pop method tests]
        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnItemRemovedAtTop()
        {
            var stack = new Fundamentals.Stack<string>();
            stack.Push("New string1");
            stack.Push("New string2");
            stack.Push("New string3");

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo("New string3"));
        }

        [Test]
        public void Pop_StackWithAFewObjects_RemoveObjectOnTheTop()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("New string1");
            stack.Push("New string2");
            stack.Push("New string3");

            var result = stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(2));
        }
        #endregion

        #region[Peek method tests]
        [Test]
        public void Peek_ListCountZero_ThrowInvalidOperationException()
        {
            var stack = new Fundamentals.Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_PeekStackWithObjects_ReturnObjectOnTopOfStack()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("New string1");
            stack.Push("New string2");

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("New string2"));
        }

        [Test]
        public void Peek_PeekStackWithObjects_DoesNotRemoveObjectFromTopOfStack()
        {
            var stack = new Fundamentals.Stack<string>();

            stack.Push("New string1");
            stack.Push("New string2");

            var result = stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(2));
        }
        #endregion
    }
}
