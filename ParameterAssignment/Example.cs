using JetBrains.UI.ExceptionReport.ExceptionAnalyzer.v4;
using JetBrains.UI.Wpf;

namespace ParameterAssignment
{
    class Example
    {
        public int MyFunction(int val)
        {
            if(val > 5)
                val = 5;
            return val;
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
