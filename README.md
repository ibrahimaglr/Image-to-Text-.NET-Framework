<p align="center">
  <img src="media/logobanner.png" width="800px">
</p>

# Image-to-Text-.NET-Framework
### Available Features

* convert the screenshot it takes to text
* convert an existing image file to text
* copy conversions to clipboard
* startup with windows


### Current Bugs
* start with windows feature doesn't read keyboard hooks (it fixes when i turn it off and on)
* If the window scale and layout is above 100%, half of the screenshot is taken. 
# Main Window
* language pack selection
* button to read text from the image you choose
* keyboard shortcut selection
* github project link
<p align="center">
  <img src="media/mainwindow.png" width="400px">
</p>



# ScreenShot Panel
* You can access this panel with the keyboard key you assigned from the main form.
* F1 displays the original image of the text that was last copied to memory
* F2 switches to new excerpt window (clicks anywhere on the screen while in this form)
* You can exit this form with 'esc'.
<p align="center">
  <img src="media/Screenshotpanel.png" width="500px">
</p>

# Sample
results of plain text document (readable background and text color)
It will show a notification when text reading is complete, if you click on the notification it will create a file in the 'temp' folder and show the text copied to memory.
If we compare the results with the original text, it can be seen that tesseract ocr gives above-average results, but objectively these results are not enough for users.
### OUTPUTs
<p align="center">
  <img src="media/data.png" width="500px">
</p>
<p align="center">
  <img src="media/resultpreview.png" width="500px">
</p>
<p align="center">
  <img src="media/result.png" width="500px">
</p>
