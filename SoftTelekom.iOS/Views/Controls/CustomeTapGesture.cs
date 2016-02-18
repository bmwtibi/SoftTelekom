using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace SoftTelekom.iOS.Views.Controls
{
    public class CustomeTapGesture : UIGestureRecognizer
    {
         private Action _changeAction;
        private Action _executeAction;
        private Action _resetAction;
        public CustomeTapGesture(Action changeAction, Action executeAction, Action resetAction)
        {
            _changeAction = changeAction;
            _executeAction = executeAction;
            _resetAction = resetAction;
        }

        private bool _strokeUp = false;

        /// <summary>
        ///   Called when the touches end or the recognizer state fails
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            _strokeUp = false;
            _resetAction.Invoke();
        }
        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);
            if (touches.Count != 1)
            {
                base.State = UIGestureRecognizerState.Failed;
            }
            else
            {
                _changeAction.Invoke();  
            }

        }
        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);
            base.State = UIGestureRecognizerState.Failed;
        }
        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            if (base.State == UIGestureRecognizerState.Possible)
            {
                base.State = UIGestureRecognizerState.Recognized;
                _executeAction.Invoke();
            }
            Console.WriteLine(base.State.ToString());
        }


        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);
            CGPoint newPoint = (touches.AnyObject as UITouch).LocationInView(View);

            if (newPoint.X < 0 || newPoint.X > View.Bounds.Width || newPoint.Y < 0 || newPoint.Y > View.Bounds.Height) 
                base.State = UIGestureRecognizerState.Failed;

        }
    }
}