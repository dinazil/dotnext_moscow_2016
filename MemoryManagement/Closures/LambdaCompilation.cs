

using System;

namespace Closures
{
    class Helper
    {
        public void foo() { }
    }

    class LambdaCompilation
    {
        private void goo()
        {
            var h = new Helper();
            Action f = () => h.foo();
            f();
        }
    }
}
