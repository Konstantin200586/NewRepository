using NUnit.Framework;
using System.Reflection;

namespace SMBTools.Web.Test.Helpers
{
    public class AssertHelper
    {
        public static bool AreObjectsEqual(object expectedObject, object actualObject, params string[] ignoreList)
        {
            if (expectedObject != null && actualObject != null)
            {
                Type expectedObjectType = expectedObject.GetType();
                Type actualObjectType = actualObject.GetType();

                foreach (PropertyInfo expectedPropertyInfo in expectedObjectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead && !ignoreList.Contains(p.Name)))
                {
                    object expectedValue = expectedPropertyInfo.GetValue(expectedObject, null);

                    PropertyInfo actualPropertyInfo = actualObjectType.GetProperty(expectedPropertyInfo.Name);

                    if (actualPropertyInfo == null)
                    {
                        Assert.Fail($"The actual object does not have {expectedPropertyInfo.Name}");
                    }

                    object actualValue = null;

                    try
                    {
                        actualValue = actualPropertyInfo.GetValue(actualObject, null);
                    }
                    catch (TargetException ex)
                    {
                        Assert.Fail("Error getting property");
                    }

                    Assert.AreEqual(expectedValue, actualValue);
                }
            }
            else
            {
                Assert.IsTrue(object.Equals(expectedObject, actualObject));
            }

            return true;
        }
    }
}
