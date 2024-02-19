# Learn NX Open and Adaptive Machining technique from egg engraving
The adaptive machining process can be split into two steps: 
## **1. Detect part deformation**  
Detect part distortion by probe:    
Part deformation can be checked by probe or scanner, here we use the probe as an example:   


When the probe hits part along the Z direction, we can get the Z coordinate of the destining point, the reading pt in the diagram below.
As the  X, Y value defined earlier, we write point_1's X, Y, Z(center) value to the txt file. We plan to check 20 points in this step, the point file was stored in CNC controller in txt or .NC format.   
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/1%20probe%2020%20pts.jpg width="600" >
<b>


## 2. Generate machining toolpath for CNC or robot 
> ### 2.1   Create points from probing data
>>  If the PC to CNC connection is ready, you can access the point coordinate file from the PC, or copy it to the PC, run apps and create points within NX CADCAM.
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.1%20read%20points%20from%20csv.jpg width="600" >
<b>
  
> 2.2   Create splines from points
>> Some parameters will be defined here; incorrect parameters will result in inaccuracies in the created spline (surface)
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.2a%20create%20splines%20from%20points.JPG width="600" 
<br/>

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.2%20create%20splines%20from%20points.JPG width="600" >
</p>

<br/>

> 2.3   Create through curve face from splines
> <br/>
>> Now we have created a face from the splines, we may arrange a manual face-to-face deviation checking to know the mismatch. (ignored here)
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.3%20create%20through%20curves%20face%20from%20splines.jpg width="600" >
</p>

<br/>

> 2.4   Create thicken and extract face from the bottom.
> <br/>
>> The thickness value of the "thicken" feature should be the radius of the probe (3mm), now we obtained the bottom face that is touching the egg face, extracted the face, and renamed it as "EXTRACE01", it will be used to generate toolpath in next step.
<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.4%20face%20offset.jpg width="600" >
</p>
<br/>

<br/>
> 2.5  Continue from the last step, we start the journal recording, create the machining operation in the same NX file, select face "EXTRACE01" as Drive Method, create tools and toolpath, stop the recording, and save.
<br/>



<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5a%20create%20toolpath.JPG width="600" >
</p>

<br/>
> Open journal codes, change the Drive Geometry with the face which is named "EXTRACT01" then build .dll file, then run it from NX CADCAM interface. 
<br/>

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5b%20create%20toolpath.JPG width="600" >
</p>
<br/>

<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5%20create%20toolpath.JPG width="600" >
</p>

<br/>

## 3. Move the pt 20 2mm in Z+, run codes again

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/3%20compare%20toolpaths.jpg width="600" >
</p>

<br/>



For this testing, we utilize Siemens NX12 and Visual Studio 2019, for any comments or suggestions, please leave a message at wjian88@gmail.com. Thank you.
Wang Jian 2024 Feb 17.

https://github.com/Plus-1000/Learn_NX_Open/assets/67260387/bee8f803-8efd-4543-a77f-804f110029b2

