// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;

using Microsoft.Test;
using Microsoft.Test.Logging;
using Microsoft.Test.TestTypes;
using Microsoft.Test.Discovery;

namespace Microsoft.Test.Animation
{
    /// <summary>
    /// <area>Animation.LowLevelScenarios.Regressions</area>


    [Test(2, "Animation.LowLevelScenarios.Regressions", "SlipBehaviorRepeatTest")]
    public class SlipBehaviorRepeatTest : XamlTest
    {

        #region Test case members

        private string              _inputString         = null;
        private Ellipse             _animatedElement;
        private Storyboard          _resourceStoryboard;
        private DispatcherTimer     _aTimer              = null;
        
        #endregion


        #region Constructor

        public SlipBehaviorRepeatTest(): this(@"SlipBehaviorRepeatSlip.xaml")
        {
        }

        [Variation(@"SlipBehaviorRepeat.xaml")]
        [Variation(@"SlipBehaviorRepeatGrow.xaml")]

        /******************************************************************************
        * Function:         SlipBehaviorRepeatTest Constructor
        ******************************************************************************/
        public SlipBehaviorRepeatTest(string testValue) : base(testValue)
        {
            _inputString = testValue;
            InitializeSteps += new TestStep(GetElement);
            RunSteps += new TestStep(FindStoryboardInResource);
            RunSteps += new TestStep(StartTest);
            RunSteps += new TestStep(StartStoryboard);
        }

        #endregion


        #region Test Steps

        
        /******************************************************************************
        * Function:          GetElement
        ******************************************************************************/
        /// <summary>
        /// Retrieves the animated element from the markup.
        /// </summary>
        /// <returns>Returns success if the element was found</returns>
        TestResult GetElement()
        {
            _animatedElement = (Ellipse)AnimationUtilities.FindElement(RootElement, "ellipse1");
            
            if (_animatedElement == null)
            {
                GlobalLog.LogEvidence("The animated element was not found.");
                return TestResult.Fail;
            }
            else
            {
                GlobalLog.LogEvidence("The animated element was found.");
                return TestResult.Pass;
            }
        }

        /******************************************************************************
           * Function:          FindStoryboardInResource
           ******************************************************************************/
        /// <summary>
        /// Retrieve the Storyboard that is specified in the Resources section in Markup.
        /// <returns>Returns success if the Storyboard was found</returns>
        /// </summary>
        /// <returns>TestResult - Pass/Fail, depending on the success of FindResource</returns>
        private TestResult FindStoryboardInResource()
        {
            _resourceStoryboard = (Storyboard)RootElement.FindResource("StoryKey");

            if (_resourceStoryboard == null)
            {
                return TestResult.Fail;
            }
            else
            {
                return TestResult.Pass;
            }
        }

        /******************************************************************************
        * Function:          StartTest
        ******************************************************************************/
        /// <summary>
        /// Starts the test by starting a DispatcherTimer; in the case of the "Grow"
        /// condition, will start the Storyboard as well.
        /// </summary>
        /// <returns>TestResult.Pass</returns>
        private TestResult StartTest()
        {
            if (_inputString.IndexOf("Grow") >= 0)
            {
                _animatedElement.BeginStoryboard(_resourceStoryboard);
            }

            _aTimer = new DispatcherTimer(DispatcherPriority.Normal);
            _aTimer.Tick += new EventHandler(OnTick);
            _aTimer.Interval = new TimeSpan(0,0,0,0,4000);
            _aTimer.Start();

            return TestResult.Pass;
        }
        
        /******************************************************************************
        * Function:          OnTick
        ******************************************************************************/
        /// <summary>
        /// Invoked every time the DispatcherTimer ticks. Used to control the timing of verification.
        /// </summary>
        /// <returns></returns>
        private void OnTick(object sender, EventArgs e)          
        {
            _aTimer.Stop();
            Signal("AnimationDone", TestResult.Pass);
        }
        
        /******************************************************************************
        * Function:          StartStoryboard
        ******************************************************************************/
        /// <summary>
        /// Starts the Storyboard, which should throw an exception.
        /// </summary>
        /// <returns></returns>
        private TestResult StartStoryboard()
        {
            if (_inputString.IndexOf("Grow") >= 0)
            {
                WaitForSignal("AnimationDone");

                double actValue = (double)_animatedElement.GetValue(Canvas.TopProperty);
                double expValue = -20d;
                GlobalLog.LogEvidence("----Verifying the Animation----");
                GlobalLog.LogEvidence("Expected Canvas.Top : " + expValue);
                GlobalLog.LogEvidence("Actual   Canvas.Top : " + actValue);

                if (actValue == expValue)
                {
                    return TestResult.Pass;
                }
                else
                {
                    return TestResult.Fail;
                }
            }
            else
            {
                SetExpectedErrorTypeInStep(typeof(NotSupportedException), "Outer");                
                _animatedElement.BeginStoryboard(_resourceStoryboard);   //This throws the exception.

                WaitForSignal("AnimationDone");
            }

            return TestResult.Pass;
        }

        #endregion
    }
}
