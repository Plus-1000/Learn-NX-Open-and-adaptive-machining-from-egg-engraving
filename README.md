# Learn NX Open and Adaptive Machining technique from egg engraving
This fictional attempt seeks to uncover the process of CNC egg engraving, all the efforts are confined to process analysis and coding, without egg engraving taking place on the CNC machine.

Click the picture below to see the movie from TTL:     
[![Create points on face with NX OPEN](https://img.youtube.com/vi/ZKnJ4k-d2gs/0.jpg)](https://www.youtube.com/watch?v=ZKnJ4k-d2gs)

  
Click the picture below to see the movie from JINGDIAO:     
[![Create points on face with NX OPEN](https://img.youtube.com/vi/2ylW4YDFr9g/0.jpg)](https://www.youtube.com/watch?v=2ylW4YDFr9g)  
I was deeply attracted when the first time I saw the videos. About a year ago, I've been trying to learn NX OPEN and VB.NET, aiming to uncover the secrets behind egg engraving. Due to limited skills in computer coding and understanding of NXOPEN, these studies are progressing slowly at an entry-level. Nonetheless, I'm grateful to my boss, colleagues, and friends for their kind help, guidance, and support.

----------------------------

As every egg is unique, when carving an egg, two key points could be addressed: detecting the shape of the egg and automatically generating a toolpath. Consequently, the simple adaptive machining process involves two steps:   

## **1. Detect part deformation**  
Detect part distortion by probe:    
Part deformation can be checked by probe or scanner, here we use the probe as an example:   


When the probe hits part along the Z- direction, we can get the Z coordinate of the destining point, the reading pt in the diagram below.
As the  X, Y value defined earlier, we write point_1's X, Y, Z(center) value to the txt file. We plan to check 20 points in this step, the point file was stored in the CNC controller in txt or .NC format.   
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/1%20probe%2020%20pts.jpg width="600" >
<b>


## 2. Generate machining toolpath for CNC or robot 
> ### 2.1   Create points from probing data
>>  If the PC to CNC connection is ready, you can access the point coordinate file from the PC, or copy it to the PC, run apps and create points within NX CADCAM.

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.1a%20read%20points%20from%20csv.jpg width="600" >
<b>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.1%20read%20points%20from%20csv.jpg width="600" >
<b>
  
> ### 2.2 Create splines from points
>> In this step, some parameters will be defined here; incorrect parameters will result in inaccuracies in the created spline.
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.2a%20create%20splines%20from%20points.JPG width="600" 
<br/>

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.2%20create%20splines%20from%20points.JPG width="600" >
</p>

<br/>

> ### 2.3 Create through curve face from splines
>> Now we have created a face from the splines, we may arrange a manual face-to-face deviation checking to know the mismatch. (ignored here)
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.3%20create%20through%20curves%20face%20from%20splines.jpg width="600" >
</p>

<br/>

> ### 2.4 Create thicken and extract face from the bottom.
>> The thickness value of the "thicken" feature should be the radius of the probe (3mm), now we obtained the bottom face that is touching the egg face, extracted the face, and renamed it as "EXTRACE01", it will be used to generate toolpath in next step.
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.4%20face%20offset.jpg width="600" >
<br>
  
> ### 2.5 Generate toolpath
> > Continue from the last step, we start the journal recording, create the machining operation in the same NX file, select face "EXTRACE01" as Drive Method, create tools and toolpath, stop the recording, and save.
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5a%20create%20toolpath.JPG width="600" >
<br/>

>> Open journal codes recorded, change the Drive Geometry with the face which is named "EXTRACT01" then build .dll file, run it from NX-->File-->Excute, the toolpath will be generated. 
<br/>

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5b%20create%20toolpath.JPG width="600" >
<br/>

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5%20create%20toolpath.JPG width="600" >
<br/>

## 3. Confirmation  
Move the pt 20 2mm along the Z+ direction, execute the entire codes (2.1~2.5), we observe  toolpath was updated accordinately. 

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/3%20compare%20toolpaths.jpg width="600" >


<br/>



For this testing, we utilize Siemens NX12 and Visual Studio 2019, if you are interested in the topic, we welcome your comments or suggestions, please leave a message at wjian88@gmail.com. Thank you.  

Wang Jian, 2024 Feb 17.    

--------------------------------------

The movie below shows step 2.1~2.5 mentioned above:   
[![Create points on face with NX OPEN](https://img.youtube.com/vi/v9PnJA1PkYM/0.jpg)](https://www.youtube.com/watch?v=v9PnJA1PkYM)







