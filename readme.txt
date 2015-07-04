webcamTracking.htm demonstrates the basic usage of getUserMedia in a static html file (no dependencies in other files).

getUserMedia is used in the case of Firefox (not sure of smartphone/tablet support) and Chrome (smartphone/tablet supported).
Browsers with no getUserMedia support default to an image file input method to get access to the webcam.

A more complex usecase that includes form input checking, barcode reader ,google map API and database management can be found in:
	MobileQA > UserInterface > Views > Log > TrackingEvent.cshtml

