
' NX 12.0.0.27
' Journal created by wangjian on Fri Oct 06 21:31:13 2023 Malay Peninsula Standard Time
'
Imports System
Imports NXOpen
Imports NXOpen.UI
Imports NXOpen.UF
Imports NXOpen.Utilities


Module NXJournal
    Sub Main(ByVal args() As String)

        Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
        Dim ufs As UFSession = UFSession.GetUFSession()
        Dim workPart As NXOpen.Part = theSession.Parts.Work
        Dim displayPart As NXOpen.Part = theSession.Parts.Display
        theSession.ApplicationSwitchImmediate("UG_APP_MODELING")
        Dim lw As ListingWindow = theSession.ListingWindow
        lw.Open()

        ' ----------------------------------------------
        '   Menu: Insert->Mesh Surface->Through Curves...
        ' ----------------------------------------------
        Dim nullNXOpen_Features_Feature As NXOpen.Features.Feature = Nothing
        Dim throughCurvesBuilder1 As NXOpen.Features.ThroughCurvesBuilder = Nothing
        throughCurvesBuilder1 = workPart.Features.CreateThroughCurvesBuilder(nullNXOpen_Features_Feature)
        throughCurvesBuilder1.PreserveShape = False
        throughCurvesBuilder1.PatchType = NXOpen.Features.ThroughCurvesBuilder.PatchTypes.Multiple
        throughCurvesBuilder1.Alignment.AlignCurve.DistanceTolerance = 0.01
        throughCurvesBuilder1.Alignment.AlignCurve.ChainingTolerance = 0.0095
        throughCurvesBuilder1.SectionTemplateString.DistanceTolerance = 0.01
        throughCurvesBuilder1.SectionTemplateString.ChainingTolerance = 0.0095
        throughCurvesBuilder1.Alignment.AlignCurve.AngleTolerance = 0.5
        throughCurvesBuilder1.SectionTemplateString.AngleTolerance = 0.5

        '----------delete old through curve feature---------
        For Each myFeature As Object In theSession.Parts.Work.Features
            lw.WriteLine(myFeature.GetType.ToString)
            lw.WriteLine(myFeature.Name)
            If myFeature.GetType.ToString = "NXOpen.Features.ThroughCurves" Then
                lw.WriteLine("========through curves found======")
                If myFeature.name = "FACE01" Then
                    lw.WriteLine("======face01 found=========")
                    lw.WriteLine("===myFeature.tag===" & myFeature.tag.ToString)
                    ufs.Obj.DeleteObject(myFeature.tag.ToString)
                    Exit For '---delete one face only---

                    '---delet more than one obj, put faces into array--------
                    'UF_OBJ_delete_array_of_objects(Int num_objects,tag_t object_id [ ] ,Int * * statuses)

                    '------put obj tag in a loop, delete items one by one
                    'Dim objs As NXObject() = workPart.Layers.GetAllObjectsOnLayer(1)
                    'For i As Integer = 0 To objs.Length - 1
                    '    ufs.obj.deleteobject(objs(i).tag)
                    'Next
                End If
            End If
        Next

        '----------- get feature JournalIdentifier from it's name--------
        Dim sp1, sp2, sp3, sp4 As String
        For Each myFeature As Object In theSession.Parts.Work.Features
            lw.WriteLine(myFeature.GetType.ToString)
            lw.WriteLine(myFeature.Name)
            If myFeature.GetType.ToString = "NXOpen.Features.StudioSpline" Then
                If myFeature.name = "NEW_SPLINE1" Then

                    sp1 = myFeature.JournalIdentifier.ToString
                    lw.WriteLine("===myFeature.getfeature.name===" & sp1)
                End If
                If myFeature.name = "NEW_SPLINE6" Then

                    sp2 = myFeature.JournalIdentifier.ToString
                    lw.WriteLine("===myFeature.getfeature.name===" & sp2)
                End If
                If myFeature.name = "NEW_SPLINE11" Then

                    sp3 = myFeature.JournalIdentifier.ToString
                    lw.WriteLine("===myFeature.getfeature.name===" & sp3)
                End If
                If myFeature.name = "NEW_SPLINE16" Then

                    sp4 = myFeature.JournalIdentifier.ToString
                    lw.WriteLine("===myFeature.getfeature.name===" & sp4)
                End If
            End If
        Next

        '-----------through curve face, section 1---------------
        Dim section1 As NXOpen.Section = Nothing
        section1 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5)
        throughCurvesBuilder1.SectionsList.Append(section1)
        section1.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.CurvesAndPoints)

        Dim features1(0) As NXOpen.Features.Feature
        Dim studioSpline1 As NXOpen.Features.StudioSpline = CType(workPart.Features.FindObject(sp1), NXOpen.Features.StudioSpline)
        features1(0) = studioSpline1
        Dim curveFeatureRule1 As NXOpen.CurveFeatureRule = Nothing
        curveFeatureRule1 = workPart.ScRuleFactory.CreateRuleCurveFeature(features1)
        section1.AllowSelfIntersection(False)

        Dim rules1(0) As NXOpen.SelectionIntentRule
        rules1(0) = curveFeatureRule1
        Dim spline1 As NXOpen.Spline = CType(studioSpline1.FindObject("CURVE 1"), NXOpen.Spline)
        Dim nullNXOpen_NXObject As NXOpen.NXObject = Nothing
        Dim helpPoint1 As NXOpen.Point3d = New NXOpen.Point3d(21.4, 10.0, 17.9)
        section1.AddToSection(rules1, spline1, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint1, NXOpen.Section.Mode.Create, False)

        Dim sections1(0) As NXOpen.Section
        sections1(0) = section1
        throughCurvesBuilder1.Alignment.SetSections(sections1)

        '-----------through curve face, section 2---------------
        Dim section2 As NXOpen.Section = Nothing
        section2 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5)
        throughCurvesBuilder1.SectionsList.Append(section2)
        section2.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.CurvesAndPoints)

        Dim features2(0) As NXOpen.Features.Feature
        Dim studioSpline2 As NXOpen.Features.StudioSpline = CType(workPart.Features.FindObject(sp2), NXOpen.Features.StudioSpline)
        features2(0) = studioSpline2
        Dim curveFeatureRule2 As NXOpen.CurveFeatureRule = Nothing
        curveFeatureRule2 = workPart.ScRuleFactory.CreateRuleCurveFeature(features2)

        section2.AllowSelfIntersection(False)
        Dim rules2(0) As NXOpen.SelectionIntentRule
        rules2(0) = curveFeatureRule2
        Dim spline2 As NXOpen.Spline = CType(studioSpline2.FindObject("CURVE 1"), NXOpen.Spline)
        Dim helpPoint2 As NXOpen.Point3d = New NXOpen.Point3d(22.1, 6.0, 19.7)
        section2.AddToSection(rules2, spline2, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint2, NXOpen.Section.Mode.Create, False)
        Dim sections2(1) As NXOpen.Section
        sections2(0) = section1
        sections2(1) = section2
        throughCurvesBuilder1.Alignment.SetSections(sections2)

        '-----------through curve face, section 3---------------
        Dim section3 As NXOpen.Section = Nothing
        section3 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5)
        throughCurvesBuilder1.SectionsList.Append(section3)
        section3.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.CurvesAndPoints)
        Dim features3(0) As NXOpen.Features.Feature
        Dim studioSpline3 As NXOpen.Features.StudioSpline = CType(workPart.Features.FindObject(sp3), NXOpen.Features.StudioSpline)

        features3(0) = studioSpline3
        Dim curveFeatureRule3 As NXOpen.CurveFeatureRule = Nothing
        curveFeatureRule3 = workPart.ScRuleFactory.CreateRuleCurveFeature(features3)
        section3.AllowSelfIntersection(False)
        Dim rules3(0) As NXOpen.SelectionIntentRule
        rules3(0) = curveFeatureRule3
        Dim spline3 As NXOpen.Spline = CType(studioSpline3.FindObject("CURVE 1"), NXOpen.Spline)
        Dim helpPoint3 As NXOpen.Point3d = New NXOpen.Point3d(22.8, 1.9, 20.6)
        section3.AddToSection(rules3, spline3, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint3, NXOpen.Section.Mode.Create, False)

        Dim sections3(2) As NXOpen.Section
        sections3(0) = section1
        sections3(1) = section2
        sections3(2) = section3
        throughCurvesBuilder1.Alignment.SetSections(sections3)

        '-----------through curve face, section 4---------------
        Dim section4 As NXOpen.Section = Nothing
        section4 = workPart.Sections.CreateSection(0.0095, 0.01, 0.5)
        throughCurvesBuilder1.SectionsList.Append(section4)
        section4.SetAllowedEntityTypes(NXOpen.Section.AllowTypes.CurvesAndPoints)

        Dim features4(0) As NXOpen.Features.Feature
        Dim studioSpline4 As NXOpen.Features.StudioSpline = CType(workPart.Features.FindObject(sp4), NXOpen.Features.StudioSpline)
        features4(0) = studioSpline4
        Dim curveFeatureRule4 As NXOpen.CurveFeatureRule = Nothing
        curveFeatureRule4 = workPart.ScRuleFactory.CreateRuleCurveFeature(features4)
        section4.AllowSelfIntersection(False)
        Dim rules4(0) As NXOpen.SelectionIntentRule
        rules4(0) = curveFeatureRule4
        Dim spline4 As NXOpen.Spline = CType(studioSpline4.FindObject("CURVE 1"), NXOpen.Spline)
        Dim helpPoint4 As NXOpen.Point3d = New NXOpen.Point3d(25.1, -2.0, 20.8)
        section4.AddToSection(rules4, spline4, nullNXOpen_NXObject, nullNXOpen_NXObject, helpPoint4, NXOpen.Section.Mode.Create, False)

        '-----------add all sections to through curve builder----------------
        Dim sections4(3) As NXOpen.Section
        sections4(0) = section1
        sections4(1) = section2
        sections4(2) = section3
        sections4(3) = section4
        throughCurvesBuilder1.Alignment.SetSections(sections4)

        '-----------build through curve feature------------
        Dim feature1 As NXOpen.Features.Feature = Nothing
        feature1 = throughCurvesBuilder1.CommitFeature()
        throughCurvesBuilder1.Destroy()
        '----------rename chrough curve face generated-------
        feature1.SetName("FACE01")


        'ufs.Modl.CreateThruCurves()

        'Public Sub CreateThruCurves(ByRef s_section As StringList, ByRef s_spine As StringList,
        'ByRef patch As Integer, ByRef alignment As Integer, value() As Double, ByRef vdegree As Integer,
        'ByRef vstatus As Integer, ByRef body_type As Integer, [boolean] As FeatureSigns,
        'tol() As Double, c_face_id() As Tag, c_flag() As Integer, ByRef body_obj_id As Tag)
    End Sub
    Public Function GetUnloadOption(ByVal dummy As String) As Integer
        'Unloads the image immediately after execution within NX
        GetUnloadOption = NXOpen.Session.LibraryUnloadOption.Immediately
    End Function
End Module





