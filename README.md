# Learn NX Open and Adaptive Machining from egg engraving
The adaptive machining process can be split into two steps: 
## **1. Detect job deformation**  
Detect part distortion by probe:
Part deformation can be checked by probe or scanner, here we use the probe as an example: 

When the probe hits part along the Z direction, we can get the Z distance of the destining point,
as the  X, Y value defined earlier, we write point 1's X, Y, Z value to the txt file. We plan to check 20 points in this step.


<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/1%20probe%2020%20pts.jpg width="800" >
</p>

<br/>




## **2. Generate machining toolpath for CNC or robot**

> **2.1   Create points from probing data (stored in .csv file)**

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.1%20read%20points%20from%20csv.jpg width="800" >
</p>
<br/>

> 2.2   Create splines from points
> <br/>
>> Many parameters will be defined here; incorrect parameters will result in inaccuracies in the created spline (surface)

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.2%20create%20splines%20from%20points.JPG width="800" >
</p>

<br/>

> 2.3   Create through curve face from splines

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.3%20create%20through%20curves%20face%20from%20splines.jpg width="800" >
</p>

<br/>

> 2.4   Create thicken and extract

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.4%20face%20offset.jpg width="800" >
</p>

<br/>

> 2.5   Create points from probing data (stored in csv file)

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/2.5%20create%20toolpath.JPG width="800" >
</p>

<br/>
## **3. test Generate machining toolpath for CNC or robot**

<br/>
<p align="center">
<img src=https://github.com/Plus-1000/Learn-NX-Open-and-adaptive-machining-from-egg-engraving/blob/main/image/3%20compare%20toolpaths.jpg width="800" >
</p>

<br/>





https://github.com/Plus-1000/Learn_NX_Open/assets/67260387/bee8f803-8efd-4543-a77f-804f110029b2

